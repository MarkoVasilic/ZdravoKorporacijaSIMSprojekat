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
using Controller;
using Service;
using ZdravoKorporacija.DTO;
using System.Collections.ObjectModel;

namespace ZdravoKorporacija.View.DoctorUI
{
    /// <summary>
    /// Interaction logic for DoctorHomePage.xaml
    /// </summary>
    public partial class DoctorHomePage : Page
    {
        private AppointmentController AppointmentController;

        public ObservableCollection<AppointmentDTO> appointments { get; set; }
        public DoctorHomePage(DoctorWindowVM doctorWindowVM)
        {
            DoctorWindowVM.setWindowTitle("Appointment Schedule");
            InitializeComponent();
            DataContext = doctorWindowVM;
        }
    }
}
