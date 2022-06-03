    using System.Windows;
using System.Windows.Controls;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.View.PatientUI.ViewModels;

namespace ZdravoKorporacija.View.PatientUI
{
    /// <summary>
    /// Interaction logic for PossibleAppointmentPatientPage.xaml
    /// </summary>
    public partial class PossibleAppointmentPatientPage : Page
    {
        private CreateAppointmentVM CreateAppointmentVM;
        public PossibleAppointmentPatientPage(CreateAppointmentVM createAppointmentVM)
        {
            CreateAppointmentVM = createAppointmentVM;
            InitializeComponent();
            DataContext = CreateAppointmentVM;
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {

            PossibleAppointmentsDTO obj = ((FrameworkElement)sender).DataContext as PossibleAppointmentsDTO;
            CreateAppointmentVM.SelectedAppointment = obj;
            CreateAppointmentVM.SelectAppointment();
        }

        private void NazadButton(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PatientHomePage());
        }
    }
}
