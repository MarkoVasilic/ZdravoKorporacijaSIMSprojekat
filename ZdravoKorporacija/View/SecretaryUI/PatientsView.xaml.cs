using System.Windows.Controls;
using ZdravoKorporacija.View.SecretaryUI.ViewModels;

namespace ZdravoKorporacija.View.SecretaryUI
{
    /// <summary>
    /// Interaction logic for PatientsView.xaml
    /// </summary>
    public partial class PatientsView : Page
    {
        public PatientsView()
        {
            InitializeComponent();
            DataContext = new PatientsViewVM();
        }

    }
}
