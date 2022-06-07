using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Repository;


namespace ZdravoKorporacija.Service
{
    public class AbsenceRequestService
    {
        private readonly AbsenceRequestRepository AbsenceRequestRepository;
        private readonly ScheduleService scheduleService;
        private readonly DoctorRepository DoctorRepository;
        private readonly AppointmentRepository AppointmentRepository;

        public AbsenceRequestService(AbsenceRequestRepository absenceRequestRepository, ScheduleService scheduleService, DoctorRepository doctorRepository, AppointmentRepository appointmentRepository)
        {
            AbsenceRequestRepository = absenceRequestRepository;
            this.scheduleService = scheduleService;
            DoctorRepository = doctorRepository;
            AppointmentRepository = appointmentRepository;
        }

        public AbsenceRequestService()
        {
        }

        public PossibleAppointmentsDTO GetPossibleAppointmentsForAbsence(String doctorJmbg,
            DateTime dateFrom, DateTime dateUntil, int duration)
        {
            List<String> doctorJmbgs = new List<String>();
            doctorJmbgs.Add(doctorJmbg);
            scheduleService.ValidateInputParametersForGetPossibleAppointments("*", doctorJmbgs, -1, dateFrom, dateUntil);
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

        public List<AbsenceRequest> GetAllByDoctorJmbg(String jmbg)
        {
            return absenceRequestRepository.FindAllByDoctorJmbg(jmbg);
        }

        public List<AbsenceRequest> GetOnHoldAbsceneRequests()
        {
            List<AbsenceRequest> absenceRequests = absenceRequestRepository.FindAll();
            List<AbsenceRequest> absenceRequestsOnHold = new List<AbsenceRequest>();
            foreach (var absenceRequest in absenceRequests)
            {
                if (absenceRequest.State == AbsenceRequestState.ON_HOLD)
                    absenceRequestsOnHold.Add(absenceRequest);
            }

            return absenceRequestsOnHold;
        }

        public void ChangeAbsceneRequestState(int absceneRequestId, AbsenceRequestState absenceRequestState)
        {
            AbsenceRequest absenceRequestToChangeState = absenceRequestRepository.FindOneById(absceneRequestId);
            if (absenceRequestToChangeState != null)
            {
                absenceRequestToChangeState.State = absenceRequestState;
                absenceRequestRepository.UpdateAbsenceRequest(absenceRequestToChangeState);
            }
            else
            {
                throw new Exception("There is no absence request with that id!");
            }
        }

        public void CreateAbsenceRequest(DateTime dateFrom, DateTime dateUntil, Boolean isUrgent, String reason)
        {
            String doctorJmbg = "1231231231231";
            String doctorSpecialtyType = doctorRepository.FindOneByJmbg(doctorJmbg).SpecialtyType;
            int interval = (int)(dateUntil - dateFrom).TotalDays;
            PossibleAppointmentsDTO possibleAppointmentsDTO = GetPossibleAppointmentsForAbsence(doctorJmbg, dateFrom, dateUntil, interval);

            if (isUrgent)
                CreateUrgentAbsenceRequest(dateFrom, dateUntil, isUrgent, reason, doctorJmbg, doctorSpecialtyType, interval);
            else
            {
                ValidateInputParameters(dateFrom, doctorSpecialtyType);
                CreateAbsenceRequestIfPossible(dateFrom, dateUntil, isUrgent, reason, doctorJmbg, doctorSpecialtyType, interval, possibleAppointmentsDTO);
            }

        }

        private void CreateAbsenceRequestIfPossible(DateTime dateFrom, DateTime dateUntil, bool isUrgent, string reason, string doctorJmbg, string doctorSpecialtyType, int interval, PossibleAppointmentsDTO possibleAppointmentsDTO)
        {
            if (possibleAppointmentsDTO == null)
                throw new Exception("No free appointments for chosen period. Choose another period of absence!");
            else
                CreateRegularAbsenceRequest(dateFrom, dateUntil, isUrgent, reason, doctorJmbg, doctorSpecialtyType, interval);
        }

        private void CreateRegularAbsenceRequest(DateTime dateFrom, DateTime dateUntil, bool isUrgent, string reason, string doctorJmbg, string doctorSpecialtyType, int interval)
        {
            int id = GenerateNewId();
            AbsenceRequest absenceRequest = new AbsenceRequest(id, doctorJmbg, doctorSpecialtyType, dateFrom, dateUntil, interval, reason, isUrgent, AbsenceRequestState.ON_HOLD, null);
            absenceRequestRepository.SaveAbsenceRequest(absenceRequest);
        }

        private void ValidateInputParameters(DateTime dateFrom, string doctorSpecialtyType)
        {
            if ((dateFrom - DateTime.Today).TotalDays <= 2)
                throw new Exception("Chosen date must be al least 2 days erlier!");
            else if (absenceRequestRepository.FindAllByDoctorSpecialtyType(doctorSpecialtyType).Count >= 2)
                throw new Exception("More than one doctor your specialty already requested absence!");
        }

        private void CreateUrgentAbsenceRequest(DateTime dateFrom, DateTime dateUntil, bool isUrgent, string reason, string doctorJmbg, string doctorSpecialtyType, int interval)
        {
            int id = GenerateNewId();
            AbsenceRequest absenceRequest = new AbsenceRequest(id, doctorJmbg, doctorSpecialtyType, dateFrom, dateUntil, interval, reason, isUrgent, AbsenceRequestState.ON_HOLD, null);
            absenceRequestRepository.SaveAbsenceRequest(absenceRequest);
        }

        private int GenerateNewId()
        {
            try
            {
                List<AbsenceRequest> absenceRequests = absenceRequestRepository.FindAll();
                int currentMax = absenceRequests.Max(obj => obj.Id);
                return currentMax + 1;
            }
            catch
            {
                return 1;
            }
        }
    }
}
