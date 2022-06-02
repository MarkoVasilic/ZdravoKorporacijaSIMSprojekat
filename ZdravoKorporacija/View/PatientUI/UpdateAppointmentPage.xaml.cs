using System.Windows;
using System.Windows.Controls;
using ZdravoKorporacija.View.PatientUI;
using ZdravoKorporacija.View.PatientUI.ViewModels;

namespace ZdravoKorporacija.View
{
    /// <summary>
    /// Interaction logic for UpdateAppointmentPage.xaml
    /// </summary>
    public partial class UpdateAppointmentPage : Page
    {
        public UpdateAppointmentPage(int id)
        {
            InitializeComponent();
            DataContext = new  UpdateAppointmentVM(id);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PatientHomePage());
        }
    }
}
