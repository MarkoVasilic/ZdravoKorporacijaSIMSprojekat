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
        private String errorMessage;

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
                errorMessage = roomController.DeleteRoom(roomId);

                if (errorMessage.Length == 0)
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show(roomController.DeleteRoom(roomId), "Error");
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show(roomController.DeleteRoom(roomId), "Error");
                this.Close();
            }

        }
    }
}
