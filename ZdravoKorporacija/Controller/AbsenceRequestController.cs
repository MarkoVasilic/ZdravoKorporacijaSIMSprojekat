using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class AbsenceRequestController
    {
        private readonly AbsenceRequestService AbsenceRequestService;

        public AbsenceRequestController(AbsenceRequestService absenceRequestService)
        {
            AbsenceRequestService = absenceRequestService;
        }

        public void CreateAbsenceRequest(DateTime dateFrom, DateTime dateUntil, Boolean isUrgent, String reason)
        {
            AbsenceRequestService.CreateAbsenceRequest(dateFrom, dateUntil, isUrgent, reason);
        }

        public List<AbsenceRequest> GetOnHoldAbsceneRequests()
        {
            return AbsenceRequestService.GetOnHoldAbsceneRequests();
        }

        public void ChangeAbsceneRequestState(int absceneRequestId, AbsenceRequestState absenceRequestState)
        {
            AbsenceRequestService.ChangeAbsceneRequestState(absceneRequestId, absenceRequestState);
        }
    }
}
