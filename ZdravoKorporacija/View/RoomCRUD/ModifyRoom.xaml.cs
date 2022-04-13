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
            int roomId = int.Parse(textBoxId.Text);

            String name = textBoxName.Text;
            
            if(name.Trim() == "")
            {
                MessageBox.Show("Please enter a name", "Error");
            }

            String description = textBoxDescription.Text;

            if(description.Trim() == "")
            {
                MessageBox.Show("Please enter a description", "Error");

            }


            roomController.ModifyRoom(roomId, name, description);

            this.Close();

        }
    }
}
