using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ZdravoKorporacija.View.SecretaryUI
{
    public partial class SecretaryHomePage : Page
    {
        public SecretaryHomePage()
        {
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
    }
}
