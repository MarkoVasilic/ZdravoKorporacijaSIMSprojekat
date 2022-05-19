using System;
using Model;
using Repository;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Service;

namespace Service
{
    public class AbsenceRequestService
    {
        private readonly AbsenceRequestRepository AbsenceRequestRepository;
        private readonly AppointmentService AppointmentService = new AppointmentService();
        private readonly DoctorRepository DoctorRepository;

        public AbsenceRequestService(AbsenceRequestRepository absenceRequestRepository, DoctorRepository doctorRepository)
        {
            AbsenceRequestRepository = absenceRequestRepository;
            DoctorRepository = doctorRepository;
        }

        public AbsenceRequestService()
        {
        }

        public void CreateAbsenceRequest(DateTime dateFrom, DateTime dateUntil, Boolean isUrgent, String reason)
        {
            String doctorSpecialtyType = DoctorRepository.FindOneByJmbg("4444444444444").SpecialtyType;//doktor moze samo za sebe, jmbg ulogovanog doktora
            int interval = (int)(dateUntil - dateFrom).TotalDays;
            if (isUrgent == true)
            {
                int id = GenerateNewId();
                AbsenceRequest absenceRequest = new AbsenceRequest(id, "4444444444444", doctorSpecialtyType, dateFrom, dateUntil, interval, reason, isUrgent, AbsenceRequestState.ON_HOLD);
                AbsenceRequestRepository.SaveAbsenceRequest(absenceRequest);
            }
            if((dateFrom - DateTime.Today).TotalDays <= 2)
            {
                throw new Exception("Choosen date must be al least 2 days erlier!");

            }
            if (AbsenceRequestRepository.FindAllByDoctorSpecialtyType(doctorSpecialtyType).Count > 1)
            {
                throw new Exception("More than one doctor your specialty already requested absence!");
            }
            PossibleAppointmentsDTO possibleAppointmentsDTO = AppointmentService.GetPossibleAppointmentsForAbsence("4444444444444", dateFrom, dateUntil, interval);
            if (possibleAppointmentsDTO != null)
            {
                int id = GenerateNewId();
                AbsenceRequest absenceRequest = new AbsenceRequest(id, "4444444444444", doctorSpecialtyType, dateFrom, dateUntil, interval, reason, isUrgent, AbsenceRequestState.ON_HOLD);
                AbsenceRequestRepository.SaveAbsenceRequest(absenceRequest);
            }
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
