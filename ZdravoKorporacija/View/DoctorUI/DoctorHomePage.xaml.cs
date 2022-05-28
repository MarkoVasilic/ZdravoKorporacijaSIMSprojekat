using Controller;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.View.DoctorUI.ViewModel;

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
