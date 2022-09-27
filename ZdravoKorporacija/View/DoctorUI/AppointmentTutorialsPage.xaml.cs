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
using ZdravoKorporacija.View.DoctorUI.ViewModel;

namespace ZdravoKorporacija.View.DoctorUI
{
    /// <summary>
    /// Interaction logic for AppointmentTutorialsPage.xaml
    /// </summary>
    public partial class AppointmentTutorialsPage : Page
    {
        public AppointmentTutorialsPage()
        {
            InitializeComponent();
            DoctorWindowVM.setWindowTitle("How to manage appintments tutorial");
            this.DataContext = this;

        }

        private void b1_Click(object sender, RoutedEventArgs e)
        {
            mojVideo.Play();
            
        }

        private void b2_Click(object sender, RoutedEventArgs e)
        {
            mojVideo.Pause();
        }

        private void b3_Click(object sender, RoutedEventArgs e)
        {
            mojVideo.Stop();
        }

        private void b4_Click(object sender, RoutedEventArgs e)
        {
            mojVideo.Stop();
            DoctorWindowVM doctorWindowVm = new DoctorWindowVM();
            NavigationService.Navigate(new DoctorHomePage(doctorWindowVm));
        }
    }
}
