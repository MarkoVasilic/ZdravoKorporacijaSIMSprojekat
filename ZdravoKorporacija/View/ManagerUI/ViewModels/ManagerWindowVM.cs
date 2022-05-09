using System.Windows.Navigation;
using ZdravoKorporacija.View.ManagerUI.Views;
using ZdravoKorporacija.View.RoomCRUD;

namespace ZdravoKorporacija.View.ManagerUI.ViewModels
{
    internal class ManagerWindowVM
    {

        public static ManagerHomeWindow? ManagerHomeWindow;
        public static ManagerHomePage ManagerHomePage;
        public static NavigationService? NavigationService { get; set; }

        public ManagerWindowVM(ManagerHomeWindow managerHomeWindow)
        {
            ManagerHomeWindow = managerHomeWindow;
            ManagerHomePage = new ManagerHomePage();
            ManagerHomeWindow.ManagerMainFrame.Content = ManagerHomePage;
            NavigationService = ManagerHomeWindow.ManagerMainFrame.NavigationService;
        }
    }
}
