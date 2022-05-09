using System.Windows;
using System.Windows.Controls;
using ZdravoKorporacija.View.AppointmentCRUD;
using ZdravoKorporacija.View.AppointmentCRUD.ViewModels;

namespace ZdravoKorporacija.View
{
    /// <summary>
    /// Interaction logic for UpdateAppointmentPage.xaml
    /// </summary>
    public partial class UpdateAppointmentPage : Page
    {
        private UpdateAppointmentVM updateAppointmentVM;
        public UpdateAppointmentPage(UpdateAppointmentVM updateAppointmentVM)
        {
            InitializeComponent();
            this.updateAppointmentVM = updateAppointmentVM;
            DataContext = updateAppointmentVM;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AppointmentPage());
        }
    }
}
