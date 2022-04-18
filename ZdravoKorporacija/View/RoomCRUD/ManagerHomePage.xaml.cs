using System.Windows;

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
            CreateRoom createRoom = new CreateRoom();
            createRoom.Show();
        }

        private void DeleteRoomClick(object sender, RoutedEventArgs e)
        {
            DeleteRoom deleteRoom = new DeleteRoom();
            deleteRoom.Show();
        }

        private void AllRoomsClick(object sender, RoutedEventArgs e)
        {
            GetAllRooms getAllRooms = new GetAllRooms();
            getAllRooms.Show();
        }

        private void ModifyRoomClick(object sender, RoutedEventArgs e)
        {
            ModifyRoom modifyRoom = new ModifyRoom();
            modifyRoom.Show();
        }
    }
}
