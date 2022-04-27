using Controller;
using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZdravoKorporacija.View.AppointmentCRUD.Commands;
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

        public CreateRoomVM()
        {
            Room = new Room();
            RoomRepository roomRepository = new RoomRepository();
            RoomService roomService = new RoomService(roomRepository);
            RoomController = new RoomController(roomService);
            SaveCommand = new RelayCommand(saveExecute);
        }

        public ICommand SaveCommand    { get; set; }


        private void saveExecute(object parameter)
        {
            
            try
            {
                RoomController.CreateRoom(Room.Name, Room.Description, Room.Type); //da li ovako kupim ovo sve (zapravo da li je xaml dobar?)
                //otvaram novi prozor
         
                GetAllRooms getAllRooms = new GetAllRooms();
                getAllRooms.Show();
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
            }

        }
    }
}
