using System;
using Service;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controller
{
    public class AbsenceRequestController
    {
        private readonly AbsenceRequestService AbsenceRequestService;

        public AbsenceRequestController(AbsenceRequestService absenceRequestService)
        {
            AbsenceRequestService = absenceRequestService;
        }

        public List<AbsenceRequest> GetAllByDoctorJmbg(String jmbg)
        {
            return AbsenceRequestService.GetAllByDoctorJmbg(jmbg);
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
