using System.Windows;
using ZdravoKorporacija.View;
using ZdravoKorporacija.View.RoomCRUD;
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

        private void Button_Click_Patient(object sender, RoutedEventArgs e)
        {
            PatientHomeWindow patientHomePage = new PatientHomeWindow();
            patientHomePage.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
