using Controller;
using Model;
using Repository;
using Service;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using ZdravoKorporacija.View.ManagerUI.Help;
using ZdravoKorporacija.View.RoomCRUD;

namespace ZdravoKorporacija.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for DeleteRoom.xaml
    /// </summary>
    public partial class DeleteRoom : Page
    {

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public RoomController roomController;

        private ObservableCollection<Room> rooms;
        public ObservableCollection<Room> Rooms
        {
            get => rooms;
            set
            {
                if (rooms != value)
                {
                    rooms = value;
                    OnPropertyChanged("Rooms");
                }
            }
        }


        public DeleteRoom()
        {
            InitializeComponent();
            RoomRepository roomRepository = new RoomRepository();
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);
            Rooms = new ObservableCollection<Room>(roomController.GetAllRooms());
            this.DataContext = this;

        }

        /* private void Button_Back_Click(object sender, RoutedEventArgs e)
         {
             NavigationService.Navigate(new ManagerHomePage());
         }*/


        private void DeleteRoomClick(object sender, RoutedEventArgs e)
        {
            int roomId = (int)((Button)sender).Tag;
            if (roomId == null) return;
            roomController.DeleteRoom(roomId);
            MessageBox.Show("Prostorija je uspešno obrisana.", "Obaveštenje", MessageBoxButton.OK);
            NavigationService.Navigate(new DeleteRoom());
            Rooms = new ObservableCollection<Room>(roomController.GetAllRooms());
        }

        private void Button_Click_Logout(object sender, RoutedEventArgs e)
        {

        }

        public void DeleteRoomHelp_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void DeleteRoomHelp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DeleteRoomHelp deleteRoomHelp = new DeleteRoomHelp();
            deleteRoomHelp.Show();
        }

        public void GoBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void GoBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerHomePage());
        }

    }

}
