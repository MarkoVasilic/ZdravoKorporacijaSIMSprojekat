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

        public void Create(DateTime dateFrom, DateTime dateUntil, Boolean isUrgent, String reason)
        {
            absenceRequestService.Create(dateFrom, dateUntil, isUrgent, reason);
        }

        public List<AbsenceRequest> GetOnHold()
        {
            return absenceRequestService.GetOnHold();
        }

        public void ChangeState(int absceneRequestId, AbsenceRequestState absenceRequestState, String response)
        {
            absenceRequestService.ChangeState(absceneRequestId, absenceRequestState, response);
        }
        public PossibleAppointmentsDTO GetPossibleAppointments(String doctorJmbg,
            DateTime dateFrom, DateTime dateUntil, int duration)
        {
            return absenceRequestService.GetPossibleAppointments(doctorJmbg, dateFrom, dateUntil, duration);
        }
    }
}
