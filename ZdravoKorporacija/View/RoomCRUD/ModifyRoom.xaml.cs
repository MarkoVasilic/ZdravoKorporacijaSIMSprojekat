using Controller;
using Repository;
using Service;
using System;
using System.Windows;

namespace ZdravoKorporacija.View.RoomCRUD
{
    public partial class ModifyRoom : Window
    {

        private RoomController roomController;

        public ModifyRoom()
        {
            InitializeComponent();
            InitializeComponent();
            RoomRepository roomRepository = new RoomRepository();
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);
        }

        private void ModifyCLick(object sender, RoutedEventArgs e)
        {
            try
            {
                int roomId = int.Parse(textBoxId.Text);

                String name = textBoxName.Text;
                String description = textBoxDescription.Text;
                if (name.Trim() == "")
                {
                    MessageBox.Show("Please enter a name", "Error");
                }



                if (description.Trim() == "")
                {
                    MessageBox.Show("Please enter a description", "Error");

                }

                roomController.ModifyRoom(roomId, name, description);

                this.Close();
            }
            catch
            {
                MessageBox.Show("Error");
                this.Close();
            }



        }
    }
}
