using Controller;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.View.DoctorUI.ViewModel;

namespace ZdravoKorporacija.View.DoctorUI
{
    /// <summary>
    /// Interaction logic for ChooseAppointmentPage.xaml
    /// </summary>
    public partial class ChooseAppointmentPage : Page, INotifyPropertyChanged
    {
        private AppointmentController appointmentController;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string? propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        private String PatientJmbg { get; set; }
        private String DoctorJmbg { get; set; }
        private DateTime DateFrom { get; set; }
        private DateTime DateTo { get; set; }
        private int Duration { get; set; }
        private String Priority { get; set; }
        private int RoomId { get; set; }
        public ObservableCollection<PossibleAppointmentsDTO> appointments { get; set; }
        public ObservableCollection<PossibleAppointmentsDTO> Appointments
        {
            get => appointments;
            set
            {
                appointments = value;
                OnPropertyChanged("Appointments");
            }
        }
        public ChooseAppointmentPage(String patientJmbg, String doctorJmbg, DateTime dateFrom, DateTime dateTo, int duration, String priority, int roomId)
        {
            InitializeComponent();
            DoctorWindowVM.setWindowTitle("Choose appointment to schedule");
            this.PatientJmbg = patientJmbg;
            this.DoctorJmbg = doctorJmbg;
            this.DateFrom = dateFrom;
            this.DateTo = dateTo;
            this.Duration = duration;
            this.Priority = priority;
            this.RoomId = roomId;
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
            appointments = new ObservableCollection<PossibleAppointmentsDTO>(appointmentController.GetPossibleAppointmentsByDoctor(PatientJmbg, DoctorJmbg, DateFrom, DateTo, Duration, Priority, RoomId));

        }

        private void ChooseAppointment_OnClick(object sender, RoutedEventArgs e)
        {
            PossibleAppointmentsDTO possibleAppointment = (PossibleAppointmentsDTO)((Button)sender).CommandParameter;
            appointmentController.CreateAppointmentByDoctor(possibleAppointment);
            notifier.ShowSuccess("Successfully scheduled appointment!");
            DoctorWindowVM doctorWindowVm = new DoctorWindowVM();
            NavigationService.Navigate(new DoctorHomePage(doctorWindowVm));
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
