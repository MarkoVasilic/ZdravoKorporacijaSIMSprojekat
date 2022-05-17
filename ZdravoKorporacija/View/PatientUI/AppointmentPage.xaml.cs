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

namespace ZdravoKorporacija.View.PatientUI
{
    /// <summary>
    /// Interaction logic for AppointmentPage.xaml
    /// </summary>
    public partial class AppointmentPage : Page
    {
        public AppointmentPage()
        {
           
            InitializeComponent();
            
        }

        private void ButtonCreateAppointment(object sender, RoutedEventArgs e)
        {
          //  NavigationService.Navigate(new CreateAppointmentPage()); 

        }



        private void DeleteAppointmentButton(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new DeleteAppointmentPage());
        }

        private void UpdateAppointmentButton(object sender, RoutedEventArgs e)
        {

           NavigationService.Navigate(new UpdateFutureAppointmentsPage());
        }

        private void MyAppointmentsButton(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new GetAllAppointmentsPatient());
        }

        private void GoHomeButton(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PatientHomePage()); 
        }
    }
}
