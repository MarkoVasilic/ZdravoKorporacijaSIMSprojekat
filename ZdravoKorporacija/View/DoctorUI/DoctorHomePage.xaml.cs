using System;
using Controller;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Repository;
using Service;
using ZdravoKorporacija.DTO;
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
            /*AppointmentRepository appointmentRepository = new AppointmentRepository();
            DoctorRepository doctorRepository = new DoctorRepository();
            PatientRepository patientRepository = new PatientRepository();
            RoomRepository roomRepository = new RoomRepository();
            AppointmentService appointmentService = new AppointmentService(appointmentRepository, patientRepository, doctorRepository, roomRepository);
            ScheduleService scheduleService = new ScheduleService();
            EmergencyService emergencyService = new EmergencyService();*/
            this.DataContext = doctorWindowVM;
            //this.DataContext = this;
            //appointmentController = new AppointmentController(appointmentService, scheduleService, emergencyService);
            //appointments = new ObservableCollection<AppointmentDTO>(appointmentController.GetAppointmentsByDoctorJmbgDTO(doctorJmbg));
        }


        private void ViewMedicalRecordButton_OnClick(object sender, RoutedEventArgs e)
        {
            String Jmbg = (String)((Button)sender).CommandParameter;
            this.doctorJmbg = Jmbg;
            NavigationService.Navigate(new ViewMedicalRecordPage(Jmbg));
        }
    }
}
