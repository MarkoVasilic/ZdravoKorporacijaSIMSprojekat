using System;
using Model;
using Repository;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AbsenceRequestService
    {
        private readonly AbsenceRequestRepository AbsenceRequestRepository;
        private readonly AppointmentService AppointmentService;

        public AbsenceRequestService(AbsenceRequestRepository absenceRequestRepository, AppointmentService appointmentService)
        {
            AbsenceRequestRepository = absenceRequestRepository;
            AppointmentService = appointmentService;
        }

        public AbsenceRequestService()
        {
        }

        public void CreateAbsenceRequest(String patientJmbg, String doctorJmbg,
            DateTime dateFrom, DateTime dateUntil, int duration)
        {

        }
    }
}
