using System.Windows;
using System.Windows.Navigation;
using ZdravoKorporacija.View.AppointmentCRUD;
using ZdravoKorporacija.View.AppointmentCRUD.ViewModels;
using ZdravoKorporacija.View.PatientUI;

namespace ZdravoKorporacija.View
{
    /// <summary>
    /// Interaction logic for PacijentHomePage.xaml
    /// </summary>
    public partial class PatientHomeWindow : Window
    {
        public static NavigationService NavigationService { get; set; }
        
        public PatientHomeWindow()
        {
            InitializeComponent();
            DataContext = new PatientWindowVM(this);
            NavigationService = PatientMainFrame.NavigationService;
            ResizeMode = ResizeMode.NoResize;

        }

        private void LogOutButton(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Close();
            main.Show();
        }

        private void HomeButton(object sender, RoutedEventArgs e)
        {
            PatientHomePage home = new PatientHomePage();
            NavigationService.Navigate(home);
        }

        private void InfoButton(object sender, RoutedEventArgs e)
        {
            PatientInfoPage patientInfo = new PatientInfoPage();
            NavigationService.Navigate(patientInfo);
        }
    }
}
