using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using ZdravoKorporacija;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Interfaces;

namespace Service
{
    public class AppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IRoomRepository _roomRepository;
        public AppointmentService(IAppointmentRepository appointmentRepository, IPatientRepository patientRepository,
            IDoctorRepository doctorRepository, IRoomRepository roomRepository)
        {
            this._appointmentRepository = appointmentRepository;
            this._patientRepository = patientRepository;
            this._doctorRepository = doctorRepository;
            this._roomRepository = roomRepository;
        }
        public AppointmentService() { }

        public List<Appointment> GetAllAppointments()
        {
            return _appointmentRepository.FindAll();
        }
        public Model.Appointment? GetOneById(int appointmentId)
        {
            return _appointmentRepository.FindOneById(appointmentId);
        }
        public List<Appointment> GetAppointmentsByPatientJmbg(String patientJmbg)
        {
            return _appointmentRepository.FindAllByPatientJmbg(patientJmbg);
        }
        public List<Appointment> GetAppointmentsByDoctorJmbg(String doctorJmbg)
        {
            return _appointmentRepository.FindAllByDoctorJmbg(doctorJmbg);
        }
        public List<PossibleAppointmentsDTO> GetPossibleAppointmentsDtos()
        {
            List<Appointment> appointments = _appointmentRepository.FindAll();
            return CreatePossibleAppointmentsDtos(appointments);
        }

        public List<PossibleAppointmentsDTO> GetAllFutureAppointmentsByPatient()
        {
            List<Appointment> appointments = _appointmentRepository.GetAllFutureByPatient(App.loggedUser.Jmbg);
            appointments.Sort((emp1, emp2) => emp1.StartTime.CompareTo(emp2.StartTime));
            return CreatePossibleAppointmentsDtos(appointments);
        }
        public List<PossibleAppointmentsDTO> GetAllPastAppointmentsByPatient()
        {
            List<Appointment> appointments = _appointmentRepository.GetAllPastAppointmentsByPatient(App.loggedUser.Jmbg);
            return CreatePossibleAppointmentsDtos(appointments);
        }
        public List<PossibleAppointmentsDTO> GetAllByJmbgAndDate(DateTime date)
        {
            List<PossibleAppointmentsDTO> appointments = this.GetAllFutureAppointmentsByPatient();
            List<PossibleAppointmentsDTO> possibleAppointmentsDTO = new List<PossibleAppointmentsDTO>();
            foreach (var ap in appointments)
            {
                if (date.Date == ap.StartTime.Date)
                    possibleAppointmentsDTO.Add(ap);
            }
            possibleAppointmentsDTO.Sort((emp1, emp2) => emp1.StartTime.Date.CompareTo(emp2.StartTime.Date));
            return possibleAppointmentsDTO;
        }
        private List<PossibleAppointmentsDTO> CreatePossibleAppointmentsDtos(List<Appointment> appointments)
        {
            List<PossibleAppointmentsDTO> possibleAppointmentsDTO = new List<PossibleAppointmentsDTO>();
            if (appointments.Count > 0)
            {
                foreach (var ap in appointments)
                {
                    Doctor doctor = _doctorRepository.FindOneByJmbg(ap.DoctorJmbg);
                    Patient patient = _patientRepository.FindOneByJmbg(ap.PatientJmbg);
                    Room room = _roomRepository.FindOneById(ap.RoomId);
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
            List<Appointment> appointments = _appointmentRepository.FindAllByDoctorJmbg(doctorJmbg);
            List<AppointmentDTO> appointmentDTOs = new List<AppointmentDTO>();
            foreach (Appointment app in appointments)
            {
                Patient patient = _patientRepository.FindOneByJmbg(app.PatientJmbg);
                appointmentDTOs.Add(new AppointmentDTO(app.Id, patient.Jmbg, patient.FirstName,
                    patient.LastName, app.StartTime, _roomRepository.FindOneById(app.RoomId).Name));
            }
            return appointmentDTOs;
        }
        private int GenerateNewId()
        {
            try
            {
                List<Appointment> appointments = _appointmentRepository.FindAll();
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
            if (_appointmentRepository.FindOneById(appointmentId) == null)
                throw new Exception("Appointment with that id doesn't exist!");
            _appointmentRepository.RemoveAppointment(appointmentId);
        }

        public void DeleteAppointmentsForOnePatient(String patientJmbg)
        {
            _appointmentRepository.RemoveAppointmentsForOnePatient(patientJmbg);
        }

        public void DeleteAppointmentByRoomId(int roomId)
        {
            _appointmentRepository.RemoveAppointmentByRoomId(roomId);
        }

        public void ModifyAppointment(int appointmentId, DateTime newDate)
        {

            var oneAppointment = _appointmentRepository.FindOneById(appointmentId);
            if (oneAppointment == null)
            {
                throw new Exception("Appointment with that id doesn't exist!");
            }
            oneAppointment.StartTime = newDate;
            if (!oneAppointment.validateAppointment())
            {
                throw new Exception("Something went wrong, new appointment isn't modified!");
            }
            _appointmentRepository.UpdateAppointment(oneAppointment);
        }
        public void CreateAppointmentByPatient(DateTime date, String doctorJmbg)
        {
            Doctor doctor = _doctorRepository.FindOneByJmbg(doctorJmbg);
            ValidateInputParametersForCreateAppointment(App.loggedUser.Jmbg, doctorJmbg, doctor.RoomId, 45);
            int id = GenerateNewId();
            Appointment appointment = new Appointment(date, 15, id, App.loggedUser.Jmbg, doctorJmbg, doctor.RoomId);
            SaveNewAppointment(appointment);
        }

        public void CreateAppointmentBySecretary(String patientJmbg, String doctorJmbg, int roomId,
            DateTime startTime, int duration)
        {
            ValidateInputParametersForCreateAppointment(patientJmbg, doctorJmbg, roomId, duration);
            int id = GenerateNewId();
            Appointment appointment = new Appointment(startTime, duration, id, patientJmbg, doctorJmbg, roomId);
            SaveNewAppointment(appointment);
        }
        public void CreateAppointmentByDoctor(PossibleAppointmentsDTO appointmentToCreate)
        {
            ValidateInputParametersForCreateAppointment(appointmentToCreate.PatientJmbg, appointmentToCreate.DoctorJmbg, appointmentToCreate.RoomId, appointmentToCreate.Duration);
            int id = GenerateNewId();
            Appointment appointment = new Appointment(appointmentToCreate.StartTime, appointmentToCreate.Duration, id, appointmentToCreate.PatientJmbg, appointmentToCreate.DoctorJmbg, appointmentToCreate.RoomId);
            SaveNewAppointment(appointment);
        }

        private void ValidateInputParametersForCreateAppointment(string patientJmbg, string doctorJmbg, int roomId, int duration)
        {
            if (_patientRepository.FindOneByJmbg(patientJmbg) == null)
                throw new Exception("Patient with that JMBG doesn't exist!");
            if (_doctorRepository.FindOneByJmbg(doctorJmbg) == null)
                throw new Exception("Doctor with that JMBG doesn't exist!");
            if (_roomRepository.FindOneById(roomId) == null)
                throw new Exception("Room with that id doesn't exist!");
            if (duration < 1 || duration == null)
            {
                throw new Exception("Please insert duration!");
            }
        }
        private void SaveNewAppointment(Appointment appointment)
        {
            if (!appointment.validateAppointment())
            {
                throw new Exception("Something went wrong, new appointment isn't created!");
            }

            _appointmentRepository.SaveAppointment(appointment);
        }

        public void CreateOperationAppointment(PossibleAppointmentsDTO appointmentToCreate)
        {
            String jmbg = "1231231231231";
            Boolean specialty = _doctorRepository.FindOneByJmbg(jmbg).Specialty;
            if (!specialty)
                throw new Exception("Only doctors with specialization can perform operation!");
            if (appointmentToCreate.DoctorJmbg.Equals(jmbg))
                CreateAppointmentByDoctor(appointmentToCreate);
        }

        public List<AppointmentDTO> FilterByTime(DateTime dateFrom, DateTime dateTo)
        {
            List<AppointmentDTO> appointments = GetAppointmentsByDoctorJmbgDTO(App.loggedUser.Jmbg);
            List<AppointmentDTO> filteredAppointments = new List<AppointmentDTO>();

            foreach (AppointmentDTO appointment in appointments)
            {
                if (appointment.StartTime > dateFrom && appointment.StartTime < dateTo)
                {
                    filteredAppointments.Add(appointment);
                }
            }

            return filteredAppointments;
        }


    }
}