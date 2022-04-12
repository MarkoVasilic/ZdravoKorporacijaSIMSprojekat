using System.Windows.Navigation;

namespace ZdravoKorporacija.View.SecretaryUI.ViewModels
{
    internal class SecretaryWindowVM
    {
        public static SecretaryWindow? SecretaryWindow;
        public static NavigationService? NavigationService { get; set; }

        public SecretaryWindowVM(SecretaryWindow secretaryWindow)
        {
            SecretaryWindow = secretaryWindow;
            SecretaryWindow.SecretaryMainFrame.Content = new SecretaryHomePage();
            NavigationService = SecretaryWindow.SecretaryMainFrame.NavigationService;
        }


    }
}
