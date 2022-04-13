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
using Controller;
using Model;
using Repository;
using Service;

namespace ZdravoKorporacija.View.RoomCRUD
{

    public partial class GetAllRooms : Window
    {

        private RoomController roomController;
        public ObservableCollection<Room> rooms { get; set; }

        public GetAllRooms()
        {
            InitializeComponent();
            RoomRepository roomRepository = new RoomRepository();
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);
            this.DataContext = this;
            rooms = new ObservableCollection<Room>(roomController.GetAllRooms());
        }
    }
}
