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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for RoomJoining.xaml
    /// </summary>
    public partial class RoomJoining : Page
    {
        private RoomController roomController;
        public ObservableCollection<Room> Rooms { get; set; }
        private DateTime start;
        private DateTime end;
        private String durationToSend;
        private String _newRoomName = " ";
        private String _newRoomDescription = " ";
        private RoomType _newRoomType;
        private List<Room> selectedRooms;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public String NewRoomName
        {
            get { return _newRoomName; }
            set
            {
                _newRoomName = value;
                OnPropertyChanged("NewRoomName");
            }
        }


        public String NewRoomDescription
        {
            get { return _newRoomDescription; }
            set
            {
                _newRoomDescription = value;
                OnPropertyChanged("NewRoomDescription");
            }
        }

        public void setNewRoomType()
        {
            if (newRoomTypeComboBox.SelectedIndex == 0)
            {
                _newRoomType = RoomType.EXAMINATION;
            }
            else if (newRoomTypeComboBox.SelectedIndex == 1)
            {
                _newRoomType = RoomType.CONFERENCE;
            }
            else if (newRoomTypeComboBox.SelectedIndex == 2)
            {
                _newRoomType = RoomType.SURGERY;
            }
        }


        public RoomJoining(DateTime dateFrom, DateTime dateUntil, String duration)
        {
            InitializeComponent();
            RoomRepository roomRepository = new RoomRepository();
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);
            this.DataContext = this;
            Rooms = new ObservableCollection<Room>(roomController.GetAllRooms());
            start = dateFrom;
            end = dateUntil;
            selectedRooms = new List<Room>();
            durationToSend = duration;
        }

        private void GoBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void GoBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NavigationService.Navigate(new ChooseRenovationType());
        }

        private void PossibleAppoitments_Click(object sender, RoutedEventArgs e)
        {
     
            NavigationService.Navigate(new CreateJoining(selectedRooms[0].Id, selectedRooms[1].Id, start, end, durationToSend, NewRoomName, NewRoomDescription, _newRoomType));
           

        }


        private void CheckedBox_Checked(object sender, RoutedEventArgs e)
        {
            int id = (int)((CheckBox)sender).Tag;

            foreach (Room r in Rooms)
            {
                if (r.Id == id)
                    selectedRooms.Add(r);
            }
        }

        private void CheckBoxList_Unchecked(object sender, RoutedEventArgs e)
        {
            int id = (int)((CheckBox)sender).Tag;

            foreach (Room r in Rooms)
            {
                if (r.Id == id)
                    selectedRooms.Remove(r);
            }
        }
    }
}
