using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
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
                OnPropertyChanged("ErrorMessage");
            }
        }

        private String absenceReason;
        public String AbsenceReason
        {
            get { return absenceReason; }
            set
            {
                absenceReason = value;
                OnPropertyChanged("AbsenceReason");
            }
        }

        private DateTime dateFrom;
        public DateTime DateFrom
        {
            get { return dateFrom; }
            set
            {
                dateFrom = value;
                OnPropertyChanged("DateFrom");
            }
        }

        private DateTime dateUntil;
        public DateTime DateUntil
        {
            get { return dateUntil; }
            set
            {
                dateUntil = value;
                OnPropertyChanged("DateUntil");
            }
        }

        private Boolean isUrgent;
        public Boolean IsUrgent
        {
            get { return isUrgent; }
            set
            {
                isUrgent = value;
                OnPropertyChanged("IsUrgent");
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
                if (String.IsNullOrWhiteSpace(AbsenceReason))
                {
                    ErrorMessage = "Please enter absence reason!";
                }
                else
                {
                    absenceRequestController.Create(DateFrom, DateUntil, IsUrgent, AbsenceReason);
                    notifier.ShowSuccess("Successfully created absence request!");
                    DoctorWindowVM.NavigationService.Navigate(new AbsenceRequestsPage());
                }
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

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 30,
                offsetY: 90);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(7),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });

    }
}
