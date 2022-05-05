using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ZdravoKorporacija.View.SecretaryUI.ViewModels;

namespace ZdravoKorporacija.View.SecretaryUI
{
    public partial class SecretaryHomePage : Page
    {
        public SecretaryHomePage(SecretaryWindowVM secretaryWindowVM)
        {
            SecretaryWindowVM.setWindowTitle("Dashboard");
            InitializeComponent();
            DataContext = secretaryWindowVM;
        }
    }
}
