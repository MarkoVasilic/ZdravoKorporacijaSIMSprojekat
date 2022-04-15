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
using Model;
using Controller;
using Repository;
using Service;

namespace ZdravoKorporacija.View.RoomCRUD
{
    public partial class ModifyRoom : Window
    {
  
        private RoomController roomController;
        private String errorMessage;

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

                errorMessage = roomController.ModifyRoom(roomId, name, description);

                if (errorMessage.Length == 0)
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show(roomController.ModifyRoom(roomId, name, description), "Error");
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("Error");
                this.Close();
            }

            

        }
    }
}
