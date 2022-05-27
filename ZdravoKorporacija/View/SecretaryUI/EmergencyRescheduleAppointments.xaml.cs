using System.Windows.Controls;
using ZdravoKorporacija.View.SecretaryUI.ViewModels;

namespace ZdravoKorporacija.View.SecretaryUI
{
    /// <summary>
    /// Interaction logic for EmergencyRescheduleAppointments.xaml
    /// </summary>
    public partial class EmergencyRescheduleAppointments : Page
    {
        private ScheduleEmergencyVM scheduleEmergencyVM;
        public EmergencyRescheduleAppointments(ScheduleEmergencyVM scheduleEmergencyVM)
        {
            InitializeComponent();
            this.scheduleEmergencyVM = scheduleEmergencyVM;
            DataContext = scheduleEmergencyVM;
        }
    }
}
