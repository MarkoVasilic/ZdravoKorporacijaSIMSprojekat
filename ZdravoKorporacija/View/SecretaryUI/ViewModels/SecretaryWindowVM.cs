using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Navigation;
using ZdravoKorporacija.View.SecretaryUI.Commands;

namespace ZdravoKorporacija.View.SecretaryUI.ViewModels
{
    internal class SecretaryWindowVM
    {
        public static SecretaryWindow? SecretaryWindow;
        public static SecretaryHomePage SecretaryHomePage;
        public static NavigationService? NavigationService { get; set; }
        public ICommand HomeCommand { get; set; }

        public static void setWindowTitle(string newTitle)
        {
            SecretaryWindow.WindowTitle.Text = newTitle;
        }

        public SecretaryWindowVM(SecretaryWindow secretaryWindow)
        {
            SecretaryWindow = secretaryWindow;
            SecretaryHomePage = new SecretaryHomePage();
            SecretaryWindow.SecretaryMainFrame.Content = SecretaryHomePage;
            NavigationService = SecretaryWindow.SecretaryMainFrame.NavigationService;
            HomeCommand = new RelayCommand(homeExecute);
        }

        private void homeExecute(object parameter)
        {
            setWindowTitle("Dashboard");
            NavigationService.Navigate(SecretaryHomePage);
        }
    }
}
