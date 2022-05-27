using System.Windows.Controls;
using ZdravoKorporacija.View.SecretaryUI.ViewModels;

namespace ZdravoKorporacija.View.SecretaryUI
{
    /// <summary>
    /// Interaction logic for ConfirmMeetingInformantions.xaml
    /// </summary>
    public partial class ConfirmMeetingInformantions : Page
    {
        public ConfirmMeetingInformantions(ScheduleMeetingVM scheduleMeetingVm)
        {
            InitializeComponent();
            this.DataContext = scheduleMeetingVm;
        }
    }
}
