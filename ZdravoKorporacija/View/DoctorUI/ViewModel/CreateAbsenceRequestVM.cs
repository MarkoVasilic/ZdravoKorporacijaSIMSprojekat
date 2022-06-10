using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.View.DoctorUI.Commands;

namespace ZdravoKorporacija.View.DoctorUI.ViewModel
{
    public class CreateAbsenceRequestVM : ViewModelBase
    {
        public AbsenceRequestController absenceRequestController { get; set; }

        private String errorMessage;
        public String ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value;
                OnProperyChanged("ErrorMessage");
            }
        }

        private String absenceReason;
        public String AbsenceReason
        {
            get { return absenceReason; }
            set
            {
                absenceReason = value;
                OnProperyChanged("AbsenceReason");
            }
        }

        private DateTime dateFrom;
        public DateTime DateFrom
        {
            get { return dateFrom; }
            set
            {
                dateFrom = value;
                OnProperyChanged("DateFrom");
            }
        }

        private DateTime dateUntil;
        public DateTime DateUntil
        {
            get { return dateUntil; }
            set
            {
                dateUntil = value;
                OnProperyChanged("DateUntil");
            }
        }

        private Boolean isUrgent;
        public Boolean IsUrgent
        {
            get { return isUrgent; }
            set
            {
                isUrgent = value;
                OnProperyChanged("IsUrgent");
            }
        }

        public ICommand ConfirmCommand { get; }

        public CreateAbsenceRequestVM()
        {
            ConfirmCommand = new RelayCommand(confirmExecute);
            DoctorWindowVM.setWindowTitle("Create absence request");
            this.errorMessage = "";
            AbsenceRequestRepository absenceRequestRepository = new AbsenceRequestRepository();
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            PatientRepository patientRepository = new PatientRepository();
            RoomRepository roomRepository = new RoomRepository();
            BasicRenovationRepository basicRenovation = new BasicRenovationRepository();
            AdvancedRenovationJoiningRepository advancedRenovationJoining = new AdvancedRenovationJoiningRepository();
            AdvancedRenovationSeparationRepository advancedRenovationSeparation =
                new AdvancedRenovationSeparationRepository();
            ManagerRepository managerRepository = new ManagerRepository();
            SecretaryRepository secretaryRepository = new SecretaryRepository();
            MeetingRepository meetingRepository = new MeetingRepository();
            DoctorRepository doctorRepository = new DoctorRepository();
            ScheduleService scheduleService = new ScheduleService(appointmentRepository, patientRepository,
                doctorRepository, roomRepository, basicRenovation, advancedRenovationJoining,
                advancedRenovationSeparation, managerRepository, secretaryRepository, meetingRepository);
            AbsenceRequestService absenceRequestService = new AbsenceRequestService(absenceRequestRepository, scheduleService, doctorRepository, appointmentRepository);
            absenceRequestController = new AbsenceRequestController(absenceRequestService);
        }

        private void confirmExecute(object parametar)
        {
            try
            {
                absenceRequestController.Create(DateFrom, DateUntil, IsUrgent, AbsenceReason);
                DoctorWindowVM.NavigationService.Navigate(new AbsenceRequestsPage());
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
        }

        public void urgentChecked(Boolean isChecked)
        {
            IsUrgent = isChecked;
        }

    }
}
