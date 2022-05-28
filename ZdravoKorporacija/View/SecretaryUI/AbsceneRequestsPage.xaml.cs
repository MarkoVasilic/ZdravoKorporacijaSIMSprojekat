using System.Windows.Controls;
using ZdravoKorporacija.View.SecretaryUI.ViewModels;

namespace ZdravoKorporacija.View.SecretaryUI
{
    /// <summary>
    /// Interaction logic for AbsceneRequestsPage.xaml
    /// </summary>
    public partial class AbsceneRequestsPage : Page
    {
        public AbsceneRequestsPage()
        {
            InitializeComponent();
            this.DataContext = new AbsceneRequestsVM();
        }
    }
}
