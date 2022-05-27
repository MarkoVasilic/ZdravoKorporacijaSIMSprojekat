using System.Windows.Controls;
using ZdravoKorporacija.View.SecretaryUI.ViewModels;

namespace ZdravoKorporacija.View.SecretaryUI
{
    /// <summary>
    /// Interaction logic for CheckScheduledMeetingsPage.xaml
    /// </summary>
    public partial class CheckScheduledMeetingsPage : Page
    {
        public CheckScheduledMeetingsPage()
        {
            InitializeComponent();
            this.DataContext = new CheckScheduledMeetingsVM();
        }
    }
}
