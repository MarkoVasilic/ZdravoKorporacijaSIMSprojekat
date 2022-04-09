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
using System.Windows.Shapes;

namespace ZdravoKorporacija.View
{
    /// <summary>
    /// Interaction logic for PacijentHomePage.xaml
    /// </summary>
    public partial class PacijentHomePage : Window
    {
        public PacijentHomePage()
        {
            InitializeComponent();
        }

        private void ButtonCreateAppointment(object sender, RoutedEventArgs e)
        {
            CreateAppointmentPage createAppointmentPage = new CreateAppointmentPage(); 
            createAppointmentPage.Show();
        }



        private void DeleteAppointmentButton(object sender, RoutedEventArgs e)
        {
            DeleteAppointmentPage deleteAppointmentPage = new DeleteAppointmentPage(); 
            deleteAppointmentPage.Show();
        }

        private void UpdateAppointmentButton(object sender, RoutedEventArgs e)
        {
            UpdateAppointmentPage updateAppointmentPage = new UpdateAppointmentPage();
            updateAppointmentPage.Show();  
        }
    }
}
