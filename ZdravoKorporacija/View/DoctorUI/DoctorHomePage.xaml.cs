using System.Windows.Controls;
using ZdravoKorporacija.View.DoctorUI.ViewModel;

namespace ZdravoKorporacija.View.DoctorUI
{
    /// <summary>
    /// Interaction logic for DoctorHomePage.xaml
    /// </summary>
    public partial class DoctorHomePage : Page
    {
        public DoctorHomePage(DoctorWindowVM doctorWindowVM)
        {
            DoctorWindowVM.setWindowTitle("Appointment Schendule");
            InitializeComponent();
            DataContext = doctorWindowVM;
        }
    }
}
