using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using ZdravoKorporacija;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;

namespace Service
{
    public class AppointmentService
    {
        readonly AppointmentRepository AppointmentRepository = new AppointmentRepository();
        readonly PatientRepository PatientRepository = new PatientRepository();
        readonly DoctorRepository DoctorRepository = new DoctorRepository();
        readonly RoomRepository RoomRepository = new RoomRepository();
        readonly BasicRenovationRepository BasicRenovationRepository = new BasicRenovationRepository();
        readonly AdvancedRenovationJoiningRepository AdvancedRenovationJoiningRepository = new AdvancedRenovationJoiningRepository();
        readonly AdvancedRenovationSeparationRepository AdvancedRenovationSeparationRepository = new AdvancedRenovationSeparationRepository();
        public AppointmentService(AppointmentRepository appointmentRepository, PatientRepository patientRepository,
            DoctorRepository doctorRepository, RoomRepository roomRepository, BasicRenovationRepository basicRenovationRepository)
        {
            this.AppointmentRepository = appointmentRepository;
            this.PatientRepository = patientRepository;
            this.DoctorRepository = doctorRepository;
            this.RoomRepository = roomRepository;
            this.BasicRenovationRepository = basicRenovationRepository;
        }
        public AppointmentService() { }

        public List<Appointment> GetAllAppointments()
        {
            return AppointmentRepository.FindAll();
        }

        public List<PossibleAppointmentsDTO> GetAllAppointmentsBySecretary()
        {
            List<Appointment> appointments = AppointmentRepository.FindAll();
            List<PossibleAppointmentsDTO> possibleAppointmentsDTO = new List<PossibleAppointmentsDTO>();
            if (appointments.Count > 0)
            {
                foreach (var ap in appointments)
                {
                    Doctor doctor = DoctorRepository.FindOneByJmbg(ap.DoctorJmbg);
                    Patient patient = PatientRepository.FindOneByJmbg(ap.PatientJmbg);
                    Room room = RoomRepository.FindOneById(ap.RoomId);
                    possibleAppointmentsDTO.Add(new PossibleAppointmentsDTO(ap.PatientJmbg, patient.FirstName + " " + patient.LastName,
                        ap.DoctorJmbg, doctor.FirstName + " " + doctor.LastName, doctor.SpecialtyType, ap.RoomId, room.Name, ap.StartTime, ap.Duration, ap.Id));
                }
            }
            possibleAppointmentsDTO.Sort((x, y) => DateTime.Compare(x.StartTime.AddMinutes(x.Duration), y.StartTime.AddMinutes(y.Duration)));
            return possibleAppointmentsDTO;
        }


        public List<PossibleAppointmentsDTO> GetAllFutureAppointmentsByPatient()
        {
            List<Appointment> appointments = AppointmentRepository.GetAllFutureByPatient(App.loggedUser.Jmbg);
            List<PossibleAppointmentsDTO> possibleAppointmentsDTO = new List<PossibleAppointmentsDTO>();
            foreach (var ap in appointments)
            {
                Doctor? doctor = DoctorRepository.FindOneByJmbg(ap.DoctorJmbg);
                Patient? patient = PatientRepository.FindOneByJmbg(ap.PatientJmbg);
                Room? room = RoomRepository.FindOneById(ap.RoomId);
                possibleAppointmentsDTO.Add(new PossibleAppointmentsDTO(ap.PatientJmbg, patient.FirstName + " " + patient.LastName,
                    ap.DoctorJmbg, doctor.FirstName + " " + doctor.LastName, doctor.SpecialtyType, ap.RoomId, room.Name, ap.StartTime, ap.Duration, ap.Id));
            }
            return possibleAppointmentsDTO;
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
            else
            {
                AppointmentRepository.RemoveAppointment(appointmentId);
            }
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
            else
            {
                Appointment newAppointment = new Appointment(newDate, oneAppointment.Duration, oneAppointment.Id, oneAppointment.PatientJmbg,
                    oneAppointment.DoctorJmbg, oneAppointment.RoomId);
                if (!newAppointment.validateAppointment())
                {
                    throw new Exception("Something went wrong, new appointment isn't modified!");
                }
                AppointmentRepository.UpdateAppointment(newAppointment);
            }

        }

        public Model.Appointment? GetOneById(int appointmentId)
        {
            return AppointmentRepository.FindOneById(appointmentId);
        }


        public List<Appointment> GetAppointmentsByDoctorJmbg(String doctorJmbg)
        {
            return AppointmentRepository.FindAllByDoctorJmbg(doctorJmbg);
        }

        public List<AppointmentDTO> GetAppointmentsByDoctorJmbgDTO(String doctorJmbg)
        {
            List<AppointmentDTO> appointmentDTOs = new List<AppointmentDTO>();

            List<Appointment> appointments = AppointmentRepository.FindAllByDoctorJmbg(doctorJmbg);
            foreach (Appointment app in appointments)
            {
                AppointmentDTO appointmentDTO = new AppointmentDTO();
                appointmentDTO.Id = app.Id;
                appointmentDTO.PatientJmbg = app.PatientJmbg;
                appointmentDTO.PatientFirstName = PatientRepository.FindOneByJmbg(app.PatientJmbg).FirstName;
                appointmentDTO.PatientLastName = PatientRepository.FindOneByJmbg(app.PatientJmbg).LastName;
                appointmentDTO.StartTime = app.StartTime;
                appointmentDTO.RoomName = RoomRepository.FindOneById(app.RoomId).Name;
                appointmentDTOs.Add(appointmentDTO);
            }
            return appointmentDTOs;
        }

        public List<Appointment> GetAppointmentsByPatientJmbg(String patientJmbg)
        {
            return AppointmentRepository.FindAllByPatientJmbg(patientJmbg);
        }

        public void CreateAppointmentByPatient(DateTime date, String doctorJmbg)
        {
            if (DoctorRepository.FindOneByJmbg(doctorJmbg) == null)
            {
                throw new Exception("Doctor with that JMBG doesn't exist!");
            }
            int id = GenerateNewId();
            Doctor? temp = new Doctor();
            temp = DoctorRepository.FindOneByJmbg(doctorJmbg);
            Appointment appointment = new Appointment(date, 15, id, App.loggedUser.Jmbg, doctorJmbg, temp.RoomId);
            AppointmentRepository.SaveAppointment(appointment);
            if (!appointment.validateAppointment())
            {
                throw new Exception("Something went wrong, new appointment isn't created!");
            }
        }

        /*public String CreateAppointmentByDoctor(DateTime startTime, int duration, String patientJmbg)
        {
            if (PatientRepository.FindOneByJmbg(patientJmbg) == null)
            {
                return "Patient with that JMBG doesn't exist!";
            }
            int id = GenerateNewId();
            Appointment appointment = new Appointment(startTime, duration, id, patientJmbg, App.loggedUser.Jmbg, 11);
            if (!appointment.validateAppointment())
            {
                return "Something went wrong, new appointment isn't created!";
            }
            else
            {
                AppointmentRepository.SaveAppointment(appointment);
                return "";
            }

        }*/

        public void CreateAppointmentBySecretary(String patientJmbg, String doctorJmbg, int roomId,
            DateTime startTime, int duration)
        {
            if (PatientRepository.FindOneByJmbg(patientJmbg) == null)
                throw new Exception("Patient with that JMBG doesn't exist!");
            else if (DoctorRepository.FindOneByJmbg(doctorJmbg) == null)
                throw new Exception("Doctor with that JMBG doesn't exist!");
            else if (RoomRepository.FindOneById(roomId) == null)
                throw new Exception("Room with that id doesn't exist!");
            int id = GenerateNewId();
            Appointment appointment = new Appointment(startTime, duration, id, patientJmbg, doctorJmbg, roomId);
            if (!appointment.validateAppointment())
            {
                throw new Exception("Something went wrong, new appointment isn't created!");
            }
            AppointmentRepository.SaveAppointment(appointment);
        }

        public List<PossibleAppointmentsDTO> GetPossibleAppointmentsForRoomJoin(int firstRoomId, int secondRoomId,
            DateTime dateFrom, DateTime dateUntil, int duration)
        {
            if (RoomRepository.FindOneById(firstRoomId) == null || RoomRepository.FindOneById(secondRoomId) == null)
                throw new Exception("One of the rooms doesn't exist!");
            else if (dateFrom > dateUntil)
                throw new Exception("Dates are not valid!");
            List<DateTime> possibleAppointmentsForFirstRoom = new List<DateTime>();
            List<DateTime> possibleAppointmentsForSecondRoom = new List<DateTime>();
            possibleAppointmentsForFirstRoom = findPossibleStartTimesOfAppointment("", "", firstRoomId,
                    dateFrom, dateUntil, duration);
            possibleAppointmentsForSecondRoom = findPossibleStartTimesOfAppointment("", "", secondRoomId,
                    dateFrom, dateUntil, duration);
            if (possibleAppointmentsForFirstRoom.Count == 0 || possibleAppointmentsForSecondRoom.Count == 0)
                throw new Exception("There are not free appointments for given parameters!");
            List<PossibleAppointmentsDTO> retValue = new List<PossibleAppointmentsDTO>();
            foreach (var pf in possibleAppointmentsForFirstRoom)
            {
                foreach (var ps in possibleAppointmentsForSecondRoom)
                {
                    if (pf == ps && pf > DateTime.Now.AddHours(1))
                    {
                        PossibleAppointmentsDTO possibleAppointmentsDTO = new PossibleAppointmentsDTO("", "", "", "", "", -1, "", pf, duration, -1);
                        retValue.Add(possibleAppointmentsDTO);
                    }
                }
            }
            return retValue;
        }

        public List<PossibleAppointmentsDTO> GetPossibleAppointmentsByManager(int roomId,
            DateTime dateFrom, DateTime dateUntil, int duration)
        {
            if (RoomRepository.FindOneById(roomId) == null)
                throw new Exception("Room with that id doesn't exist!");
            else if (dateFrom > dateUntil)
                throw new Exception("Dates are not valid!");
            List<DateTime> possibleAppointments = new List<DateTime>();
            possibleAppointments = findPossibleStartTimesOfAppointment("", "", roomId,
                    dateFrom, dateUntil, duration);
            if (possibleAppointments.Count == 0)
                throw new Exception("There are not free appointments for given parameters!");
            List<PossibleAppointmentsDTO> retValue = new List<PossibleAppointmentsDTO>();
            Room selectedRoom = RoomRepository.FindOneById(roomId);
            foreach (var pa in possibleAppointments)
            {

                if (pa > DateTime.Now.AddHours(1))
                {
                    PossibleAppointmentsDTO possibleAppointmentsDTO = new PossibleAppointmentsDTO("", "", "", "", "", roomId, selectedRoom.Name, pa, duration, -1);
                    retValue.Add(possibleAppointmentsDTO);
                }
            }
            return retValue;
        }

        public PossibleAppointmentsDTO GetPossibleAppointmentsForAbsence(String doctorJmbg,
            DateTime dateFrom, DateTime dateUntil, int duration)
        {
            ValidateInputParametersForAbsence(doctorJmbg, dateFrom, dateUntil);

            PossibleAppointmentsDTO absentRequest;
            List<DateTime> possibleAppointments = new List<DateTime>();
            List<Appointment> doctorAppointments = AppointmentRepository.FindAllByDoctorJmbg(doctorJmbg);
            dateFrom = new DateTime(dateFrom.Year, dateFrom.Month, dateFrom.Day, 0, 0, 0);
            dateUntil = new DateTime(dateUntil.Year, dateUntil.Month, dateUntil.Day, 0, 0, 0);
            DateTime current = dateFrom;

            GetAllAppointmentsForAbsencePeriod(dateUntil, possibleAppointments, current);
            removeOccupiedDatesFromPossibleAppointment(dateFrom, dateUntil, possibleAppointments, doctorAppointments);

            DateTime startDate = possibleAppointments[0];
            int counter = 1;

            IsThereEnoughPossibleAppointments(duration, possibleAppointments, ref startDate, ref counter);
            absentRequest = (counter == duration) ? new PossibleAppointmentsDTO("", "", doctorJmbg, "", "", -1, "", startDate, duration, -1) : null;

            return absentRequest;
        }

        private static void GetAllAppointmentsForAbsencePeriod(DateTime dateUntil, List<DateTime> possibleAppointments, DateTime current)
        {
            while (current != dateUntil.AddDays(1))
            {
                possibleAppointments.Add(current);
                current = current.AddDays(1);
            }
        }

        private static void IsThereEnoughPossibleAppointments(int duration, List<DateTime> possibleAppointments, ref DateTime startDate, ref int counter)
        {
            for (int i = 1; i < possibleAppointments.Count - 1; i++)
            {
                if (counter == duration)
                    break;
                else if (possibleAppointments[i].Date.AddDays(1) == possibleAppointments[i + 1])
                    counter++;
                else
                {
                    counter = 1;
                    i++;
                    startDate = possibleAppointments[i];
                }
            }
        }

        private static void removeOccupiedDatesFromPossibleAppointment(DateTime dateFrom, DateTime dateUntil, List<DateTime> possibleAppointments, List<Appointment> doctorAppointments)
        {
            foreach (var da in doctorAppointments)
            {
                if (da.StartTime >= dateFrom && da.StartTime <= dateUntil)
                {
                    DateTime whatDateToRemove = possibleAppointments[0];
                    whatDateToRemove = FindDateToRemove(possibleAppointments, da, whatDateToRemove);
                    possibleAppointments.Remove(whatDateToRemove);
                }
            }
        }

        private static DateTime FindDateToRemove(List<DateTime> possibleAppointments, Appointment da, DateTime whatDateToRemove)
        {
            foreach (var pa in possibleAppointments)
            {
                if (pa.Date == da.StartTime.Date)
                {
                    whatDateToRemove = pa;
                    break;
                }
            }

            return whatDateToRemove;
        }

        private void ValidateInputParametersForAbsence(string doctorJmbg, DateTime dateFrom, DateTime dateUntil)
        {
            if (doctorJmbg == null || DoctorRepository.FindOneByJmbg(doctorJmbg) == null)
                throw new Exception("Doctor with that JMBG doesn't exist!");
            else if (dateFrom > dateUntil)
                throw new Exception("Dates are not valid!");
            else if((DateTime.Today.AddDays(2) > dateFrom))
                throw new Exception("Choosen date must be al least 2 days erlier!");
        }

        public List<PossibleAppointmentsDTO> GetPossibleAppointmentsByDoctor(String patientJmbg, String doctorJmbg,
            DateTime dateFrom, DateTime dateUntil, int duration, String priority)
        {
            ValidateInputParametersForDoctorAppointment(patientJmbg, doctorJmbg, dateFrom, dateUntil);

            List<PossibleAppointmentsDTO> possibleAppointmentsDTOs = new List<PossibleAppointmentsDTO>();
            List<DateTime> possibleAppointments = new List<DateTime>();
            Doctor sentDoctor = DoctorRepository.FindOneByJmbg(doctorJmbg);
            List<Doctor> doctorsNeeded = DoctorRepository.FindAllBySpeciality(sentDoctor.SpecialtyType);
            int roomId = sentDoctor.RoomId;

            if (priority == "doctor")
                possibleAppointments = FindPossibleAppointmentsForDoctorPriority(patientJmbg, doctorJmbg, ref dateFrom, ref dateUntil, duration, roomId);
            else
                possibleAppointments = FindPossibleAppointmentsForTimePriority(patientJmbg, ref doctorJmbg, dateFrom, dateUntil, duration, doctorsNeeded, ref roomId);

            Patient selectedPatient = PatientRepository.FindOneByJmbg(patientJmbg);
            Doctor selectedDoctor = DoctorRepository.FindOneByJmbg(doctorJmbg);
            Room selectedRoom = RoomRepository.FindOneById(roomId);

            GetAllPossibleAppointmentsDTOByDoctor(patientJmbg, doctorJmbg, duration, possibleAppointmentsDTOs, possibleAppointments, sentDoctor, roomId, selectedPatient, selectedDoctor, selectedRoom);
            
            return possibleAppointmentsDTOs;
        }

        private static void GetAllPossibleAppointmentsDTOByDoctor(string patientJmbg, string doctorJmbg, int duration, List<PossibleAppointmentsDTO> possibleAppointmentsDTOs, List<DateTime> possibleAppointments, Doctor sentDoctor, int roomId, Patient selectedPatient, Doctor selectedDoctor, Room selectedRoom)
        {
            foreach (var pa in possibleAppointments)
            {
                if (pa > DateTime.Now.AddHours(1))
                {
                    PossibleAppointmentsDTO possibleAppointmentsDTO = new PossibleAppointmentsDTO(patientJmbg, selectedPatient.FirstName + " " + selectedPatient.LastName, doctorJmbg,
                       selectedDoctor.FirstName + " " + selectedDoctor.LastName, sentDoctor.SpecialtyType, roomId, selectedRoom.Name, pa, duration, -1);
                    possibleAppointmentsDTOs.Add(possibleAppointmentsDTO);
                }
            }
        }

        private List<DateTime> FindPossibleAppointmentsForTimePriority(string patientJmbg, ref string doctorJmbg, DateTime dateFrom, DateTime dateUntil, int duration, List<Doctor> doctorsNeeded, ref int roomId)
        {
            List<DateTime> possibleAppointments = findPossibleStartTimesOfAppointment(patientJmbg, doctorJmbg, roomId,
                                dateFrom, dateUntil, duration);
            while (possibleAppointments.Count == 0)
            {
                foreach (var doc in doctorsNeeded)
                {
                    doctorJmbg = doc.Jmbg;
                    roomId = doc.RoomId;
                    possibleAppointments = findPossibleStartTimesOfAppointment(patientJmbg, doctorJmbg, roomId,
                    dateFrom, dateUntil, duration);
                    if (possibleAppointments.Count != 0)
                        break;
                }
                if (possibleAppointments.Count == 0)
                    throw new Exception("There are not free appointments for given parameters!");
            }

            return possibleAppointments;
        }

        private List<DateTime> FindPossibleAppointmentsForDoctorPriority(string patientJmbg, string doctorJmbg, ref DateTime dateFrom, ref DateTime dateUntil, int duration, int roomId)
        {
            List<DateTime> possibleAppointments = findPossibleStartTimesOfAppointment(patientJmbg, doctorJmbg, roomId,
                                dateFrom, dateUntil, duration);
            while (possibleAppointments.Count == 0)
            {
                dateFrom = dateUntil;
                dateUntil = dateUntil.AddDays(5);
                possibleAppointments = findPossibleStartTimesOfAppointment(patientJmbg, doctorJmbg, roomId,
                dateFrom, dateUntil, duration);
            }

            return possibleAppointments;
        }

        private void ValidateInputParametersForDoctorAppointment(string patientJmbg, string doctorJmbg, DateTime dateFrom, DateTime dateUntil)
        {
            if (patientJmbg == null || PatientRepository.FindOneByJmbg(patientJmbg) == null)
                throw new Exception("Patient with that JMBG doesn't exist!");
            else if (doctorJmbg == null || DoctorRepository.FindOneByJmbg(doctorJmbg) == null)
                throw new Exception("Doctor with that JMBG doesn't exist!");
            else if (dateFrom > dateUntil)
                throw new Exception("Dates are not valid!");
            else if (dateFrom < DateTime.Now)
                throw new Exception("Can't create appointment in the past!");
        }

        public void CreateAppointmentByDoctor(PossibleAppointmentsDTO appointmentToCreate)
        {
            int id = GenerateNewId();
            Appointment appointment = new Appointment(appointmentToCreate.StartTime, appointmentToCreate.Duration, id, appointmentToCreate.PatientJmbg, appointmentToCreate.DoctorJmbg, appointmentToCreate.RoomId);
            
            if (!appointment.validateAppointment())
                throw new Exception("Something went wrong, new appointment isn't created!");
            AppointmentRepository.SaveAppointment(appointment);
            
        }
        public void CreateOperationAppointment(PossibleAppointmentsDTO appointmentToCreate)
        {
            Boolean specialty = DoctorRepository.FindOneByJmbg(appointmentToCreate.DoctorJmbg).Specialty;
            if (!specialty)
                throw new Exception("Only doctors with specialization can perform operation!");

            if (appointmentToCreate.DoctorJmbg.Equals("1231231231231")) //hard codovan ulogovan doktor, jer operaciju moze samo kod sebe da zakaze
                CreateAppointmentByDoctor(appointmentToCreate);

        }
        private void ValidateParametersForScheduleEmergency (String patientJmbg, String doctorSpeciality)
        {
            if (patientJmbg == null || PatientRepository.FindOneByJmbg(patientJmbg) == null)
                throw new Exception("Patient with that JMBG doesn't exist!");
            else if (doctorSpeciality == null || DoctorRepository.FindAllBySpeciality(doctorSpeciality) == null)
                throw new Exception("There are no doctors with that speciality!");
            else
                return;
        }
        private Boolean ValidateAppointmentTimeForScheduleEmergency(DateTime startTime, int duration)
        {
            if ((startTime > DateTime.Now && startTime < DateTime.Now.AddMinutes(75)) ||
                    (startTime.AddMinutes(duration) > DateTime.Now &&
                    startTime.AddMinutes(duration) < DateTime.Now.AddMinutes(75)) ||
                    (startTime < DateTime.Now && startTime.AddMinutes(duration) > DateTime.Now.AddMinutes(75)))
                return true;
            return false;
        }
        private Boolean IsOccupied(List<Appointment> appointmentList){
            foreach (var appointment in appointmentList)
            {
                if (ValidateAppointmentTimeForScheduleEmergency(appointment.StartTime, appointment.Duration))
                {
                    return true;
                }
            }
            return false;
        }

        private Boolean IsRoomOccupiedByBasicRenovation(Room room)
        {
            List<BasicRenovation> roomBasicRenovations = BasicRenovationRepository.FindAllByRoomId(room.Id);
            foreach (var roomBasicRenovation in roomBasicRenovations)
            {
                if (ValidateAppointmentTimeForScheduleEmergency(roomBasicRenovation.StartTime, roomBasicRenovation.Duration))
                {
                    return true;
                }
            }
            return false; ;
        }

        private PossibleAppointmentsDTO getPossibleEmergencyAppointment(List<Appointment> doctorAppointments, Patient patient, Doctor doctor)
        {
            if (!IsOccupied(doctorAppointments))
            {
                List<Room> rooms = RoomRepository.FindAll();
                foreach (var room in rooms)
                {
                    List<Appointment> roomAppointments = AppointmentRepository.FindAllByRoomId(room.Id);
                    if (!IsOccupied(roomAppointments) && !IsRoomOccupiedByBasicRenovation(room))
                    {
                        return new PossibleAppointmentsDTO(patient.Jmbg, patient.FirstName + " " + patient.LastName, doctor.Jmbg,
                    doctor.FirstName + " " + doctor.LastName, doctor.SpecialtyType, room.Id, room.Name, DateTime.Now.AddMinutes(5), 60, 0);
                    }
                }
            }
            return null;
        }

        public PossibleAppointmentsDTO ScheduleEmergencyAppointment(String patientJmbg, String doctorSpeciality)
        {
            ValidateParametersForScheduleEmergency(patientJmbg, doctorSpeciality);
            Patient patient = PatientRepository.FindOneByJmbg(patientJmbg);
            List<Doctor> doctors = DoctorRepository.FindAllBySpeciality(doctorSpeciality);
            foreach (var doctor in doctors)
            {
                List<Appointment> doctorAppointments = AppointmentRepository.FindAllByDoctorJmbg(doctor.Jmbg);
                PossibleAppointmentsDTO possibleEmergencyAppointment = getPossibleEmergencyAppointment(doctorAppointments, patient, doctor);
                if (possibleEmergencyAppointment != null)
                    return possibleEmergencyAppointment;
            }
            return null;
        }

        public List<ModifyAppointmentForEmergencyDto> RescheduleAppointmentsForEmergency(String patientJmbg, String doctorSpeciality)
        {
            ValidateParametersForScheduleEmergency(patientJmbg, doctorSpeciality);
            Patient patient = PatientRepository.FindOneByJmbg(patientJmbg);
            List<Doctor> doctors = DoctorRepository.FindAllBySpeciality(doctorSpeciality);
            List<ModifyAppointmentForEmergencyDto> appointmentsToReschedule = new List<ModifyAppointmentForEmergencyDto>();
            foreach (var doctor in doctors)
            {
                List<Appointment> doctorAppointments = AppointmentRepository.FindAllByDoctorJmbg(doctor.Jmbg);
                appointmentsToReschedule.AddRange(getPossibleAppointmentsToReschedule(doctorAppointments, doctor, patientJmbg));
            }
            if (appointmentsToReschedule != null)
                appointmentsToReschedule.Sort((x, y) => DateTime.Compare(x.NewStartTime, y.NewStartTime));
            return appointmentsToReschedule;
        }

        private List<ModifyAppointmentForEmergencyDto> getPossibleAppointmentsToReschedule(List<Appointment> doctorAppointments, Doctor doctor, String patientJmbg)
        {
            List<ModifyAppointmentForEmergencyDto> appointmentsToReschedule = new List<ModifyAppointmentForEmergencyDto>();
            foreach (var appointment in doctorAppointments)
            {
                if (appointment.StartTime > DateTime.Now && appointment.StartTime < DateTime.Now.AddMinutes(75))
                {
                    Room room = RoomRepository.FindOneById(appointment.RoomId);
                    Patient patientInOldAppointment = PatientRepository.FindOneByJmbg(appointment.PatientJmbg);
                    List<PossibleAppointmentsDTO> newPossibleAppointments = GetPossibleAppointmentsBySecretary(patientJmbg, doctor.Jmbg,
                        room.Id, DateTime.Now.AddHours(3), DateTime.Now.AddDays(5), appointment.Duration, "doctor");
                    newPossibleAppointments.Sort((x, y) => DateTime.Compare(x.StartTime, y.StartTime));
                    appointmentsToReschedule.Add(new ModifyAppointmentForEmergencyDto(appointment.PatientJmbg, patientInOldAppointment.FirstName + " " + patientInOldAppointment.LastName, doctor.Jmbg,
                    doctor.FirstName + " " + doctor.LastName, doctor.SpecialtyType, room.Id, room.Name, appointment.StartTime,
                    newPossibleAppointments[0].StartTime, appointment.Duration, appointment.Id));
                    break;
                }
            }
            return appointmentsToReschedule;
        }

        public List<PossibleAppointmentsDTO> GetPossibleAppointmentsBySecretary(String patientJmbg, String doctorJmbg, int roomId,
            DateTime dateFrom, DateTime dateUntil, int duration, String priority)
        {
            if (patientJmbg == null || PatientRepository.FindOneByJmbg(patientJmbg) == null)
                throw new Exception("Patient with that JMBG doesn't exist!");
            else if (doctorJmbg == null || DoctorRepository.FindOneByJmbg(doctorJmbg) == null)
                throw new Exception("Doctor with that JMBG doesn't exist!");
            else if (RoomRepository.FindOneById(roomId) == null)
                throw new Exception("Room with that id doesn't exist!");
            else if (dateFrom > dateUntil)
                throw new Exception("Dates are not valid!");
            List<DateTime> possibleAppointments = new List<DateTime>();
            Doctor sentDoctor = DoctorRepository.FindOneByJmbg(doctorJmbg);
            List<Doctor> doctorsNeeded = DoctorRepository.FindAllBySpeciality(sentDoctor.SpecialtyType);
            doctorsNeeded.Remove(sentDoctor);
            if (priority == "doctor")
            {
                possibleAppointments = findPossibleStartTimesOfAppointment(patientJmbg, doctorJmbg, roomId,
                    dateFrom, dateUntil, duration);
                while (possibleAppointments.Count == 0)
                {
                    dateFrom = dateUntil;
                    dateUntil = dateUntil.AddDays(5);
                    possibleAppointments = findPossibleStartTimesOfAppointment(patientJmbg, doctorJmbg, roomId,
                    dateFrom, dateUntil, duration);
                }
            }
            else
            {
                possibleAppointments = findPossibleStartTimesOfAppointment(patientJmbg, doctorJmbg, roomId,
                    dateFrom, dateUntil, duration);
                while (possibleAppointments.Count == 0)
                {
                    foreach (var doc in doctorsNeeded)
                    {
                        doctorJmbg = doc.Jmbg;
                        if (roomId == -1)
                            roomId = doc.RoomId;
                        possibleAppointments = findPossibleStartTimesOfAppointment(patientJmbg, doctorJmbg, roomId,
                        dateFrom, dateUntil, duration);
                        if (possibleAppointments.Count != 0)
                            break;
                    }
                    if (possibleAppointments.Count == 0)
                        throw new Exception("There are not free appointments for given parameters!");
                }
            }
            List<PossibleAppointmentsDTO> retValue = new List<PossibleAppointmentsDTO>();
            Patient selectedPatient = PatientRepository.FindOneByJmbg(patientJmbg);
            Doctor selectedDoctor = DoctorRepository.FindOneByJmbg(doctorJmbg);
            Room selectedRoom = RoomRepository.FindOneById(roomId);
            foreach (var pa in possibleAppointments)
            {
                if (pa > DateTime.Now.AddHours(1))
                {
                    PossibleAppointmentsDTO possibleAppointmentsDTO = new PossibleAppointmentsDTO(patientJmbg, selectedPatient.FirstName + " " + selectedPatient.LastName, doctorJmbg,
                       selectedDoctor.FirstName + " " + selectedDoctor.LastName, sentDoctor.SpecialtyType, roomId, selectedRoom.Name, pa, duration, -1);
                    retValue.Add(possibleAppointmentsDTO);
                }
            }
            return retValue;
        }

        public List<DateTime> findPossibleStartTimesOfAppointment(String patientJmbg, String doctorJmbg, int roomId, DateTime dateFrom,
            DateTime dateUntil, int duration)
        {
            List<Appointment> neededAppointments = findAppointmentsToWatch(patientJmbg, doctorJmbg, roomId, dateFrom, dateUntil);
            List<(DateTime, DateTime)> freePeriod = new List<(DateTime, DateTime)>();
            List<(DateTime, DateTime)> newFreePeriod = new List<(DateTime, DateTime)>();
            List<DateTime> possibleAppointments = new List<DateTime>();
            if (neededAppointments.Count == 0)
            {
                var firstDate = dateFrom;
                if (duration > 900)
                {
                    while (freePeriod.Count < 3)
                    {
                        freePeriod.Add((firstDate.AddHours(8).AddMinutes(15), firstDate.AddHours(8).AddMinutes(duration + 15)));
                        firstDate = firstDate.AddHours(8).AddMinutes(duration + 15);
                    }
                }
                else
                {
                    while (firstDate < dateUntil && firstDate < dateFrom.AddDays(5))
                    {
                        freePeriod.Add((firstDate.AddHours(8).AddMinutes(15), firstDate.AddDays(1).AddMinutes(-15)));
                        firstDate = firstDate.AddDays(1);
                    }
                }
            }
            else
                freePeriod = findFreePeriods(neededAppointments, dateFrom, dateUntil, duration);

            for (int i = 0; i < freePeriod.Count; i++)
            {
                if (freePeriod[i].Item1.AddHours(9) < freePeriod[i].Item2 && duration < 540)
                {
                    newFreePeriod.AddRange(splitPeriod(freePeriod[i]));
                }
                else
                    newFreePeriod.Add(freePeriod[i]);

            }
            if (freePeriod.Count > 0)
            {
                possibleAppointments = findFreeAppointments(newFreePeriod, duration);
            }
            return possibleAppointments;
        }

        private List<Appointment> findAppointmentsToWatch(String patientJmbg, String doctorJmbg, int? roomId, DateTime dateFrom,
            DateTime dateUntil)
        {
            List<Appointment> allAppointments = this.GetAllAppointments();
            List<BasicRenovation> allBasicRenovations = BasicRenovationRepository.FindAll();
            List<AdvancedRenovationJoining> advancedRenovationJoinings = AdvancedRenovationJoiningRepository.FindAll();
            List<AdvancedRenovationSeparation> advancedRenovationSeparations = AdvancedRenovationSeparationRepository.FindAll();
            List<Appointment> neededAppointments = new List<Appointment>();
            foreach (var app in allAppointments)
            {
                if (app.StartTime > DateTime.Now && app.StartTime.AddMinutes(app.Duration) > dateFrom && app.StartTime < dateUntil && (
                    (doctorJmbg != null && app.DoctorJmbg == doctorJmbg) ||
                    (patientJmbg != null && app.PatientJmbg == patientJmbg) || (roomId != null && app.RoomId == roomId)))
                    neededAppointments.Add(app);
            }
            foreach (var br in allBasicRenovations)
            {
                if (br.StartTime > DateTime.Now && br.StartTime.AddMinutes(br.Duration) > dateFrom && br.StartTime < dateUntil && ((roomId != null && br.RoomId == roomId)))
                {
                    Appointment basicRenovationAppointment = new Appointment(br.StartTime, br.Duration, -1, "", "", br.RoomId);
                    neededAppointments.Add(basicRenovationAppointment);
                }
            }
            foreach (var ars in advancedRenovationSeparations)
            {
                if (ars.StartTime > DateTime.Now && ars.StartTime.AddMinutes(ars.Duration) > dateFrom && ars.StartTime < dateUntil && ((roomId != null && ars.StartRoomId == roomId)))
                {
                    Appointment advancedRenovationAppointment = new Appointment(ars.StartTime, ars.Duration, -1, "", "", ars.StartRoomId);
                    neededAppointments.Add(advancedRenovationAppointment);
                }
            }
            foreach (var ars in advancedRenovationJoinings)
            {
                if (ars.StartTime > DateTime.Now && ars.StartTime.AddMinutes(ars.Duration) > dateFrom && ars.StartTime < dateUntil && ((roomId != null && (ars.FirstStartRoom == roomId || ars.SecondStartRoom == roomId))))
                {
                    int whatRoomId = -1;
                    if (ars.FirstStartRoom == roomId)
                        whatRoomId = ars.FirstStartRoom;
                    else
                        whatRoomId = ars.SecondStartRoom;
                    Appointment advancedRenovationAppointment = new Appointment(ars.StartTime, ars.Duration, -1, "", "", whatRoomId);
                    neededAppointments.Add(advancedRenovationAppointment);
                }
            }
            neededAppointments.Sort((x, y) => DateTime.Compare(x.StartTime.AddMinutes(x.Duration), y.StartTime.AddMinutes(y.Duration)));
            return neededAppointments;
        }

        private List<(DateTime, DateTime)> findFreePeriods(List<Appointment> neededAppointments, DateTime dateFrom, DateTime dateUntil, int duration)
        {
            List<(DateTime, DateTime)> freePeriod = new List<(DateTime, DateTime)>();
            if (neededAppointments[0].StartTime > dateFrom.AddMinutes(duration + 15))
            {
                freePeriod.Add((dateFrom.AddMinutes(15), neededAppointments[0].StartTime.AddMinutes(-15)));
            }
            for (int i = 0; i < neededAppointments.Count - 1; i++)
            {
                if (neededAppointments[i].StartTime.AddMinutes(neededAppointments[i].Duration + 15) < neededAppointments[i + 1].StartTime)
                {
                    (DateTime, DateTime) newPeriod = (neededAppointments[i].StartTime.AddMinutes(neededAppointments[i].Duration + 15),
                        neededAppointments[i + 1].StartTime.AddMinutes(-15));
                    freePeriod.Add(newPeriod);
                }

            }
            if (neededAppointments.Last().StartTime.AddMinutes(neededAppointments.Last().Duration) < dateUntil.AddMinutes(-duration - 15))
            {
                freePeriod.Add((neededAppointments.Last().StartTime.AddMinutes(neededAppointments.Last().Duration + 15), dateUntil.AddMinutes(-15)));
            }
            return freePeriod;
        }

        private List<(DateTime, DateTime)> splitPeriod((DateTime, DateTime) period)
        {
            List<(DateTime, DateTime)> retValue = new List<(DateTime, DateTime)>();
            while (period.Item1.AddHours(9) < period.Item2)
            {
                retValue.Add((period.Item1, period.Item1.AddHours(8)));
                period = (period.Item1.AddHours(8).AddMinutes(15), period.Item2);
            }
            retValue.Add((period.Item1, period.Item2));
            return retValue;
        }

        private List<DateTime> findFreeAppointments(List<(DateTime, DateTime)> newFreePeriod, int duration)
        {
            List<DateTime> possibleAppointments = new List<DateTime>();
            int counter = 0;
            while (counter < 10)
            {
                Boolean change = false;
                for (int i = 0; i < newFreePeriod.Count; i++)
                {
                    if (counter == 10)
                        break;
                    if (newFreePeriod[i].Item1.AddMinutes(duration) <= newFreePeriod[i].Item2)
                    {
                        possibleAppointments.Add(newFreePeriod[i].Item1);
                        newFreePeriod[i] = (newFreePeriod[i].Item1.AddMinutes(duration + 15), newFreePeriod[i].Item2);
                        counter++;
                        change = true;
                    }
                }
                if (!change)
                    break;
            }
            possibleAppointments.Sort((x, y) => DateTime.Compare(x, y));
            return possibleAppointments;
        }
    }
}