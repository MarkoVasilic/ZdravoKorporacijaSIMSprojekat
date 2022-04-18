using System.Windows;
using ZdravoKorporacija.View.AppointmentCRUD;

namespace ZdravoKorporacija.View
{
    /// <summary>
    /// Interaction logic for PacijentHomePage.xaml
    /// </summary>
    public partial class PacijentHomePage : Window
    {
        public PacijentHomePage()
        {
            InitializeComponent();
        }

        private void ButtonCreateAppointment(object sender, RoutedEventArgs e)
        {
            CreateAppointmentPage createAppointmentPage = new CreateAppointmentPage();
            createAppointmentPage.Show();
        }



        private void DeleteAppointmentButton(object sender, RoutedEventArgs e)
        {
            DeleteAppointmentPage deleteAppointmentPage = new DeleteAppointmentPage();
            deleteAppointmentPage.Show();
        }

        private void UpdateAppointmentButton(object sender, RoutedEventArgs e)
        {
            UpdateAppointmentPage updateAppointmentPage = new UpdateAppointmentPage();
            updateAppointmentPage.Show();
        }

        private void MyAppointmentsButton(object sender, RoutedEventArgs e)
        {
            GetAllAppointmentsPatient getAllAppointmentsPatient = new GetAllAppointmentsPatient();
            getAllAppointmentsPatient.Show();
        }
    }
}
