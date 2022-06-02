using System.Windows;
using System.Windows.Navigation;
using ZdravoKorporacija.View.PatientUI;
using ZdravoKorporacija.View.PatientUI.ViewModels;
using ZdravoKorporacija.View.PatientUI;
using System;

namespace ZdravoKorporacija.View
{
    /// <summary>
    /// Interaction logic for PacijentHomePage.xaml
    /// </summary>
    public partial class PatientHomeWindow : Window
    {
        public static NavigationService NavigationService { get; set; }
        public String LoggedUser = App.loggedUser.FirstName + " " + App.loggedUser.LastName;

        public PatientHomeWindow()
        {
            InitializeComponent();
            DataContext = new PatientWindowVM(this);
            NavigationService = PatientMainFrame.NavigationService;
            ResizeMode = ResizeMode.NoResize;
            LoggedUser = App.loggedUser.FirstName + " " + App.loggedUser.LastName;

        }

        private void LogOutButton(object sender, RoutedEventArgs e)
        {
           var result = MessageBox.Show("Želite da se odjavite?","ODJAVI SE",MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MainWindow main = new MainWindow();
                this.Close();
                main.Show();
            }
        }

        private void HomeButton(object sender, RoutedEventArgs e)
        {
            PatientHomePage home = new PatientHomePage();
            NavigationService.Navigate(new PatientHomePage());
        }


        private void InfoButton2(object sender, RoutedEventArgs e)
        {
            PatientInfoPage patientInfo = new PatientInfoPage();
            NavigationService.Navigate(patientInfo);
        }

        private void infoButton(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            PatientInfoPage patientInfo = new PatientInfoPage();
            NavigationService.Navigate(patientInfo);
        }


        // Treba ubaciti i material design i textbox u button i tek onda horizontal aligment left


        // <StackPanel HorizontalAlignment="Left">
        //<Button Height = "70" Width="270" Margin="0 0 0 0" Click="InfoButton" Content="Moj Profil" FontSize="20" FontWeight="Bold" Background="SteelBlue"></Button>
        //</StackPanel>
    }
}
