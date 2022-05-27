using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using ZdravoKorporacija;
using ZdravoKorporacija.DTO;

namespace Service
{
    public class AppointmentService
    {
        private readonly AppointmentRepository AppointmentRepository;
        private readonly PatientRepository PatientRepository;
        private readonly DoctorRepository DoctorRepository;
        private readonly RoomRepository RoomRepository;
        public AppointmentService(AppointmentRepository appointmentRepository, PatientRepository patientRepository,
            DoctorRepository doctorRepository, RoomRepository roomRepository)
        {
            this.AppointmentRepository = appointmentRepository;
            this.PatientRepository = patientRepository;
            this.DoctorRepository = doctorRepository;
            this.RoomRepository = roomRepository;
        }
        public AppointmentService() { }

        public List<Appointment> GetAllAppointments()
        {
            return AppointmentRepository.FindAll();
        }
        public Model.Appointment? GetOneById(int appointmentId)
        {
            return AppointmentRepository.FindOneById(appointmentId);
        }
        public List<Appointment> GetAppointmentsByPatientJmbg(String patientJmbg)
        {
            return AppointmentRepository.FindAllByPatientJmbg(patientJmbg);
        }
        public List<Appointment> GetAppointmentsByDoctorJmbg(String doctorJmbg)
        {
            return AppointmentRepository.FindAllByDoctorJmbg(doctorJmbg);
        }
        public List<PossibleAppointmentsDTO> GetPossibleAppointmentsDtos()
        {
            List<Appointment> appointments = AppointmentRepository.FindAll();
            return CreatePossibleAppointmentsDtos(appointments);
        }

        public List<PossibleAppointmentsDTO> GetAllFutureAppointmentsByPatient()
        {
            List<Appointment> appointments = AppointmentRepository.GetAllFutureByPatient(App.loggedUser.Jmbg);
            return CreatePossibleAppointmentsDtos(appointments);
        }
        public List<PossibleAppointmentsDTO> GetAllPastAppointmentsByPatient()
        {
            List<Appointment> appointments = AppointmentRepository.GetAllPastAppointmentsByPatient(App.loggedUser.Jmbg);
            return CreatePossibleAppointmentsDtos(appointments);
        }

        private List<PossibleAppointmentsDTO> CreatePossibleAppointmentsDtos(List<Appointment> appointments)
        {
            List<PossibleAppointmentsDTO> possibleAppointmentsDTO = new List<PossibleAppointmentsDTO>();
            if (appointments.Count > 0)
            {
                foreach (var ap in appointments)
                {
                    Doctor doctor = DoctorRepository.FindOneByJmbg(ap.DoctorJmbg);
                    Patient patient = PatientRepository.FindOneByJmbg(ap.PatientJmbg);
                    Room room = RoomRepository.FindOneById(ap.RoomId);
                    possibleAppointmentsDTO.Add(new PossibleAppointmentsDTO(ap.PatientJmbg,
                        patient.FirstName + " " + patient.LastName,
                        ap.DoctorJmbg, doctor.FirstName + " " + doctor.LastName, doctor.SpecialtyType, ap.RoomId,
                        room.Name, ap.StartTime, ap.Duration, ap.Id));
                }
            }
            possibleAppointmentsDTO.Sort((x, y) => DateTime.Compare(x.StartTime.AddMinutes(x.Duration), y.StartTime.AddMinutes(y.Duration)));
            return possibleAppointmentsDTO;
        }
        public List<AppointmentDTO> GetAppointmentsByDoctorJmbgDTO(String doctorJmbg)
        {
            List<AppointmentDTO> appointmentDTOs = new List<AppointmentDTO>();
            List<Appointment> appointments = AppointmentRepository.FindAllByDoctorJmbg(doctorJmbg);
            foreach (Appointment app in appointments)
            {
                Patient patient = PatientRepository.FindOneByJmbg(app.PatientJmbg);
                appointmentDTOs.Add(new AppointmentDTO(app.Id, patient.Jmbg, patient.FirstName,
                    patient.LastName, app.StartTime, RoomRepository.FindOneById(app.RoomId).Name));
            }
            return appointmentDTOs;
        }
        private int GenerateNewId()
        {
            try
            {
                List<Appointment> appointments = AppointmentRepository.FindAll();
                int currentMax = appointments.Max(obj => obj.Id);
                return currentMax + 1;
            }
            catch
            {
                return 1;
            }
        }
        public void DeleteAppointment(int appointmentId)
        {
            if (AppointmentRepository.FindOneById(appointmentId) == null)
            {
                throw new Exception("Appointment with that id doesn't exist!");
            }
            AppointmentRepository.RemoveAppointment(appointmentId);
        }

        public void DeleteAppointmentsForOnePatient(String patientJmbg)
        {
            AppointmentRepository.RemoveAppointmentsForOnePatient(patientJmbg);
        }

        public void DeleteAppointmentByRoomId(int roomId)
        {
            AppointmentRepository.RemoveAppointmentByRoomId(roomId);
        }

        public void ModifyAppointment(int appointmentId, DateTime newDate)
        {

            var oneAppointment = AppointmentRepository.FindOneById(appointmentId);
            if (oneAppointment == null)
            {
                throw new Exception("Appointment with that id doesn't exist!");
            }
            oneAppointment.StartTime = newDate;
            if (!oneAppointment.validateAppointment())
            {
                throw new Exception("Something went wrong, new appointment isn't modified!");
            }
            AppointmentRepository.UpdateAppointment(oneAppointment);
        }
        public void CreateAppointmentByPatient(DateTime date, String doctorJmbg)
        {
            Doctor doctor = DoctorRepository.FindOneByJmbg(doctorJmbg);
            ValidateInputParametersForCreateAppointment(App.loggedUser.Jmbg, doctorJmbg, doctor.RoomId);
            int id = GenerateNewId();
            Appointment appointment = new Appointment(date, 15, id, App.loggedUser.Jmbg, doctorJmbg, doctor.RoomId);
            SaveNewAppointment(appointment);
        }

        public void CreateAppointmentBySecretary(String patientJmbg, String doctorJmbg, int roomId,
            DateTime startTime, int duration)
        {
            ValidateInputParametersForCreateAppointment(patientJmbg, doctorJmbg, roomId);
            int id = GenerateNewId();
            Appointment appointment = new Appointment(startTime, duration, id, patientJmbg, doctorJmbg, roomId);
            SaveNewAppointment(appointment);
        }
        public void CreateAppointmentByDoctor(PossibleAppointmentsDTO appointmentToCreate)
        {
            ValidateInputParametersForCreateAppointment(appointmentToCreate.PatientJmbg, appointmentToCreate.DoctorJmbg, appointmentToCreate.RoomId);
            int id = GenerateNewId();
            Appointment appointment = new Appointment(appointmentToCreate.StartTime, appointmentToCreate.Duration, id, appointmentToCreate.PatientJmbg, appointmentToCreate.DoctorJmbg, appointmentToCreate.RoomId);
            SaveNewAppointment(appointment);
        }

        private void ValidateInputParametersForCreateAppointment(string patientJmbg, string doctorJmbg, int roomId)
        {
            if (PatientRepository.FindOneByJmbg(patientJmbg) == null)
                throw new Exception("Patient with that JMBG doesn't exist!");
            if (DoctorRepository.FindOneByJmbg(doctorJmbg) == null)
                throw new Exception("Doctor with that JMBG doesn't exist!");
            if (RoomRepository.FindOneById(roomId) == null)
                throw new Exception("Room with that id doesn't exist!");
        }
        private void SaveNewAppointment(Appointment appointment)
        {
            if (!appointment.validateAppointment())
            {
                throw new Exception("Something went wrong, new appointment isn't created!");
            }

            AppointmentRepository.SaveAppointment(appointment);
        }

        //OVO PROVJERITI DA LI SE UOPSTE POZIVA NEGDJE, AKO NE IZBRISATI
        public void CreateOperationAppointment(PossibleAppointmentsDTO appointmentToCreate)
        {
            Boolean specialty = DoctorRepository.FindOneByJmbg(appointmentToCreate.DoctorJmbg).Specialty;
            if (!specialty)
                throw new Exception("Only doctors with specialization can perform operation!");
            if (appointmentToCreate.DoctorJmbg.Equals("1231231231231")) //hard codovan ulogovan doktor, jer operaciju moze samo kod sebe da zakaze
                CreateAppointmentByDoctor(appointmentToCreate);
        }


    }
}