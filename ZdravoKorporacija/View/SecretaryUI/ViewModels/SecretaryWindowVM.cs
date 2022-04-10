using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using ZdravoKorporacija.View.SecretaryUI.Commands;

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
