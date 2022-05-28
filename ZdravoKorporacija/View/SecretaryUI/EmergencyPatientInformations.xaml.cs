using System.Windows.Controls;
using ZdravoKorporacija.View.SecretaryUI.ViewModels;

namespace ZdravoKorporacija.View.SecretaryUI
{
    /// <summary>
    /// Interaction logic for EmergencyPatientInformations.xaml
    /// </summary>
    public partial class EmergencyPatientInformations : Page
    {
        private ScheduleEmergencyVM scheduleEmergencyVM;
        public EmergencyPatientInformations(ScheduleEmergencyVM scheduleEmergencyVM)
        {
            InitializeComponent();
            this.scheduleEmergencyVM = scheduleEmergencyVM;
            DataContext = scheduleEmergencyVM;
        }
    }
}
