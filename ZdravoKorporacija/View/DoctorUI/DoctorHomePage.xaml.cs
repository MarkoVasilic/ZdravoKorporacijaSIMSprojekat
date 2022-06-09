using System;
using Controller;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Repository;
using Service;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.View.DoctorUI.ViewModel;

namespace ZdravoKorporacija.View.DoctorUI
{
    /// <summary>
    /// Interaction logic for DoctorHomePage.xaml
    /// </summary>
    public partial class DoctorHomePage : Page
    {
        private AppointmentController appointmentController;
        private String doctorJmbg { get; set; }


        public ObservableCollection<AppointmentDTO> appointments { get; set; }
        public DoctorHomePage(DoctorWindowVM doctorWindowVM)
        {
            InitializeComponent();
            DoctorWindowVM.setWindowTitle("Appointment Schedule");
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
            //this.DataContext = doctorWindowVM;
            this.DataContext = this;
            appointmentController = new AppointmentController(appointmentService, scheduleService, emergencyService);
            appointments = new ObservableCollection<AppointmentDTO>(appointmentController.GetAppointmentsByDoctorJmbgDTO(App.loggedUser.Jmbg));
        }


        private void ViewMedicalRecordButton_OnClick(object sender, RoutedEventArgs e)
        {
            String Jmbg = (String)((Button)sender).CommandParameter;
            this.doctorJmbg = Jmbg;
            NavigationService.Navigate(new ViewMedicalRecordPage(Jmbg));
        }
    }
}
