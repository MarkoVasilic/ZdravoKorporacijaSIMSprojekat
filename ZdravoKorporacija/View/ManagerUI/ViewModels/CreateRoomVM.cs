using Controller;
using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using ZdravoKorporacija.View.ManagerUI.Help;
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

        Page page;

        public CreateRoomVM(Page page)
        {
            this.page = page;
            Room = new Room();
            RoomRepository roomRepository = new RoomRepository();
            RoomService roomService = new RoomService(roomRepository);
            RoomController = new RoomController(roomService);
            SaveCommand = new RelayCommand(saveExecute);
            GetAllRoomsCommand = new RelayCommand(getAllRoomsManager);
            Rooms = new ObservableCollection<Room>(RoomController.GetAllRooms());

            page.CommandBindings.Add(new CommandBinding(ZdravoKorporacija.View.ManagerUI.Commands.RoutedCommand.Help, CreateRoomHelp_Executed, CreateRoomHelp_CanExecute));

            page.CommandBindings.Add(new CommandBinding(ZdravoKorporacija.View.ManagerUI.Commands.RoutedCommand.Back, GoBack_Executed, GoBack_CanExecute));

        }

        public ICommand SaveCommand { get; set; }

        public ICommand GetAllRoomsCommand { get; set; }


        private void saveExecute(object parameter)
        {

            try
            {
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

        private void CreateRoomHelp_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CreateRoomHelp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CreateRoomHelp createRoomHelp = new CreateRoomHelp();
            createRoomHelp.Show();
        }

        private void GoBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void GoBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Console.WriteLine("Nadja");
            page.NavigationService.Navigate(new ManagerHomePage());
        }

    }
}
