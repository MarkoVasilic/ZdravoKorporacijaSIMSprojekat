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
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            AppointmentService appointmentService = new AppointmentService();
            ratingService = new RatingService(ratingRepository, appointmentRepository);
            ScheduleService scheduleService = new ScheduleService();
            EmergencyService emergencyService = new EmergencyService();
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