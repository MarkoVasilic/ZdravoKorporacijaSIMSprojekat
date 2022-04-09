using System.Windows;
using ZdravoKorporacija.View;

namespace ZdravoKorporacija
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_ClickPacijent(object sender, RoutedEventArgs e)
        {
            PacijentHomePage pacijentHomePage = new PacijentHomePage();
            pacijentHomePage.Show();
        }

        private void Button_Click_Secretary(object sender, RoutedEventArgs e)
        {
            SecretaryAddAccountWindow secretaryAddAccountWindow = new SecretaryAddAccountWindow();
            secretaryAddAccountWindow.Show();
        }

        private void Button_Click_Manager(object sender, RoutedEventArgs e)
        {
           ManagerHomePage managerHomePage = new ManagerHomePage();
           managerHomePage.Show();
        }
    }
}
