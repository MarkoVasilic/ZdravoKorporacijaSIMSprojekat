using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Interfaces;
using ZdravoKorporacija.Repository;


namespace ZdravoKorporacija.Service
{
    public class AbsenceRequestService
    {
        private readonly AbsenceRequestRepository _absenceRequestRepository;
        private readonly ScheduleService _scheduleService;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IAppointmentRepository _appointmentRepository;

        public AbsenceRequestService(AbsenceRequestRepository absenceRequestRepository, ScheduleService scheduleService, IDoctorRepository doctorRepository, IAppointmentRepository appointmentRepository)
        {
            this._absenceRequestRepository = absenceRequestRepository;
            this._scheduleService = scheduleService;
            this._doctorRepository = doctorRepository;
            this._appointmentRepository = appointmentRepository;
        }

        public AbsenceRequest GetOneById(int id)
        {
            return _absenceRequestRepository.FindOneById(id);
        }

        public PossibleAppointmentsDTO GetPossibleAppointments(String doctorJmbg,
            DateTime dateFrom, DateTime dateUntil, int duration)
        {
            List<String> doctorJmbgs = new List<String>();
            doctorJmbgs.Add(doctorJmbg);
            _scheduleService.ValidateInputParametersForGetPossibleAppointments("*", doctorJmbgs, -1, dateFrom, dateUntil);
            if ((DateTime.Today.AddDays(2) > dateFrom))
                throw new Exception("Chosen date must be al least 2 days earlier!");
            dateFrom = dateFrom.Date;
            dateUntil = dateUntil.Date;
            List<DateTime> possibleStartDaysOfAbsencePeriod = GetCandidateStartDays(dateFrom, dateUntil);
            RemoveOccupiedDaysFromCandidateDays(dateFrom, dateUntil, possibleStartDaysOfAbsencePeriod, doctorJmbg);
            DateTime startDate = GetStartDate(duration, possibleStartDaysOfAbsencePeriod);
            return (startDate.Year != 1) ? new PossibleAppointmentsDTO("", "", doctorJmbg, "", "", -1, "", startDate, duration, -1) : null;
        }

        private List<DateTime> GetCandidateStartDays(DateTime dateFrom, DateTime dateUntil)
        {
            List<DateTime> candidateStartDays = new List<DateTime>();
            while (dateFrom != dateUntil.AddDays(1))
            {
                candidateStartDays.Add(dateFrom);
                dateFrom = dateFrom.AddDays(1);
            }

            return candidateStartDays;
        }

        private DateTime GetStartDate(int duration, List<DateTime> possibleStartDaysOfAbsencePeriod)
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

        private void RemoveOccupiedDaysFromCandidateDays(DateTime dateFrom, DateTime dateUntil, List<DateTime> possibleStartDaysOfAbsencePeriod, String doctorJmbg)
        {
            List<Appointment> doctorAppointments = _appointmentRepository.FindAllByDoctorJmbg(doctorJmbg);
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
            return _absenceRequestRepository.FindAllByDoctorJmbg(jmbg);
        }

        public List<AbsenceRequest> GetOnHold()
        {
            List<AbsenceRequest> absenceRequests = _absenceRequestRepository.FindAll();
            List<AbsenceRequest> absenceRequestsOnHold = new List<AbsenceRequest>();
            foreach (var absenceRequest in absenceRequests)
            {
                if (absenceRequest.State == AbsenceRequestState.ON_HOLD)
                    absenceRequestsOnHold.Add(absenceRequest);
            }

            return absenceRequestsOnHold;
        }

        public void ChangeState(int absceneRequestId, AbsenceRequestState absenceRequestState, String response)
        {
            AbsenceRequest absenceRequestToChangeState = _absenceRequestRepository.FindOneById(absceneRequestId);
            if (absenceRequestToChangeState != null)
            {
                absenceRequestToChangeState.State = absenceRequestState;
                absenceRequestToChangeState.Response = response;
                _absenceRequestRepository.UpdateAbsenceRequest(absenceRequestToChangeState);
            }
            else
            {
                throw new Exception("There is no absence request with that id!");
            }
        }

        public void Create(DateTime dateFrom, DateTime dateUntil, Boolean isUrgent, String reason)
        {
            String doctorJmbg = "1231231231231";
            String doctorSpecialtyType = _doctorRepository.FindOneByJmbg(doctorJmbg).SpecialtyType;
            int interval = (int)(dateUntil - dateFrom).TotalDays;
            PossibleAppointmentsDTO possibleAppointmentsDTO = GetPossibleAppointments(doctorJmbg, dateFrom, dateUntil, interval);

            if (isUrgent)
                CreateUrgent(dateFrom, dateUntil, isUrgent, reason, doctorJmbg, doctorSpecialtyType, interval);
            else
            {
                ValidateInputParameters(dateFrom, doctorSpecialtyType);
                CreateIfPossible(dateFrom, dateUntil, isUrgent, reason, doctorJmbg, doctorSpecialtyType, interval, possibleAppointmentsDTO);
            }

        }

        private void CreateIfPossible(DateTime dateFrom, DateTime dateUntil, bool isUrgent, string reason, string doctorJmbg, string doctorSpecialtyType, int interval, PossibleAppointmentsDTO possibleAppointmentsDTO)
        {
            if (possibleAppointmentsDTO == null)
                throw new Exception("No free appointments for chosen period. Choose another period of absence!");
            else
                CreateRegular(dateFrom, dateUntil, isUrgent, reason, doctorJmbg, doctorSpecialtyType, interval);
        }

        private void CreateRegular(DateTime dateFrom, DateTime dateUntil, bool isUrgent, string reason, string doctorJmbg, string doctorSpecialtyType, int interval)
        {
            int id = GenerateNewId();
            AbsenceRequest absenceRequest = new AbsenceRequest(id, doctorJmbg, doctorSpecialtyType, dateFrom, dateUntil, interval, reason, isUrgent, AbsenceRequestState.ON_HOLD, null);
            _absenceRequestRepository.SaveAbsenceRequest(absenceRequest);
        }

        private void ValidateInputParameters(DateTime dateFrom, string doctorSpecialtyType)
        {
            if ((dateFrom - DateTime.Today).TotalDays <= 2)
                throw new Exception("Chosen date must be al least 2 days erlier!");
            else if (_absenceRequestRepository.FindAllByDoctorSpecialtyType(doctorSpecialtyType).Count >= 2)
                throw new Exception("More than one doctor your specialty already requested absence!");
        }

        private void CreateUrgent(DateTime dateFrom, DateTime dateUntil, bool isUrgent, string reason, string doctorJmbg, string doctorSpecialtyType, int interval)
        {
            int id = GenerateNewId();
            AbsenceRequest absenceRequest = new AbsenceRequest(id, doctorJmbg, doctorSpecialtyType, dateFrom, dateUntil, interval, reason, isUrgent, AbsenceRequestState.ON_HOLD, null);
            _absenceRequestRepository.SaveAbsenceRequest(absenceRequest);
        }

        private int GenerateNewId()
        {
            try
            {
                List<AbsenceRequest> absenceRequests = _absenceRequestRepository.FindAll();
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
