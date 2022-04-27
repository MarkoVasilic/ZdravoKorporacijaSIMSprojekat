using System.Windows.Controls;
using ZdravoKorporacija.View.SecretaryUI.ViewModels;

namespace ZdravoKorporacija.View.SecretaryUI
{
    /// <summary>
    /// Interaction logic for ScheduleEmergencyView.xaml
    /// </summary>
    public partial class ScheduleEmergencyView : Page
    {
        public ScheduleEmergencyView()
        {
            InitializeComponent();
            this.DataContext = new ScheduleEmergencyVM();
        }
    }
}
