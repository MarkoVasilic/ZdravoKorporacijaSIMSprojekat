using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;

namespace ZdravoKorporacija.Service
{
    public class ScheduleService
    {
        private readonly AppointmentRepository AppointmentRepository = new AppointmentRepository();
        private readonly PatientRepository PatientRepository = new PatientRepository();
        private readonly DoctorRepository DoctorRepository = new DoctorRepository();
        private readonly ManagerRepository ManagerRepository = new ManagerRepository();
        private readonly SecretaryRepository SecretaryRepository = new SecretaryRepository();
        private readonly RoomRepository RoomRepository = new RoomRepository();
        private readonly BasicRenovationRepository BasicRenovationRepository = new BasicRenovationRepository();
        private readonly AdvancedRenovationJoiningRepository AdvancedRenovationJoiningRepository = new AdvancedRenovationJoiningRepository();
        private readonly AdvancedRenovationSeparationRepository AdvancedRenovationSeparationRepository = new AdvancedRenovationSeparationRepository();
        private readonly MeetingRepository MeetingRepository = new MeetingRepository();

        public ScheduleService() { }

        public List<PossibleMeetingDTO> GetPossibleMeetingAppointments(List<String> userJmbgs, int roomId,
            DateTime dateFrom, DateTime dateUntil, int duration)
        {
            ValidateInputParametersForGetPossibleAppointments("*", userJmbgs, roomId, dateFrom, dateUntil);
            List<DateTime> possibleAppointments = new List<DateTime>();
            possibleAppointments =
                FindPossibleStartTimesOfAppointment("", userJmbgs, roomId, dateFrom, dateUntil, duration);
            var possibleMeetingDtos = CreatePossibleMeetingsDtos(userJmbgs, roomId, duration, possibleAppointments);
            return possibleMeetingDtos;
        }

        private List<PossibleMeetingDTO> CreatePossibleMeetingsDtos(List<String> userJmbgs, int roomId, int duration,
            List<DateTime> possibleAppointments)
        {
            List<PossibleMeetingDTO> possibleMeetingDtos = new List<PossibleMeetingDTO>();
            Room selectedRoom = RoomRepository.FindOneById(roomId);
            foreach (var pa in possibleAppointments)
            {
                if (pa > DateTime.Now.AddHours(1))
                {
                    PossibleMeetingDTO possibleMeetingDto = new PossibleMeetingDTO(new List<string>(),
                        new List<string>(), selectedRoom.Id, selectedRoom.Name, pa, duration);
                    foreach (var userJmbg in userJmbgs)
                    {
                        var user = CheckUserJmbgExistence(userJmbg);
                        possibleMeetingDto.UserJmbgs.Add(userJmbg);
                        possibleMeetingDto.UserFullNames.Add(user.FirstName + " " + user.LastName);
                    }

                    possibleMeetingDtos.Add(possibleMeetingDto);
                }
            }
            return possibleMeetingDtos;
        }

        private User? CheckUserJmbgExistence(string userJmbg)
        {
            User user = DoctorRepository.FindOneByJmbg(userJmbg);
            if (user == null)
                user = ManagerRepository.FindOneByJmbg(userJmbg);
            if (user == null)
                user = SecretaryRepository.FindOneByJmbg(userJmbg);
            if (user == null)
                throw new Exception("Something went wrong!");
            return user;
        }


        public PossibleAppointmentsDTO GetPossibleAppointmentsForAbsence(String doctorJmbg,
            DateTime dateFrom, DateTime dateUntil, int duration)
        {
            List<String> doctorJmbgs = new List<String>();
            doctorJmbgs.Add(doctorJmbg);
            ValidateInputParametersForGetPossibleAppointments("*", doctorJmbgs, -1, dateFrom, dateUntil);
            if ((DateTime.Today.AddDays(2) > dateFrom))
                throw new Exception("Chosen date must be al least 2 days earlier!");
            dateFrom = dateFrom.Date;
            dateUntil = dateUntil.Date;
            List<DateTime> possibleStartDaysOfAbsencePeriod = GetListOfDaysForAbsencePeriod(dateUntil, dateFrom);
            RemoveOccupiedDaysFromPossibleStartDaysOfAbscenePeriod(dateFrom, dateUntil, ref possibleStartDaysOfAbsencePeriod, doctorJmbg);
            DateTime startDate = GetStartDateOfAbsencePeriod(duration, possibleStartDaysOfAbsencePeriod);
            return (startDate.Year != 1) ? new PossibleAppointmentsDTO("", "", doctorJmbg, "", "", -1, "", startDate, duration, -1) : null;
        }

        private List<DateTime> GetListOfDaysForAbsencePeriod(DateTime dateUntil, DateTime dateFrom)
        {
            List<DateTime> possibleAppointments = new List<DateTime>();
            while (dateFrom != dateUntil.AddDays(1))
            {
                possibleAppointments.Add(dateFrom);
                dateFrom = dateFrom.AddDays(1);
            }

            return possibleAppointments;
        }

        private DateTime GetStartDateOfAbsencePeriod(int duration, List<DateTime> possibleStartDaysOfAbsencePeriod)
        {
            DateTime startDate = possibleStartDaysOfAbsencePeriod[0];
            int counter = 1;
            for (int i = 1; i < possibleStartDaysOfAbsencePeriod.Count - 1; i++)
            {
                if (counter == duration)
                    break;
                else if (possibleStartDaysOfAbsencePeriod[i].Date.AddDays(1) == possibleStartDaysOfAbsencePeriod[i + 1])
                    counter++;
                else
                {
                    counter = 1;
                    i++;
                    startDate = possibleStartDaysOfAbsencePeriod[i];
                }
            }

            if (counter == duration) return startDate;
            return new DateTime(1, 1, 1);
        }

        private void RemoveOccupiedDaysFromPossibleStartDaysOfAbscenePeriod(DateTime dateFrom, DateTime dateUntil, ref List<DateTime> possibleStartDaysOfAbsencePeriod, String doctorJmbg)
        {
            List<Appointment> doctorAppointments = AppointmentRepository.FindAllByDoctorJmbg(doctorJmbg);
            foreach (var doctorAppointment in doctorAppointments)
            {
                if (doctorAppointment.StartTime >= dateFrom && doctorAppointment.StartTime <= dateUntil)
                {
                    DateTime whatDateToRemove = possibleStartDaysOfAbsencePeriod[0];
                    whatDateToRemove = RemoveDate(possibleStartDaysOfAbsencePeriod, doctorAppointment, whatDateToRemove);
                    possibleStartDaysOfAbsencePeriod.Remove(whatDateToRemove);
                }
            }
        }

        private static DateTime RemoveDate(List<DateTime> possibleAppointments, Appointment da, DateTime whatDateToRemove)
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
        public List<PossibleAppointmentsDTO> GetPossibleAppointmentsForRoomJoin(int firstRoomId, int secondRoomId,
            DateTime dateFrom, DateTime dateUntil, int duration)
        {
            List<String> doctorJmbgs = new List<String>();
            doctorJmbgs.Add("");
            ValidateInputParametersForRoomJoin(firstRoomId, secondRoomId, dateFrom, dateUntil);
            List<DateTime> possibleAppointmentsForFirstRoom = possibleAppointmentsForFirstRoom = FindPossibleStartTimesOfAppointment("", doctorJmbgs, firstRoomId,
                dateFrom, dateUntil, duration);
            List<DateTime> possibleAppointmentsForSecondRoom = possibleAppointmentsForSecondRoom = FindPossibleStartTimesOfAppointment("", doctorJmbgs, secondRoomId,
                dateFrom, dateUntil, duration);
            if (possibleAppointmentsForFirstRoom.Count == 0 || possibleAppointmentsForSecondRoom.Count == 0)
                throw new Exception("There are not free appointments for given parameters!");
            return CreatePossibleAppointmentsDtosForRoomJoin(duration, possibleAppointmentsForFirstRoom, possibleAppointmentsForSecondRoom);
        }

        private static List<PossibleAppointmentsDTO> CreatePossibleAppointmentsDtosForRoomJoin(int duration, List<DateTime> possibleAppointmentsForFirstRoom,
            List<DateTime> possibleAppointmentsForSecondRoom)
        {
            List<PossibleAppointmentsDTO> retValue = new List<PossibleAppointmentsDTO>();
            foreach (var firstRoom in possibleAppointmentsForFirstRoom)
            {
                foreach (var secondRoom in possibleAppointmentsForSecondRoom)
                {
                    if (firstRoom == secondRoom && firstRoom > DateTime.Now.AddHours(1))
                    {
                        PossibleAppointmentsDTO possibleAppointmentsDTO =
                            new PossibleAppointmentsDTO("", "", "", "", "", -1, "", firstRoom, duration, -1);
                        retValue.Add(possibleAppointmentsDTO);
                    }
                }
            }

            return retValue;
        }

        private void ValidateInputParametersForRoomJoin(int firstRoomId, int secondRoomId, DateTime dateFrom,
            DateTime dateUntil)
        {
            if (RoomRepository.FindOneById(firstRoomId) == null || RoomRepository.FindOneById(secondRoomId) == null)
                throw new Exception("One of the rooms doesn't exist!");
            else if (dateFrom > dateUntil)
                throw new Exception("Dates are not valid!");
        }

        public List<PossibleAppointmentsDTO> GetPossibleAppointmentsByDoctor(String patientJmbg, String doctorJmbg,
            DateTime dateFrom, DateTime dateUntil, int duration, String priority)
        {
            List<String> doctorJmbgs = new List<String>();
            doctorJmbgs.Add(doctorJmbg);
            ValidateInputParametersForGetPossibleAppointments(patientJmbg, doctorJmbgs, -1, dateFrom, dateUntil);
            List<PossibleAppointmentsDTO> possibleAppointmentsDTOs = new List<PossibleAppointmentsDTO>();
            List<DateTime> possibleAppointments = new List<DateTime>();
            int roomId = DoctorRepository.FindOneByJmbg(doctorJmbg).RoomId;

            if (priority == "doctor")
            {
                possibleAppointments = FindPossibleAppointmentsForDoctorPriority(patientJmbg, doctorJmbgs, roomId, dateFrom, dateUntil, duration);
            }
            else
            {
                possibleAppointments = FindPossibleAppointmentsForTimePriority(patientJmbg, ref doctorJmbgs, ref roomId, dateFrom, dateUntil, duration);
            }
            return CreatePossibleAppointmentsDtos(patientJmbg, doctorJmbgs[0], roomId, duration, possibleAppointments);
        }

        public List<PossibleAppointmentsDTO> GetPossibleAppointmentsByManager(int roomId,
            DateTime dateFrom, DateTime dateUntil, int duration)
        {
            List<String> doctorJmbgs = new List<String>();
            doctorJmbgs.Add("*");
            ValidateInputParametersForGetPossibleAppointments("*", doctorJmbgs, roomId, dateFrom, dateUntil);
            List<DateTime> possibleAppointments = new List<DateTime>();

            possibleAppointments = FindPossibleStartTimesOfAppointment("", doctorJmbgs, roomId,
                dateFrom, dateUntil, duration);
            if (possibleAppointments.Count == 0)
                throw new Exception("There are not free appointments for given parameters!");
            return CreatePossibleAppointmentsDtos("", "", roomId, duration, possibleAppointments);
        }

        public List<PossibleAppointmentsDTO> GetPossibleAppointmentsBySecretary(String patientJmbg, String doctorJmbg, int roomId,
            DateTime dateFrom, DateTime dateUntil, int duration, String priority)
        {
            List<String> doctorJmbgs = new List<String>();
            doctorJmbgs.Add(doctorJmbg);
            ValidateInputParametersForGetPossibleAppointments(patientJmbg, doctorJmbgs, roomId, dateFrom, dateUntil);
            List<DateTime> possibleAppointments = new List<DateTime>();

            if (priority == "doctor")
            {
                possibleAppointments = FindPossibleAppointmentsForDoctorPriority(patientJmbg, doctorJmbgs, roomId, dateFrom, dateUntil, duration);
            }
            else
            {
                possibleAppointments = FindPossibleAppointmentsForTimePriority(patientJmbg, ref doctorJmbgs, ref roomId, dateFrom, dateUntil, duration);
            }
            return CreatePossibleAppointmentsDtos(patientJmbg, doctorJmbg, roomId, duration, possibleAppointments);
        }
        private void ValidateInputParametersForGetPossibleAppointments(string patientJmbg, List<String> userJmbgs,
            int roomId, DateTime dateFrom, DateTime dateUntil)
        {
            foreach (var userJmbg in userJmbgs)
            {
                if (userJmbgs[0] != "*" && (userJmbg == null || (DoctorRepository.FindOneByJmbg(userJmbg) == null &&
                                                                 ManagerRepository.FindOneByJmbg(userJmbg) == null
                                                                 && SecretaryRepository.FindOneByJmbg(userJmbg) ==
                                                                 null)))
                {
                    if (userJmbgs.Count == 1)
                        throw new Exception("Doctor with that JMBG doesn't exist!");
                    throw new Exception("One of the users doesn't exist!");
                }
            }
            if (patientJmbg != "*" && (patientJmbg == null || PatientRepository.FindOneByJmbg(patientJmbg) == null))
                throw new Exception("Patient with that JMBG doesn't exist!");
            if (roomId != -1 && RoomRepository.FindOneById(roomId) == null)
                throw new Exception("Room with that id doesn't exist!");
            if (dateFrom > dateUntil || dateFrom < DateTime.Now)
                throw new Exception("Dates are not valid!");
        }
        private List<PossibleAppointmentsDTO> CreatePossibleAppointmentsDtos(string patientJmbg, string doctorJmbg, int roomId, int duration,
            List<DateTime> possibleAppointments)
        {
            List<PossibleAppointmentsDTO> possibleAppointmentsDtos = new List<PossibleAppointmentsDTO>();
            Patient selectedPatient = PatientRepository.FindOneByJmbg(patientJmbg);
            Doctor selectedDoctor = DoctorRepository.FindOneByJmbg(doctorJmbg);
            Room selectedRoom = RoomRepository.FindOneById(roomId);
            CheckExistenceOfInputParameters(ref selectedPatient, ref selectedDoctor, ref selectedRoom);
            foreach (var pa in possibleAppointments)
            {
                if (pa > DateTime.Now.AddHours(1))
                {
                    possibleAppointmentsDtos.Add(new PossibleAppointmentsDTO(patientJmbg,
                        selectedPatient.FirstName + " " + selectedPatient.LastName, doctorJmbg,
                        selectedDoctor.FirstName + " " + selectedDoctor.LastName, selectedDoctor.SpecialtyType, roomId,
                        selectedRoom.Name, pa, duration, -1));
                }
            }
            return possibleAppointmentsDtos;
        }

        private void CheckExistenceOfInputParameters(ref Patient patient, ref Doctor doctor, ref Room room)
        {
            if (patient == null)
                patient = new Patient(false, new List<string>(), BloodType.NONE, "", "", "", "", "", DateTime.Now,
                    Gender.NONE, "", "", "");
            if (doctor == null)
                doctor = new Doctor(false, "", 0, "", "", "", "", "", DateTime.Now, Gender.NONE, "", "", "");
            if (room == null)
                room = new Room("", 0, "", RoomType.NONE);
        }

        private List<DateTime> FindPossibleAppointmentsForTimePriority(string patientJmbg, ref List<String> doctorJmbgs, ref int roomId,
            DateTime dateFrom, DateTime dateUntil, int duration)
        {
            List<DateTime> possibleAppointments;
            Doctor sentDoctor = DoctorRepository.FindOneByJmbg(doctorJmbgs[0]);
            List<Doctor> doctorsNeeded = DoctorRepository.FindAllBySpeciality(sentDoctor.SpecialtyType);
            doctorsNeeded.Remove(sentDoctor);
            possibleAppointments = FindPossibleStartTimesOfAppointment(patientJmbg, doctorJmbgs, roomId,
                dateFrom, dateUntil, duration);
            if (possibleAppointments.Count > 0) return possibleAppointments;
            foreach (var doc in doctorsNeeded)
            {
                doctorJmbgs[0] = doc.Jmbg;
                roomId = doc.RoomId;
                possibleAppointments = FindPossibleStartTimesOfAppointment(patientJmbg, doctorJmbgs, roomId, dateFrom, dateUntil, duration);
                if (possibleAppointments.Count != 0) return possibleAppointments;
            }
            throw new Exception("There are not free appointments for given parameters!");
        }

        private List<DateTime> FindPossibleAppointmentsForDoctorPriority(string patientJmbg, List<String> doctorJmbgs, int roomId,
            DateTime dateFrom, DateTime dateUntil, int duration)
        {
            List<DateTime> possibleAppointments;
            possibleAppointments = FindPossibleStartTimesOfAppointment(patientJmbg, doctorJmbgs, roomId,
                dateFrom, dateUntil, duration);
            while (possibleAppointments.Count == 0)
            {
                dateFrom = dateUntil;
                dateUntil = dateUntil.AddDays(5);
                possibleAppointments = FindPossibleStartTimesOfAppointment(patientJmbg, doctorJmbgs, roomId,
                    dateFrom, dateUntil, duration);
            }
            return possibleAppointments;
        }
        public List<DateTime> FindPossibleStartTimesOfAppointment(String patientJmbg, List<String> doctorJmbgs, int roomId, DateTime dateFrom,
            DateTime dateUntil, int duration)
        {
            List<Appointment> appointmentsToWatch = FindAppointmentsToWatch(patientJmbg, doctorJmbgs, roomId, dateFrom, dateUntil);
            List<(DateTime, DateTime)> freePeriods = new List<(DateTime, DateTime)>();
            if (appointmentsToWatch.Count == 0)
            {
                freePeriods = AddDefaultFreePeriods(dateFrom, dateUntil, duration);
            }
            else
                freePeriods = FindFreePeriods(appointmentsToWatch, dateFrom, dateUntil, duration);

            freePeriods = SplitFreePeriods(duration, freePeriods);
            List<DateTime> possibleAppointments = new List<DateTime>();
            if (freePeriods.Count > 0)
            {
                possibleAppointments = FindStartTimesOfPossibleAppointments(freePeriods, duration);
            }
            return possibleAppointments;
        }

        private List<(DateTime, DateTime)> SplitFreePeriods(int duration, List<(DateTime, DateTime)> freePeriods)
        {
            List<(DateTime, DateTime)> newFreePeriods = new List<(DateTime, DateTime)>();
            for (int i = 0; i < freePeriods.Count; i++)
            {
                if (freePeriods[i].Item1.AddHours(9) < freePeriods[i].Item2 && duration < 540)
                {
                    newFreePeriods.AddRange(SplitPeriod(freePeriods[i]));
                }
                else
                    newFreePeriods.Add(freePeriods[i]);
            }

            return newFreePeriods;
        }

        private static List<(DateTime, DateTime)> AddDefaultFreePeriods(DateTime dateFrom, DateTime dateUntil, int duration)
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

        private List<Appointment> FindAppointmentsToWatch(String patientJmbg, List<String> doctorJmbgs, int? roomId, DateTime dateFrom,
            DateTime dateUntil)
        {
            List<Appointment> appointmentsToWatch = CheckAppointmentsToWatch(patientJmbg, doctorJmbgs, roomId, dateFrom, dateUntil);
            appointmentsToWatch.AddRange(CheckBasicRenovationsAppointmentsToWatch(roomId, dateFrom, dateUntil));
            appointmentsToWatch.AddRange(CheckAdvancedRenovationSeparationsToWatch(roomId, dateFrom, dateUntil));
            appointmentsToWatch.AddRange(CheckAdvancedRenovationsJoiningToWatch(roomId, dateFrom, dateUntil));
            appointmentsToWatch.AddRange(CheckMeetingsToWatch(doctorJmbgs, roomId, dateFrom, dateUntil));
            appointmentsToWatch.Sort((x, y) => DateTime.Compare(x.StartTime.AddMinutes(x.Duration), y.StartTime.AddMinutes(y.Duration)));
            return appointmentsToWatch;
        }

        private static Boolean CheckStartTimeForWhatAppointmentsToWatch(DateTime startTime, DateTime dateFrom, DateTime dateUntil, int duration)
        {
            if (startTime > DateTime.Now && startTime.AddMinutes(duration) > dateFrom && startTime < dateUntil)
            {
                return true;
            }
            return false;
        }

        private List<Appointment> CheckAdvancedRenovationsJoiningToWatch(int? roomId, DateTime dateFrom, DateTime dateUntil)
        {
            List<Appointment> appointmentsToWatch = new List<Appointment>();
            List<AdvancedRenovationJoining> advancedRenovationJoinings = AdvancedRenovationJoiningRepository.FindAll();
            foreach (var arj in advancedRenovationJoinings)
            {
                if (CheckStartTimeForWhatAppointmentsToWatch(arj.StartTime, dateFrom, dateUntil, arj.Duration) &&
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

        private List<Appointment> CheckAdvancedRenovationSeparationsToWatch(int? roomId, DateTime dateFrom, DateTime dateUntil)
        {
            List<Appointment> appointmentsToWatch = new List<Appointment>();
            List<AdvancedRenovationSeparation> advancedRenovationSeparations = AdvancedRenovationSeparationRepository.FindAll();
            foreach (var ars in advancedRenovationSeparations)
            {
                if (CheckStartTimeForWhatAppointmentsToWatch(ars.StartTime, dateFrom, dateUntil, ars.Duration) &&
                    ((roomId != null && ars.StartRoomId == roomId)))
                {
                    Appointment advancedRenovationAppointment =
                        new Appointment(ars.StartTime, ars.Duration, -1, "", "", ars.StartRoomId);
                    appointmentsToWatch.Add(advancedRenovationAppointment);
                }
            }

            return appointmentsToWatch;
        }

        private List<Appointment> CheckBasicRenovationsAppointmentsToWatch(int? roomId, DateTime dateFrom, DateTime dateUntil)
        {
            List<BasicRenovation> allBasicRenovations = BasicRenovationRepository.FindAll();
            List<Appointment> appointmentsToWatch = new List<Appointment>();
            foreach (var br in allBasicRenovations)
            {
                if (CheckStartTimeForWhatAppointmentsToWatch(br.StartTime, dateFrom, dateUntil, br.Duration) &&
                    ((roomId != null && br.RoomId == roomId)))
                {
                    Appointment basicRenovationAppointment =
                        new Appointment(br.StartTime, br.Duration, -1, "", "", br.RoomId);
                    appointmentsToWatch.Add(basicRenovationAppointment);
                }
            }

            return appointmentsToWatch;
        }

        private List<Appointment> CheckMeetingsToWatch(List<String> userJmbgs, int? roomId, DateTime dateFrom, DateTime dateUntil)
        {
            List<Meeting> allMeetings = MeetingRepository.FindAll();
            List<Appointment> appointmentsToWatch = new List<Appointment>();
            foreach (var am in allMeetings)
            {
                if (CheckStartTimeForWhatAppointmentsToWatch(am.StartTime, dateFrom, dateUntil, am.Duration) &&
                    ((roomId != null && am.RoomId == roomId) || CheckUsersInMeeting(userJmbgs, am.UserJmbgs)))
                {
                    Appointment basicRenovationAppointment =
                        new Appointment(am.StartTime, am.Duration, -1, "", "", am.RoomId);
                    appointmentsToWatch.Add(basicRenovationAppointment);
                }
            }

            return appointmentsToWatch;
        }

        private Boolean CheckUsersInMeeting(List<String> userJmbgs, List<String> usersInMeeting)
        {
            foreach (var uj in userJmbgs)
            {
                if (usersInMeeting.Contains(uj))
                    return true;
            }

            return false;
        }

        private List<Appointment> CheckAppointmentsToWatch(string patientJmbg, List<string> doctorJmbgs, int? roomId, DateTime dateFrom,
            DateTime dateUntil)
        {
            List<Appointment> appointmentsToWatch = new List<Appointment>();
            List<Appointment> allAppointments = AppointmentRepository.FindAll();
            foreach (var app in allAppointments)
            {
                foreach (var doctorJmbg in doctorJmbgs)
                {
                    if (CheckStartTimeForWhatAppointmentsToWatch(app.StartTime, dateFrom, dateUntil, app.Duration) && (
                            (doctorJmbg != null && app.DoctorJmbg == doctorJmbg) ||
                            (patientJmbg != null && app.PatientJmbg == patientJmbg) ||
                            (roomId != null && app.RoomId == roomId)))
                        appointmentsToWatch.Add(app);
                }
            }
            return appointmentsToWatch;
        }

        private List<(DateTime, DateTime)> FindFreePeriods(List<Appointment> appointmentsToWatch, DateTime dateFrom, DateTime dateUntil, int duration)
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

        private List<(DateTime, DateTime)> SplitPeriod((DateTime, DateTime) period)
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

        private List<DateTime> FindStartTimesOfPossibleAppointments(List<(DateTime, DateTime)> newFreePeriod, int duration)
        {
            List<DateTime> possibleAppointments = new List<DateTime>();
            int numberOfAppointments = 0;
            while (numberOfAppointments < 10)
            {
                Boolean newAppointmentAdded = false;
                for (int i = 0; i < newFreePeriod.Count; i++)
                {
                    if (numberOfAppointments == 10)
                        break;
                    if (newFreePeriod[i].Item1.AddMinutes(duration) <= newFreePeriod[i].Item2)
                    {
                        possibleAppointments.Add(newFreePeriod[i].Item1);
                        newFreePeriod[i] = (newFreePeriod[i].Item1.AddMinutes(duration + 15), newFreePeriod[i].Item2);
                        numberOfAppointments++;
                        newAppointmentAdded = true;
                    }
                }
                if (!newAppointmentAdded)
                    break;
            }
            possibleAppointments.Sort((x, y) => DateTime.Compare(x, y));
            return possibleAppointments;
        }
    }
}
