using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Repository;
using Service;

namespace ZdravoKorporacija.View.RoomCRUD
{
    public partial class CreateRoom : Window

    {

        private RoomType type;
        private RoomController roomController;
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

            if(name.Trim()== "")
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
            else if(comboBoxType.SelectedIndex == 1)
            {
                type = RoomType.CONFERENCE;
            }
            else if (comboBoxType.SelectedIndex == 2)
            {
                type = RoomType.STORAGE;
            }
            else if(comboBoxType.SelectedIndex == 3)
            {
                type = RoomType.SURGERY;
            }
            else
            {
                MessageBox.Show("Please select a type", "Error");
                return;
            }

            roomController.CreateRoom(name, description, type);
            this.Close();

        }
    }
}
