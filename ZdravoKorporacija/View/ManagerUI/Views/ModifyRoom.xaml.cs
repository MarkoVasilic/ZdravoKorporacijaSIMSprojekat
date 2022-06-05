using Controller;
using Model;
using Repository;
using Service;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ZdravoKorporacija.View.RoomCRUD
{
    public partial class ModifyRoom : Page
    {

        private RoomController roomController;
        private Room room;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string name = "";
        private string description = "";
        private int roomId;
        private string errorMessage;

        public int RoomId
        {
            get { return roomId; }
            set
            {
                roomId = value;
                OnPropertyChanged("RoomId");
            }
        }

        public string NameRoom
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
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


        public ModifyRoom(int roomId)
        {

            InitializeComponent();
            RoomRepository roomRepository = new RoomRepository();
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);
            textBoxName.Focus();
            room = roomController.GetRoomById(roomId);
            if (room is not null)
            {
                RoomId = room.Id;
                NameRoom = Room.Name;
                Description = Room.Description;
            }
            else
            {
                NavigationService.GoBack();
            }
            this.DataContext = this;

        }


        private void Button_ModifyRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                roomController.ModifyRoom(RoomId, NameRoom, Description);
                MessageBox.Show("Prostorija je uspešno modifikovana.", "Obaveštenje", MessageBoxButton.OK);
                NavigationService.Navigate(new RoomsBeforeModification());

            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                MessageBox.Show(ErrorMessage, "Greška");
            }
        }


        public void GoBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void GoBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NavigationService.Navigate(new RoomsBeforeModification());
        }
    }
}
