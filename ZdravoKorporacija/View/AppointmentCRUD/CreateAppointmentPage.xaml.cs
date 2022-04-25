using System.Windows;
using System.Windows.Controls;
using ZdravoKorporacija.View.AppointmentCRUD;
using ZdravoKorporacija.View.AppointmentCRUD.ViewModels;

namespace ZdravoKorporacija.View
{
    /// <summary>
    /// Interaction logic for CreateAppointmentPage.xaml
    /// </summary>
    public partial class CreateAppointmentPage : Window
    {
        public CreateAppointmentPage()
        {
            InitializeComponent();
            DataContext = new CreateAppointmentVM();
        }

        private void ButtonBack(object sender, RoutedEventArgs e)
        {
            PacijentHomePage pacijentHomePage = new PacijentHomePage();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PossibleAppointmentPatientPage possibleAppointmentPatient = new PossibleAppointmentPatientPage();
            this.Content = possibleAppointmentPatient;
        }
    }
}
