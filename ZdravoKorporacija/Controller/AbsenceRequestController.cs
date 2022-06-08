using Model;
using System;
using ZdravoKorporacija.Service;
using System.Collections.Generic;
using ZdravoKorporacija.DTO;

namespace ZdravoKorporacija.Controller
{
    public class AbsenceRequestController
    {
        private readonly AbsenceRequestService absenceRequestService;

        public AbsenceRequestController(AbsenceRequestService absenceRequestService)
        {
            this.absenceRequestService = absenceRequestService;
        }

        public AbsenceRequest GetOneById(int id)
        {
            return absenceRequestService.GetOneById(id);
        }

        public List<AbsenceRequest> GetAllByDoctorJmbg(String doctorJmbg)
        {
            return absenceRequestService.GetAllByDoctorJmbg(doctorJmbg);
        }

        public void CreateAbsenceRequest(DateTime dateFrom, DateTime dateUntil, Boolean isUrgent, String reason)
        {
            absenceRequestService.CreateAbsenceRequest(dateFrom, dateUntil, isUrgent, reason);
        }

        public List<AbsenceRequest> GetOnHoldAbsceneRequests()
        {
            return absenceRequestService.GetOnHoldAbsceneRequests();
        }

        public void ChangeAbsceneRequestState(int absceneRequestId, AbsenceRequestState absenceRequestState)
        {
            absenceRequestService.ChangeAbsceneRequestState(absceneRequestId, absenceRequestState);
        }
        public PossibleAppointmentsDTO GetPossibleAppointmentsForAbsence(String doctorJmbg,
            DateTime dateFrom, DateTime dateUntil, int duration)
        {
            return GetPossibleAppointmentsForAbsence(doctorJmbg, dateFrom, dateUntil, duration);
        }
    }
}
