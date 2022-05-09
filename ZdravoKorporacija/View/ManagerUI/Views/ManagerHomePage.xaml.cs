using System.Windows;
using System.Windows.Controls;
using ZdravoKorporacija.View.Equipment;
using ZdravoKorporacija.View.ManagerUI.Views;

namespace ZdravoKorporacija.View.RoomCRUD
{

    public partial class ManagerHomePage : Page

    {

        public ManagerHomePage()
        {
            InitializeComponent();
        }

        private void CreateRoomClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateRoom());
        }

        private void DeleteRoomClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DeleteRoom());
        }

        private void AllRoomsClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GetAllRooms());
        }

        private void ModifyRoomClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RoomsBeforeModification());
        }

        private void EquipmentClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GetAllEquipment());
        }
    }
}
