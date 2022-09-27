using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Controller;
using Repository;
using Service;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.View.DoctorUI.ViewModel;

namespace ZdravoKorporacija.View.DoctorUI
{
    /// <summary>
    /// Interaction logic for ModifyDoctorAppointmentPage.xaml
    /// </summary>
    public partial class ModifyDoctorAppointmentPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string? propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int AppointmentId { get; set; }

        private AppointmentController appointmentController;

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

        private DateTime newDate;
        public DateTime NewDate
        {
            get { return newDate; }
            set
            {
                newDate = value;
                OnPropertyChanged("DateFrom");
            }
        }
        public ModifyDoctorAppointmentPage(int id)
        {
            InitializeComponent();
            DoctorWindowVM.setWindowTitle("Choose new date to reschedule appointment");
            this.ErrorMessage = "";
            this.AppointmentId = id;
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            DoctorRepository doctorRepository = new DoctorRepository();
            PatientRepository patientRepository = new PatientRepository();
            RoomRepository roomRepository = new RoomRepository();
            BasicRenovationRepository basicRenovationRepository = new BasicRenovationRepository();
            AdvancedRenovationJoiningRepository advancedRenovationJoining = new AdvancedRenovationJoiningRepository();
            AdvancedRenovationSeparationRepository advancedRenovationSeparation =
                new AdvancedRenovationSeparationRepository();
            ManagerRepository managerRepository = new ManagerRepository();
            SecretaryRepository secretaryRepository = new SecretaryRepository();
            AppointmentService appointmentService = new AppointmentService(appointmentRepository, patientRepository, doctorRepository, roomRepository);
            MeetingRepository meetingRepository = new MeetingRepository();
            ScheduleService scheduleService = new ScheduleService(appointmentRepository, patientRepository,
                doctorRepository, roomRepository, basicRenovationRepository, advancedRenovationJoining,
                advancedRenovationSeparation, managerRepository, secretaryRepository, meetingRepository);
            EmergencyService emergencyService = new EmergencyService(appointmentRepository, patientRepository,
                doctorRepository, roomRepository, basicRenovationRepository, advancedRenovationJoining,
                advancedRenovationSeparation, scheduleService);
            this.DataContext = this;
            appointmentController = new AppointmentController(appointmentService, scheduleService, emergencyService);
        }

        private void Confirm_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (NewDate == null || NewDate < DateTime.Now)
                {
                    ErrorMessage = "Please pick new date to reschedule appointment!";
                }
                else
                {
                    appointmentController.ModifyAppointment(AppointmentId, NewDate);
                    notifier.ShowSuccess("Successfully modified appointment!");
                    DoctorWindowVM doctorWindowVm = new DoctorWindowVM();
                    NavigationService.Navigate(new DoctorHomePage(doctorWindowVm));
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
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
