using System.Windows;
using ZdravoKorporacija.View.ManagerUI.ViewModels;

namespace ZdravoKorporacija.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for ManagerHomeWindow.xaml
    /// </summary>
    public partial class ManagerHomeWindow : Window
    {
        public ManagerHomeWindow()
        {

            InitializeComponent();
            DataContext = new ManagerWindowVM(this);
        }

    }
}
