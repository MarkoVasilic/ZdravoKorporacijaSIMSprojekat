using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ZdravoKorporacija.View.SecretaryUI.ViewModels;

namespace ZdravoKorporacija.View.SecretaryUI
{
    public partial class SecretaryHomePage : Page
    {
        public SecretaryHomePage()
        {
            SecretaryWindowVM.setWindowTitle("Dashboard");
            InitializeComponent();
        }

        private void Patient_accounts_click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PatientsView());
        }

        private void Schedule_Emergency_Appointment_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ScheduleEmergencyView());
        }

        private void Schedule_Appointment_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ScheduleAppointmentView());
        }

        private void Check_Scheduled_Appointments_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AppointmentView());
        }
    }
}
