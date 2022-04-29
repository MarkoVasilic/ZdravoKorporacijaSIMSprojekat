using System.Windows;
using System.Windows.Controls;
using ZdravoKorporacija.View.Equipment;
using ZdravoKorporacija.View.ManagerUI;
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
            //this.Close();
            //DeleteRoom deleteRoom = new DeleteRoom();
            //deleteRoom.Show();
        }

        private void AllRoomsClick(object sender, RoutedEventArgs e)
        {
            //this.Close();
            //GetAllRooms getAllRooms = new GetAllRooms();
            //getAllRooms.Show();
        }

        private void ModifyRoomClick(object sender, RoutedEventArgs e)
        {
           // this.Close();
            //RoomsBeforeModification roomsBeforeModification = new RoomsBeforeModification();
            //roomsBeforeModification.Show();
        }

        private void EquipmentClick(object sender, RoutedEventArgs e)
        {
            //this.Close();
            //GetAllEquipment equipment = new GetAllEquipment();
            //equipment.Show();
        }
    }
}
