using Controller;
using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ZdravoKorporacija.View.PatientUI.Commands;
using ZdravoKorporacija.View.RoomCRUD;

namespace ZdravoKorporacija.View.ManagerUI.ViewModels
{
    public class CreateRoomVM : INotifyPropertyChanged
    {
        private Room room;
        private string name;
        private string description;
        private string errorMessage;

        public event PropertyChangedEventHandler PropertyChanged;

        public RoomController RoomController { get; set; }
        public ObservableCollection<Room> rooms;


        public Room Room
        {
            get
            {
                return room;
            }
            set
            {
                room = value;
                OnPropertyChanged("Room");
            }
        }
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Username");
            }
        }



        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }
            set
            {
                errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }

        public void setRoomType(RoomType roomType)
        {
            Room.Type = roomType;
        }

        public ObservableCollection<Room> Rooms
        {
            get
            {
                return rooms;
            }
            set
            {
                rooms = value;
                OnPropertyChanged("Rooms");
            }
        }

        public CreateRoomVM()
        {
            Room = new Room();
            RoomRepository roomRepository = new RoomRepository();
            RoomService roomService = new RoomService(roomRepository);
            RoomController = new RoomController(roomService);
            SaveCommand = new RelayCommand(saveExecute);
            GetAllRoomsCommand = new RelayCommand(getAllRoomsManager);
            Rooms = new ObservableCollection<Room>(RoomController.GetAllRooms());

        }

        public ICommand SaveCommand { get; set; }

        public ICommand GetAllRoomsCommand { get; set; }


        private void saveExecute(object parameter)
        {

            try
            {
                //Console.WriteLine(Room.Name);
                //Console.WriteLine(Room.Description);
                //Console.WriteLine(Room.Type);
                RoomController.CreateRoom(Room.Name, Room.Description, Room.Type);

                MessageBox.Show("Prostorija je uspešno kreirana.", "Obaveštenje", MessageBoxButton.OK);

                ManagerWindowVM.NavigationService.Navigate(new GetAllRooms());

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                MessageBox.Show(ErrorMessage, "Greška");
            }
        }



        private void getAllRoomsManager(object sender)
        {
            roomsListToRoomList(RoomController.GetAllRooms());

        }



        private void roomsListToRoomList(List<Room> roomsToAdd)
        {
            Rooms = new ObservableCollection<Room>();
            foreach (var r in roomsToAdd)
            {
                Rooms.Add(r);
            }
        }
    }
}
