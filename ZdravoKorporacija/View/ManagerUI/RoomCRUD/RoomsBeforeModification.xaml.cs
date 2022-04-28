using Controller;
using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ZdravoKorporacija.View.RoomCRUD
{
    public partial class RoomsBeforeModification : Window
    {

        private RoomController roomController;
        public ObservableCollection<Room> rooms { get; set; }
        public Room checkedRoom     { get; set; }
        public RoomsBeforeModification()
        {
            InitializeComponent();
            RoomRepository roomRepository = new RoomRepository();
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);
            this.DataContext = this;
            rooms = new ObservableCollection<Room>(roomController.GetAllRooms());
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            ManagerHomePage menagerHomePage = new ManagerHomePage();
            menagerHomePage.Show();
        }

        private void ModifyButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            ModifyRoom modifyRoom = new ModifyRoom(checkedRoom);
            modifyRoom.Show();
        }

        private void RadioButtonList_Checked(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(((RadioButton)sender).Tag);
            int id = (int)((RadioButton)sender).Tag ;

            foreach(Room r in rooms)
            {
                if (r.Id == id)
                    checkedRoom = r;
            }


        }
    }
}
