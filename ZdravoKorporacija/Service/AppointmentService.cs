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
        readonly ManagerRepository ManagerRepository = new ManagerRepository();
        private readonly SecretaryRepository SecretaryRepository = new SecretaryRepository();
        readonly RoomRepository RoomRepository = new RoomRepository();
        readonly BasicRenovationRepository BasicRenovationRepository = new BasicRenovationRepository();
        readonly AdvancedRenovationJoiningRepository AdvancedRenovationJoiningRepository = new AdvancedRenovationJoiningRepository();
        readonly AdvancedRenovationSeparationRepository AdvancedRenovationSeparationRepository = new AdvancedRenovationSeparationRepository();
        private readonly MeetingRepository MeetingRepository = new MeetingRepository();
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

        public List<PossibleAppointmentsDTO> GetAllPastAppointmentsByPatient()
        {
            List<Appointment> appointments = AppointmentRepository.GetAllPastAppointmentsByPatient(App.loggedUser.Jmbg);
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
            List<String> doctorJmbgs = new List<String>();
            doctorJmbgs.Add("");
            possibleAppointmentsForFirstRoom = findPossibleStartTimesOfAppointment("", doctorJmbgs, firstRoomId,
                    dateFrom, dateUntil, duration);
            possibleAppointmentsForSecondRoom = findPossibleStartTimesOfAppointment("", doctorJmbgs, secondRoomId,
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
            List<String> doctorJmbgs = new List<String>();
            doctorJmbgs.Add("");
            possibleAppointments = findPossibleStartTimesOfAppointment("", doctorJmbgs, roomId,
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
            List<String> doctorJmbgs = new List<String>();
            doctorJmbgs.Add(doctorJmbg);
            if (priority == "doctor")
            {
                possibleAppointments = findPossibleAppointmentsForDoctorPriority(patientJmbg, doctorJmbgs, roomId, dateFrom, dateUntil, duration);
            }
            else
            {
                possibleAppointments = findPossibleAppointmentsForTimePriority(patientJmbg, doctorJmbgs, ref roomId, dateFrom, dateUntil, duration);
            }
            
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

        public List<PossibleMeetingDTO> GetPossibleMeetingAppointments(List<String> userJmbgs, int roomId,
            DateTime dateFrom, DateTime dateUntil, int duration)
        {
            ValidateParametersForScheduleMeeting(userJmbgs, roomId, dateFrom, dateUntil);
            List<DateTime> possibleAppointments = new List<DateTime>();
            possibleAppointments =
                findPossibleStartTimesOfAppointment("", userJmbgs, roomId, dateFrom, dateUntil, duration);
            var possibleMeetingDtos = createPossibleMeetingsDtos(userJmbgs, roomId, duration, possibleAppointments);
            return possibleMeetingDtos;
        }

        private List<PossibleMeetingDTO> createPossibleMeetingsDtos(List<String> userJmbgs, int roomId, int duration,
            List<DateTime> possibleAppointments)
        {
            List<PossibleMeetingDTO> possibleMeetingDtos = new List<PossibleMeetingDTO>();
            Room selectedRoom = RoomRepository.FindOneById(roomId);
            foreach (var pa in possibleAppointments)
            {
                if (pa > DateTime.Now.AddHours(1))
                {
                    PossibleMeetingDTO possibleMeetingDto = new PossibleMeetingDTO();
                    possibleMeetingDto.Duration = duration;
                    possibleMeetingDto.StartTime = pa;
                    possibleMeetingDto.RoomName = selectedRoom.Name;
                    possibleMeetingDto.RoomId = selectedRoom.Id;
                    foreach (var userJmbg in userJmbgs)
                    {
                        User user = DoctorRepository.FindOneByJmbg(userJmbg);
                        if (user == null)
                            user = ManagerRepository.FindOneByJmbg(userJmbg);
                        if (user == null)
                            user = SecretaryRepository.FindOneByJmbg(userJmbg);
                        if (user == null)
                            throw new Exception("Something went wrong!");
                        possibleMeetingDto.UserJmbgs.Add(userJmbg);
                        possibleMeetingDto.UserFullNames.Add(user.FirstName + " " + user.LastName);
                    }

                    possibleMeetingDtos.Add(possibleMeetingDto);
                }
            }
            return possibleMeetingDtos;
        }

        private void ValidateParametersForScheduleMeeting(List<String> userJmbgs, int roomId, DateTime dateFrom, DateTime dateUntil)
        {
            foreach (var userJmbg in userJmbgs)
            {
                if (userJmbg == null || (DoctorRepository.FindOneByJmbg(userJmbg) == null && ManagerRepository.FindOneByJmbg(userJmbg) == null
                    && SecretaryRepository.FindOneByJmbg(userJmbg) == null))
                    throw new Exception("One of the users doesn't exist!");
            }
            if (RoomRepository.FindOneById(roomId) == null)
                throw new Exception("Room with that id doesn't exist!");
            else if (dateFrom > dateUntil)
                throw new Exception("Dates are not valid!");
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

        private PossibleAppointmentsDTO getPossibleEmergencyAppointment(Patient patient, Doctor doctor)
        {
            List<Appointment> doctorAppointments = AppointmentRepository.FindAllByDoctorJmbg(doctor.Jmbg);
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

        public PossibleAppointmentsDTO FindPossibleEmergencyAppointment(String patientJmbg, String doctorSpeciality)
        {
            ValidateParametersForScheduleEmergency(patientJmbg, doctorSpeciality);
            Patient patient = PatientRepository.FindOneByJmbg(patientJmbg);
            List<Doctor> doctors = DoctorRepository.FindAllBySpeciality(doctorSpeciality);
            foreach (var doctor in doctors)
            {
                PossibleAppointmentsDTO possibleEmergencyAppointment = getPossibleEmergencyAppointment(patient, doctor);
                if (possibleEmergencyAppointment != null)
                    return possibleEmergencyAppointment;
            }
            return null;
        }

        public List<ModifyAppointmentForEmergencyDto> FindAppointmentsToRescheduleForEmergency(String patientJmbg, String doctorSpeciality)
        {
            ValidateParametersForScheduleEmergency(patientJmbg, doctorSpeciality);
            Patient patient = PatientRepository.FindOneByJmbg(patientJmbg);
            List<Doctor> doctors = DoctorRepository.FindAllBySpeciality(doctorSpeciality);
            List<ModifyAppointmentForEmergencyDto> appointmentsToReschedule = new List<ModifyAppointmentForEmergencyDto>();
            foreach (var doctor in doctors)
            {
                appointmentsToReschedule.AddRange(getPossibleAppointmentsToReschedule(doctor, patientJmbg));
            }
            if (appointmentsToReschedule != null)
                appointmentsToReschedule.Sort((x, y) => DateTime.Compare(x.NewStartTime, y.NewStartTime));
            return appointmentsToReschedule;
        }

        private List<ModifyAppointmentForEmergencyDto> getPossibleAppointmentsToReschedule(Doctor doctor, String patientJmbg)
        {
            List<Appointment> doctorAppointments = AppointmentRepository.FindAllByDoctorJmbg(doctor.Jmbg);
            List<ModifyAppointmentForEmergencyDto> appointmentsToReschedule = new List<ModifyAppointmentForEmergencyDto>();
            foreach (var appointment in doctorAppointments)
            {
                if (checkAppointmentTimeForReschedule(appointment.StartTime))
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

        private Boolean checkAppointmentTimeForReschedule(DateTime appointmentStartTime)
        {
            if (appointmentStartTime > DateTime.Now && appointmentStartTime < DateTime.Now.AddMinutes(75))
            {
                return true;
            }

            return false;
        }

        public List<PossibleAppointmentsDTO> GetPossibleAppointmentsBySecretary(String patientJmbg, String doctorJmbg, int roomId,
            DateTime dateFrom, DateTime dateUntil, int duration, String priority)
        {
            validateInputParametersForGetPossibleAppointmentsBySecretary(patientJmbg, doctorJmbg, roomId, dateFrom, dateUntil);
            List<DateTime> possibleAppointments = new List<DateTime>();
            List<String> doctorJmbgs = new List<String>();
            doctorJmbgs.Add(doctorJmbg);
            if (priority == "doctor")
            {
                possibleAppointments = findPossibleAppointmentsForDoctorPriority(patientJmbg, doctorJmbgs, roomId, dateFrom, dateUntil, duration);
            }
            else
            {
                possibleAppointments = findPossibleAppointmentsForTimePriority(patientJmbg, doctorJmbgs, ref roomId, dateFrom, dateUntil, duration);
            }
            var possibleAppointmentsDtos = createPossibleAppointmentsDtos(patientJmbg, doctorJmbg, roomId, duration, possibleAppointments);
            return possibleAppointmentsDtos;
        }

        private List<PossibleAppointmentsDTO> createPossibleAppointmentsDtos(string patientJmbg, string doctorJmbg, int roomId, int duration,
            List<DateTime> possibleAppointments)
        {
            List<PossibleAppointmentsDTO> possibleAppointmentsDtos = new List<PossibleAppointmentsDTO>();
            Patient selectedPatient = PatientRepository.FindOneByJmbg(patientJmbg);
            Doctor selectedDoctor = DoctorRepository.FindOneByJmbg(doctorJmbg);
            Room selectedRoom = RoomRepository.FindOneById(roomId);
            foreach (var pa in possibleAppointments)
            {
                if (pa > DateTime.Now.AddHours(1))
                {
                    PossibleAppointmentsDTO possibleAppointmentsDTO = new PossibleAppointmentsDTO(patientJmbg,
                        selectedPatient.FirstName + " " + selectedPatient.LastName, doctorJmbg,
                        selectedDoctor.FirstName + " " + selectedDoctor.LastName, selectedDoctor.SpecialtyType, roomId,
                        selectedRoom.Name, pa, duration, -1);
                    possibleAppointmentsDtos.Add(possibleAppointmentsDTO);
                }
            }
            return possibleAppointmentsDtos;
        }

        private List<DateTime> findPossibleAppointmentsForTimePriority(string patientJmbg, List<String> doctorJmbgs, ref int roomId,
            DateTime dateFrom, DateTime dateUntil, int duration)
        {
            List<DateTime> possibleAppointments;
            Doctor sentDoctor = DoctorRepository.FindOneByJmbg(doctorJmbgs[0]);
            List<Doctor> doctorsNeeded = DoctorRepository.FindAllBySpeciality(sentDoctor.SpecialtyType);
            doctorsNeeded.Remove(sentDoctor);
            possibleAppointments = findPossibleStartTimesOfAppointment(patientJmbg, doctorJmbgs, roomId,
                dateFrom, dateUntil, duration);
            if (possibleAppointments.Count > 0) return possibleAppointments;
            foreach (var doc in doctorsNeeded)
            {
                doctorJmbgs[0] = doc.Jmbg;
                roomId = doc.RoomId;
                possibleAppointments = findPossibleStartTimesOfAppointment(patientJmbg, doctorJmbgs, roomId, dateFrom, dateUntil, duration);
                if (possibleAppointments.Count != 0) return possibleAppointments;
            }
            throw new Exception("There are not free appointments for given parameters!");
        }

        private List<DateTime> findPossibleAppointmentsForDoctorPriority(string patientJmbg, List<String> doctorJmbgs, int roomId,
            DateTime dateFrom, DateTime dateUntil, int duration)
        {
            List<DateTime> possibleAppointments;
            possibleAppointments = findPossibleStartTimesOfAppointment(patientJmbg, doctorJmbgs, roomId,
                dateFrom, dateUntil, duration);
            while (possibleAppointments.Count == 0)
            {
                dateFrom = dateUntil;
                dateUntil = dateUntil.AddDays(5);
                possibleAppointments = findPossibleStartTimesOfAppointment(patientJmbg, doctorJmbgs, roomId,
                    dateFrom, dateUntil, duration);
            }

            return possibleAppointments;
        }


        private void validateInputParametersForGetPossibleAppointmentsBySecretary(string patientJmbg, string doctorJmbg,
            int roomId, DateTime dateFrom, DateTime dateUntil)
        {
            if (patientJmbg == null || PatientRepository.FindOneByJmbg(patientJmbg) == null)
                throw new Exception("Patient with that JMBG doesn't exist!");
            else if (doctorJmbg == null || DoctorRepository.FindOneByJmbg(doctorJmbg) == null)
                throw new Exception("Doctor with that JMBG doesn't exist!");
            else if (RoomRepository.FindOneById(roomId) == null)
                throw new Exception("Room with that id doesn't exist!");
            else if (dateFrom > dateUntil)
                throw new Exception("Dates are not valid!");
        }

        public List<DateTime> findPossibleStartTimesOfAppointment(String patientJmbg, List<String> doctorJmbgs, int roomId, DateTime dateFrom,
            DateTime dateUntil, int duration)
        {
            List<Appointment> appointmentsToWatch = findAppointmentsToWatch(patientJmbg, doctorJmbgs, roomId, dateFrom, dateUntil);
            List<(DateTime, DateTime)> freePeriods = new List<(DateTime, DateTime)>();
            List<DateTime> possibleAppointments = new List<DateTime>();
            if (appointmentsToWatch.Count == 0)
            {
                freePeriods = addDefaultFreePeriods(dateFrom, dateUntil, duration);
            }
            else
                freePeriods = findFreePeriods(appointmentsToWatch, dateFrom, dateUntil, duration);

            freePeriods = splitFreePeriods(duration, freePeriods);
            if (freePeriods.Count > 0)
            {
                possibleAppointments = findStartTimesOfPossibleAppointments(freePeriods, duration);
            }
            return possibleAppointments;
        }

        private List<(DateTime, DateTime)> splitFreePeriods(int duration, List<(DateTime, DateTime)> freePeriods)
        {
            List<(DateTime, DateTime)> newFreePeriods = new List<(DateTime, DateTime)>();
            for (int i = 0; i < freePeriods.Count; i++)
            {
                if (freePeriods[i].Item1.AddHours(9) < freePeriods[i].Item2 && duration < 540)
                {
                    newFreePeriods.AddRange(splitPeriod(freePeriods[i]));
                }
                else
                    newFreePeriods.Add(freePeriods[i]);
            }

            return newFreePeriods;
        }

        private static List<(DateTime, DateTime)> addDefaultFreePeriods(DateTime dateFrom, DateTime dateUntil, int duration)
        {
            List<(DateTime, DateTime)> freePeriods = new List<(DateTime, DateTime)>();
            var firstDate = dateFrom;
            if (duration > 900)
            {
                while (freePeriods.Count < 3)
                {
                    freePeriods.Add((firstDate.AddHours(8).AddMinutes(15), firstDate.AddHours(8).AddMinutes(duration + 15)));
                    firstDate = firstDate.AddHours(8).AddMinutes(duration + 15);
                }
            }
            else
            {
                while (firstDate < dateUntil && firstDate < dateFrom.AddDays(5))
                {
                    freePeriods.Add((firstDate.AddHours(8).AddMinutes(15), firstDate.AddDays(1).AddMinutes(-15)));
                    firstDate = firstDate.AddDays(1);
                }
            }

            return freePeriods;
        }

        private List<Appointment> findAppointmentsToWatch(String patientJmbg, List<String> doctorJmbgs, int? roomId, DateTime dateFrom,
            DateTime dateUntil)
        {
            List<Appointment> appointmentsToWatch = checkAppointmentsToWatch(patientJmbg, doctorJmbgs, roomId, dateFrom, dateUntil);
            appointmentsToWatch.AddRange(checkBasicRenovationsAppointmentsToWatch(roomId, dateFrom, dateUntil));
            appointmentsToWatch.AddRange(checkAdvancedRenovationSeparationsToWatch(roomId, dateFrom, dateUntil));
            appointmentsToWatch.AddRange(checkAdvancedRenovationsJoiningToWatch(roomId, dateFrom, dateUntil));
            appointmentsToWatch.AddRange(checkMeetingsToWatch(doctorJmbgs, roomId, dateFrom, dateUntil));
            appointmentsToWatch.Sort((x, y) => DateTime.Compare(x.StartTime.AddMinutes(x.Duration), y.StartTime.AddMinutes(y.Duration)));
            return appointmentsToWatch;
        }

        private static Boolean checkStartTimeForWhatAppointmentsToWatch(DateTime startTime, DateTime dateFrom, DateTime dateUntil, int duration)
        {
            if (startTime > DateTime.Now && startTime.AddMinutes(duration) > dateFrom && startTime < dateUntil)
            {
                return true;
            }
            return false;
        }

        private List<Appointment> checkAdvancedRenovationsJoiningToWatch(int? roomId, DateTime dateFrom, DateTime dateUntil)
        {
            List<Appointment> appointmentsToWatch = new List<Appointment>();
            List<AdvancedRenovationJoining> advancedRenovationJoinings = AdvancedRenovationJoiningRepository.FindAll();
            foreach (var arj in advancedRenovationJoinings)
            {
                if (checkStartTimeForWhatAppointmentsToWatch(arj.StartTime, dateFrom, dateUntil, arj.Duration) &&
                    ((roomId != null && (arj.FirstStartRoom == roomId || arj.SecondStartRoom == roomId))))
                {
                    int whatRoomId = -1;
                    if (arj.FirstStartRoom == roomId)
                        whatRoomId = arj.FirstStartRoom;
                    else
                        whatRoomId = arj.SecondStartRoom;
                    Appointment advancedRenovationAppointment =
                        new Appointment(arj.StartTime, arj.Duration, -1, "", "", whatRoomId);
                    appointmentsToWatch.Add(advancedRenovationAppointment);
                }
            }

            return appointmentsToWatch;
        }

        private List<Appointment> checkAdvancedRenovationSeparationsToWatch(int? roomId, DateTime dateFrom, DateTime dateUntil)
        {
            List<Appointment> appointmentsToWatch = new List<Appointment>();
            List<AdvancedRenovationSeparation> advancedRenovationSeparations = AdvancedRenovationSeparationRepository.FindAll();
            foreach (var ars in advancedRenovationSeparations)
            {
                if (checkStartTimeForWhatAppointmentsToWatch(ars.StartTime, dateFrom, dateUntil, ars.Duration) &&
                    ((roomId != null && ars.StartRoomId == roomId)))
                {
                    Appointment advancedRenovationAppointment =
                        new Appointment(ars.StartTime, ars.Duration, -1, "", "", ars.StartRoomId);
                    appointmentsToWatch.Add(advancedRenovationAppointment);
                }
            }

            return appointmentsToWatch;
        }

        private List<Appointment> checkBasicRenovationsAppointmentsToWatch(int? roomId, DateTime dateFrom, DateTime dateUntil)
        {
            List<BasicRenovation> allBasicRenovations = BasicRenovationRepository.FindAll();
            List<Appointment> appointmentsToWatch = new List<Appointment>();
            foreach (var br in allBasicRenovations)
            {
                if (checkStartTimeForWhatAppointmentsToWatch(br.StartTime, dateFrom, dateUntil, br.Duration) &&
                    ((roomId != null && br.RoomId == roomId)))
                {
                    Appointment basicRenovationAppointment =
                        new Appointment(br.StartTime, br.Duration, -1, "", "", br.RoomId);
                    appointmentsToWatch.Add(basicRenovationAppointment);
                }
            }

            return appointmentsToWatch;
        }

        private List<Appointment> checkMeetingsToWatch(List<String> userJmbgs, int? roomId, DateTime dateFrom, DateTime dateUntil)
        {
            List<Meeting> allMeetings = MeetingRepository.FindAll();
            List<Appointment> appointmentsToWatch = new List<Appointment>();
            foreach (var am in allMeetings)
            {
                if (checkStartTimeForWhatAppointmentsToWatch(am.StartTime, dateFrom, dateUntil, am.Duration) &&
                    ((roomId != null && am.RoomId == roomId) || checkUsersInMeeting(userJmbgs, am.UserJmbgs)))
                {
                    Appointment basicRenovationAppointment =
                        new Appointment(am.StartTime, am.Duration, -1, "", "", am.RoomId);
                    appointmentsToWatch.Add(basicRenovationAppointment);
                }
            }

            return appointmentsToWatch;
        }

        private Boolean checkUsersInMeeting(List<String> userJmbgs, List<String> usersInMeeting)
        {
            foreach (var uj in userJmbgs)
            {
                if (usersInMeeting.Contains(uj))
                    return true;
            }

            return false;
        }

        private List<Appointment> checkAppointmentsToWatch(string patientJmbg, List<string> doctorJmbgs, int? roomId, DateTime dateFrom,
            DateTime dateUntil)
        {
            List<Appointment> appointmentsToWatch = new List<Appointment>();
            List<Appointment> allAppointments = this.GetAllAppointments();
            foreach (var app in allAppointments)
            {
                foreach (var doctorJmbg in doctorJmbgs)
                {
                    if (checkStartTimeForWhatAppointmentsToWatch(app.StartTime, dateFrom, dateUntil, app.Duration) && (
                            (doctorJmbg != null && app.DoctorJmbg == doctorJmbg) ||
                            (patientJmbg != null && app.PatientJmbg == patientJmbg) ||
                            (roomId != null && app.RoomId == roomId)))
                        appointmentsToWatch.Add(app);
                }
            }
            return appointmentsToWatch;
        }

        private List<(DateTime, DateTime)> findFreePeriods(List<Appointment> appointmentsToWatch, DateTime dateFrom, DateTime dateUntil, int duration)
        {
            List<(DateTime, DateTime)> freePeriods = new List<(DateTime, DateTime)>();
            if (appointmentsToWatch[0].StartTime > dateFrom.AddMinutes(duration + 15))
            {
                freePeriods.Add((dateFrom.AddMinutes(15), appointmentsToWatch[0].StartTime.AddMinutes(-15)));
            }
            for (int i = 0; i < appointmentsToWatch.Count - 1; i++)
            {
                if (appointmentsToWatch[i].StartTime.AddMinutes(appointmentsToWatch[i].Duration + 15) < appointmentsToWatch[i + 1].StartTime)
                {
                    freePeriods.Add((appointmentsToWatch[i].StartTime.AddMinutes(appointmentsToWatch[i].Duration + 15),
                        appointmentsToWatch[i + 1].StartTime.AddMinutes(-15)));
                }

            }
            if (appointmentsToWatch.Last().StartTime.AddMinutes(appointmentsToWatch.Last().Duration) < dateUntil.AddMinutes(-duration - 15))
            {
                freePeriods.Add((appointmentsToWatch.Last().StartTime.AddMinutes(appointmentsToWatch.Last().Duration + 15), dateUntil.AddMinutes(-15)));
            }
            return freePeriods;
        }

        private List<(DateTime, DateTime)> splitPeriod((DateTime, DateTime) period)
        {
            List<(DateTime, DateTime)> splitedPeriods = new List<(DateTime, DateTime)>();
            while (period.Item1.AddHours(9) < period.Item2)
            {
                splitedPeriods.Add((period.Item1, period.Item1.AddHours(8)));
                period = (period.Item1.AddHours(8).AddMinutes(15), period.Item2);
            }
            splitedPeriods.Add((period.Item1, period.Item2));
            return splitedPeriods;
        }

        private List<DateTime> findStartTimesOfPossibleAppointments(List<(DateTime, DateTime)> newFreePeriod, int duration)
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