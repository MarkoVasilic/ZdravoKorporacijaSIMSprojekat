using System.Windows.Controls;
using ZdravoKorporacija.View.SecretaryUI.ViewModels;

namespace ZdravoKorporacija.View.SecretaryUI
{
    public partial class ConfirmAppointmentInformations : Page
    {
        private ScheduleAppointmentVM scheduleAppointmentVM;
        private ScheduleEmergencyVM scheduleEmergencyVM;
        public ConfirmAppointmentInformations(ScheduleAppointmentVM scheduleAppointmentVM, ScheduleEmergencyVM scheduleEmergencyVM)
        {
            InitializeComponent();
            this.scheduleAppointmentVM = scheduleAppointmentVM;
            this.scheduleEmergencyVM = scheduleEmergencyVM;
            if (scheduleEmergencyVM == null)
                DataContext = scheduleAppointmentVM;
            else
                DataContext = scheduleEmergencyVM;
        }
    }
}
