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
            //hard code ulogovan doctor
            String doctorJmbg = "1231231231231";
            String doctorSpecialtyType = DoctorRepository.FindOneByJmbg(doctorJmbg).SpecialtyType; //doktor moze samo za sebe, jmbg ulogovanog doktora
            int interval = (int)(dateUntil - dateFrom).TotalDays;
            PossibleAppointmentsDTO possibleAppointmentsDTO = AppointmentService.GetPossibleAppointmentsForAbsence(doctorJmbg, dateFrom, dateUntil, interval);

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
