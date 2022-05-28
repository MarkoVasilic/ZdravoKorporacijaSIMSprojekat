using System.Windows;
using System.Windows.Controls;
using ZdravoKorporacija.View.PatientUI.ViewModels;

namespace ZdravoKorporacija.View
{
    /// <summary>
    /// Interaction logic for CreateAppointmentPage.xaml
    /// </summary>
    public partial class CreateAppointmentPage : Page
    {

        public CreateAppointmentPage()
        {
            InitializeComponent();
            DataContext = new CreateAppointmentVM();
        }

        private void ButtonBack(object sender, RoutedEventArgs e)
        {

            NavigationService.GoBack();
        }

    }
}
