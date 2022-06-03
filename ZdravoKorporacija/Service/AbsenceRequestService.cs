using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Service;

namespace Service
{
    public class AbsenceRequestService
    {
        private readonly AbsenceRequestRepository AbsenceRequestRepository;
        private readonly ScheduleService scheduleService;
        private readonly DoctorRepository DoctorRepository;

        public AbsenceRequestService(AbsenceRequestRepository absenceRequestRepository, ScheduleService scheduleService, DoctorRepository doctorRepository)
        {
            AbsenceRequestRepository = absenceRequestRepository;
            this.scheduleService = scheduleService;
            DoctorRepository = doctorRepository;
        }

        public AbsenceRequestService()
        {
        }

        public List<AbsenceRequest> GetAllByDoctorJmbg(String jmbg)
        {
            return AbsenceRequestRepository.FindAllByDoctorJmbg(jmbg);
        }

        public List<AbsenceRequest> GetOnHoldAbsceneRequests()
        {
            List<AbsenceRequest> absenceRequests = AbsenceRequestRepository.FindAll();
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
            AbsenceRequest absenceRequestToChangeState = AbsenceRequestRepository.FindOneById(absceneRequestId);
            if (absenceRequestToChangeState != null)
            {
                absenceRequestToChangeState.State = absenceRequestState;
                AbsenceRequestRepository.UpdateAbsenceRequest(absenceRequestToChangeState);
            }
            else
            {
                throw new Exception("There is no abscene request with that id!");
            }
        }

        public void CreateAbsenceRequest(DateTime dateFrom, DateTime dateUntil, Boolean isUrgent, String reason)
        {
            //hard code ulogovan doctor
            String doctorJmbg = "1231231231231";
            String doctorSpecialtyType = DoctorRepository.FindOneByJmbg(doctorJmbg).SpecialtyType; //doktor moze samo za sebe, jmbg ulogovanog doktora
            int interval = (int)(dateUntil - dateFrom).TotalDays;
            PossibleAppointmentsDTO possibleAppointmentsDTO = scheduleService.GetPossibleAppointmentsForAbsence(doctorJmbg, dateFrom, dateUntil, interval);

            if (isUrgent)
                CreateUgrentAbsenceRequest(dateFrom, dateUntil, isUrgent, reason, doctorJmbg, doctorSpecialtyType, interval);
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
            AbsenceRequest absenceRequest = new AbsenceRequest(id, doctorJmbg, doctorSpecialtyType, dateFrom, dateUntil, interval, reason, isUrgent, AbsenceRequestState.ON_HOLD);
            AbsenceRequestRepository.SaveAbsenceRequest(absenceRequest);
        }

        private void ValidateInputParameters(DateTime dateFrom, string doctorSpecialtyType)
        {
            if ((dateFrom - DateTime.Today).TotalDays <= 2)
                throw new Exception("Chosen date must be al least 2 days erlier!");
            else if (AbsenceRequestRepository.FindAllByDoctorSpecialtyType(doctorSpecialtyType).Count >= 2)
                throw new Exception("More than one doctor your specialty already requested absence!");
        }

        private void CreateUgrentAbsenceRequest(DateTime dateFrom, DateTime dateUntil, bool isUrgent, string reason, string doctorJmbg, string doctorSpecialtyType, int interval)
        {
            int id = GenerateNewId();
            AbsenceRequest absenceRequest = new AbsenceRequest(id, doctorJmbg, doctorSpecialtyType, dateFrom, dateUntil, interval, reason, isUrgent, AbsenceRequestState.ON_HOLD);
            AbsenceRequestRepository.SaveAbsenceRequest(absenceRequest);
        }

        private int GenerateNewId()
        {
            try
            {
                List<AbsenceRequest> absenceRequests = AbsenceRequestRepository.FindAll();
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
