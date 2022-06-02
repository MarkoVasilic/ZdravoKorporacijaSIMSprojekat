using Controller;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ZdravoKorporacija.View.PatientUI.ViewModels;

namespace ZdravoKorporacija.View.PatientUI
{
    /// <summary>
    /// Interaction logic for UpdateFutureAppointmentsPage.xaml
    /// </summary>
    public partial class UpdateFutureAppointmentsPage : Page
    {
        private static AppointmentController appointmentController { get; set; }

        public UpdateFutureAppointmentsPage()
        {
            InitializeComponent();
            int id = 44;
           // DataContext = new UpdateAppointmentVM(id);
            /* PatientRepository patientRepository = new PatientRepository();
             PatientService patientService = new PatientService(patientRepository);
             DoctorRepository doctorRepository = new DoctorRepository();
             DoctorService doctorService = new DoctorService(doctorRepository);
             RoomRepository roomRepository = new RoomRepository();
             RoomService roomService = new RoomService(roomRepository);
             BasicRenovationRepository basicRenovationRepository = new BasicRenovationRepository();
             AppointmentRepository appointmentRepository = new AppointmentRepository();
             AppointmentService appointmentService = new AppointmentService(appointmentRepository, patientRepository, doctorRepository,
             roomRepository, basicRenovationRepository);
             appointmentController = new AppointmentController(appointmentService);*/
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

    }
}
