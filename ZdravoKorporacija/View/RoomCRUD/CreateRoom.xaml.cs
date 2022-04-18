using Controller;
using Model;
using Repository;
using Service;
using System;
using System.Windows;

namespace ZdravoKorporacija.View.RoomCRUD
{
    public partial class CreateRoom : Window

    {

        private RoomType type;
        private RoomController roomController;
        private String errorMessage;
        public CreateRoom()
        {
            InitializeComponent();
            RoomRepository roomRepository = new RoomRepository();
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);
        }

        private void CreateRoomClick(object sender, RoutedEventArgs e)
        {
            String name = textBoxName.Text;

            if (name.Trim() == "")
            {
                MessageBox.Show("Please enter a name", "Error");
                return;
            }

            String description = textBoxDescription.Text;

            if (description.Trim() == "")
            {
                MessageBox.Show("Please enter a description", "Error");
                return;
            }

            if (comboBoxType.SelectedIndex == 0)
            {
                type = RoomType.EXAMINATION;
            }
            else if (comboBoxType.SelectedIndex == 1)
            {
                type = RoomType.CONFERENCE;
            }
            else if (comboBoxType.SelectedIndex == 2)
            {
                type = RoomType.STORAGE;
            }
            else if (comboBoxType.SelectedIndex == 3)
            {
                type = RoomType.SURGERY;
            }
            else
            {
                MessageBox.Show("Please select a type", "Error");
                return;
            }

            errorMessage = roomController.CreateRoom(name, description, type);

            if (errorMessage.Length == 0)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show(roomController.CreateRoom(name, description, type), "Error");
                this.Close();
            }

        }
    }
}
