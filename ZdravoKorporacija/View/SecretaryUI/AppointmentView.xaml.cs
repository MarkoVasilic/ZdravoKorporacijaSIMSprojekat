using System.Windows.Controls;
using ZdravoKorporacija.View.SecretaryUI.ViewModels;

namespace ZdravoKorporacija.View.SecretaryUI
{
    public partial class AppointmentView : Page
    {
        public AppointmentView()
        {
            InitializeComponent();

            DataContext = new AppointmentViemVM();
        }
    }
}
