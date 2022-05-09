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

        private void Button_Click_Logout(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            this.Close();
            window.Show();
        }
    }
}
