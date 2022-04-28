using System.Windows;
using ZdravoKorporacija.View.Equipment;
using ZdravoKorporacija.View.ManagerUI;

namespace ZdravoKorporacija.View.RoomCRUD
{

    public partial class ManagerHomePage : Window

    {

        public ManagerHomePage()
        {
            InitializeComponent();
        }

        private void CreateRoomClick(object sender, RoutedEventArgs e)
        {
            this.Close();

            CreateRoom createRoom = new CreateRoom();

            createRoom.Show();
        }

        private void DeleteRoomClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            DeleteRoom deleteRoom = new DeleteRoom();
            deleteRoom.Show();
        }

        private void AllRoomsClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            GetAllRooms getAllRooms = new GetAllRooms();
            getAllRooms.Show();
        }

        private void ModifyRoomClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            RoomsBeforeModification roomsBeforeModification = new RoomsBeforeModification();
            roomsBeforeModification.Show();
        }

        private void EquipmentClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            GetAllEquipment equipment = new GetAllEquipment();
            equipment.Show();
        }
    }
}
