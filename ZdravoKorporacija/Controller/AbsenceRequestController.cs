using Model;
using System;
using ZdravoKorporacija.Service;
using System.Collections.Generic;
using ZdravoKorporacija.DTO;

namespace ZdravoKorporacija.Controller
{
    public class AbsenceRequestController
    {
        private readonly AbsenceRequestService _absenceRequestService;

        public AbsenceRequestController(AbsenceRequestService absenceRequestService)
        {
            this._absenceRequestService = absenceRequestService;
        }

        public AbsenceRequest GetOneById(int id)
        {
            return _absenceRequestService.GetOneById(id);
        }

        public List<AbsenceRequest> GetAllByDoctorJmbg(String doctorJmbg)
        {
            return _absenceRequestService.GetAllByDoctorJmbg(doctorJmbg);
        }

        public void Create(DateTime dateFrom, DateTime dateUntil, Boolean isUrgent, String reason)
        {
            _absenceRequestService.Create(dateFrom, dateUntil, isUrgent, reason);
        }

        public List<AbsenceRequest> GetOnHold()
        {
            return _absenceRequestService.GetOnHold();
        }

        public void ChangeState(int absceneRequestId, AbsenceRequestState absenceRequestState, String response)
        {
            _absenceRequestService.ChangeState(absceneRequestId, absenceRequestState, response);
        }
        public PossibleAppointmentsDTO GetPossibleAppointments(String doctorJmbg,
            DateTime dateFrom, DateTime dateUntil, int duration)
        {
            return _absenceRequestService.GetPossibleAppointments(doctorJmbg, dateFrom, dateUntil, duration);
        }
    }
}
