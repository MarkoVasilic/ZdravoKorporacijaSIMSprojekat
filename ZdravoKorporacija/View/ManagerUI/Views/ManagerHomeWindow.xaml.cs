using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using ZdravoKorporacija.View.ManagerUI.ViewModels;

namespace ZdravoKorporacija.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for ManagerHomeWindow.xaml
    /// </summary>
    public partial class ManagerHomeWindow : Window
    {


        public static NavigationService? NavigationService { get; set; }

        public ManagerHomeWindow()
        {

            InitializeComponent();
            DataContext = new ManagerWindowVM(this);
            NavigationService = this.ManagerMainFrame.NavigationService;

        }

        private void Logout_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Logout_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            this.Close();
            window.Show();
        }

    }
}
