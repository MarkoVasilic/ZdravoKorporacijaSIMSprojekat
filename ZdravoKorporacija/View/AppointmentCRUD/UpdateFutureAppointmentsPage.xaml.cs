using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ZdravoKorporacija.View.AppointmentCRUD.ViewModels;

namespace ZdravoKorporacija.View.AppointmentCRUD
{
    /// <summary>
    /// Interaction logic for UpdateFutureAppointmentsPage.xaml
    /// </summary>
    public partial class UpdateFutureAppointmentsPage : Page
    {

        public UpdateFutureAppointmentsPage()
        {
            InitializeComponent();
            DataContext = new UpdateAppointmentVM();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AppointmentPage());
        }
    }
}
