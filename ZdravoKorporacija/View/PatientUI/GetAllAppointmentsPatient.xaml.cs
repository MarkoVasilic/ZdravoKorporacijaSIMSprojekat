using Controller;
using Repository;
using Service;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.View.PatientUI
{

    public partial class GetAllAppointmentsPatient : Page
    {
        private AppointmentController appointmentController;

        private RatingService ratingService { get; set; }

        public ObservableCollection<PossibleAppointmentsDTO> appointments { get; set; }

        public static int AppointmentToBeRatedId { get; set; }

        public GetAllAppointmentsPatient()
        {
            InitializeComponent();
            RatingRepository ratingRepository = new RatingRepository();
            PatientRepository patientRepository = new PatientRepository();
            RoomRepository roomRepository = new RoomRepository();
            DoctorRepository doctorRepository = new DoctorRepository();
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            AppointmentService appointmentService = new AppointmentService(appointmentRepository,patientRepository,doctorRepository,roomRepository);
            ratingService = new RatingService(ratingRepository, appointmentRepository);
            ManagerRepository managerRepository = new ManagerRepository();
            SecretaryRepository secretaryRepository = new SecretaryRepository();
            MeetingRepository meetingRepository = new MeetingRepository();
            AdvancedRenovationJoiningRepository advancedRenovationJoining = new AdvancedRenovationJoiningRepository();
            AdvancedRenovationSeparationRepository advancedRenovationSeparation =
                new AdvancedRenovationSeparationRepository();
            BasicRenovationRepository basicRenovationRepository = new BasicRenovationRepository();
            ScheduleService scheduleService = new ScheduleService(appointmentRepository, patientRepository,
                doctorRepository, roomRepository, basicRenovationRepository, advancedRenovationJoining,
                advancedRenovationSeparation, managerRepository, secretaryRepository, meetingRepository);
            EmergencyService emergencyService = new EmergencyService(appointmentRepository, patientRepository,
                doctorRepository, roomRepository, basicRenovationRepository, advancedRenovationJoining,
                advancedRenovationSeparation, scheduleService);
            appointmentController = new AppointmentController(appointmentService, scheduleService, emergencyService);
            this.DataContext = this;
            appointments = new ObservableCollection<PossibleAppointmentsDTO>(appointmentController.GetAllPastAppointmentsByPatient());
            for (int i = 0; i < appointments.Count; i++)
            {
                if (ratingService.FindByAppointmentId(appointments[i].AppointmentId) == true)
                {
                    appointments.RemoveAt(i);
                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PatientHomePage());
        }

        private void OcijeniButtonClick(object sender, RoutedEventArgs e)
        {
            PossibleAppointmentsDTO obj = ((FrameworkElement)sender).DataContext as PossibleAppointmentsDTO;
            AppointmentToBeRatedId = obj.AppointmentId;
            NavigationService.Navigate(new AppointmentRatingPage());
        }
    }
}