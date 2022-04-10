using System.Windows;
using ZdravoKorporacija.View;
using ZdravoKorporacija.View.SecretaryUI;

namespace ZdravoKorporacija
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Secretary(object sender, RoutedEventArgs e)
        {
            SecretaryWindow secretaryWindow = new SecretaryWindow();
            secretaryWindow.Show();
        }

        private void Button_Click_Manager(object sender, RoutedEventArgs e)
        {
           ManagerHomePage managerHomePage = new ManagerHomePage();
           managerHomePage.Show();
        }
    }
}
