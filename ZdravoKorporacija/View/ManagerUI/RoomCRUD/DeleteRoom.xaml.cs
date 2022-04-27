using Controller;
using Repository;
using Service;
using System;
using System.Windows;

namespace ZdravoKorporacija.View.RoomCRUD
{

    public partial class DeleteRoom : Window
    {

        private RoomController roomController;
        private int roomId;

        public DeleteRoom()
        {
            InitializeComponent();
            RoomRepository roomRepository = new RoomRepository();
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);
        }

        private void DeleteRoomClick(object sender, RoutedEventArgs e)
        {
            try
            {
                roomId = int.Parse(textBoxDeleteRoom.Text);
                roomController.DeleteRoom(roomId);
                this.Close();
                ManagerHomePage managerHome = new ManagerHomePage();
                managerHome.Show();
            }
            catch
            {

                MessageBox.Show("Error");
                this.Close();
                ManagerHomePage managerHomePage = new ManagerHomePage();    
                managerHomePage.Show();
            }

        }
    }
}
