using System.Windows.Controls;
using ZdravoKorporacija.View.SecretaryUI.ViewModels;

namespace ZdravoKorporacija.View.SecretaryUI
{
    public partial class ScheduleAppointmentView : Page
    {
        public ScheduleAppointmentView()
        {
            InitializeComponent();
            DataContext = new ScheduleAppointmentVM();
        }
    }
}
