using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
            NavigationService.GoBack();
        }
    }
}
