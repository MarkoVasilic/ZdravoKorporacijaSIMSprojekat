using Controller;
using Model;
using Repository;
using Service;
using System.Collections.ObjectModel;
using System.Windows;

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
