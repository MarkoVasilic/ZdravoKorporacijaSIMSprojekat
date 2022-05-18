using System.Windows.Controls;
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
            this.DataContext = doctorWindowVM;
        }
    }
}
