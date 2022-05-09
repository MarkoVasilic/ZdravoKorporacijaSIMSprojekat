using System.Windows.Controls;
using ZdravoKorporacija.View.SecretaryUI.ViewModels;

namespace ZdravoKorporacija.View.SecretaryUI
{
    public partial class ModifyAppointmentView : Page
    {
        private AppointmentViemVM AppointmentViemVM;
        public ModifyAppointmentView(AppointmentViemVM appointmentViemVM)
        {
            InitializeComponent();
            this.AppointmentViemVM = appointmentViemVM;
            DataContext = appointmentViemVM;
        }
    }
}
