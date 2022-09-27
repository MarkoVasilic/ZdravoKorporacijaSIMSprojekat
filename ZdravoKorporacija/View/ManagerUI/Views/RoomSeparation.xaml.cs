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
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for RoomSeparation.xaml
    /// </summary>
    public partial class RoomSeparation : Page
    {

        private int checkedRoomId;
        private RoomController roomController;
        public ObservableCollection<Room> Rooms { get; set; }
        private DateTime start;
        private DateTime end;
        private String durationToSend;
        private String _firstRoomName = " ";
        private String _secondRoomName = " ";
        private String _firstRoomDescription = " ";
        private String _secondRoomDescription = " ";
        private RoomType _firstRoomType;
        private RoomType _secondRoomType;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

    
        public String FirstRoomName
        {
            get { return _firstRoomName; }
            set
            { 
                _firstRoomName = value;
                OnPropertyChanged("FirstRoomName");
            }
        }


        public String SecondRoomName
        {
            get { return _secondRoomName; }
            set
            {
                _secondRoomName = value;
                OnPropertyChanged("SecondRoomName");
            }
        }

        public String FirstRoomDescription
        {
            get { return _firstRoomDescription; }
            set
            {
                _firstRoomDescription = value;
                OnPropertyChanged("FirstRoomDescription");
            }
        }


        public String SecondRoomDescription
        {
            get { return _secondRoomDescription; }
            set
            {
                _secondRoomDescription = value;
                OnPropertyChanged("SecondRoomDescription");
            }
        }


        public RoomSeparation(DateTime dateFrom, DateTime dateUntil, String duration)
        {
            InitializeComponent();
            RoomRepository roomRepository = new RoomRepository();
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);
            this.DataContext = this;
            Rooms = new ObservableCollection<Room>(roomController.GetAllRooms());
            start = dateFrom;
            end = dateUntil;
            durationToSend = duration;
        }
        

        public void setFirstRoomType()
        {
            if (firstRoomTypeComboBox.SelectedIndex == 0)
            {
                _firstRoomType = RoomType.EXAMINATION;
            }
            else if (firstRoomTypeComboBox.SelectedIndex == 1)
            {
                _firstRoomType = RoomType.CONFERENCE;
            }
            else if (firstRoomTypeComboBox.SelectedIndex == 2)
            {
                _firstRoomType = RoomType.SURGERY;
            }
        }


        public void setSecondRoomType()
        {

            if (secondRoomTypeComboBox.SelectedIndex == 0)
            {
                _secondRoomType = RoomType.EXAMINATION;
            }
            else if (secondRoomTypeComboBox.SelectedIndex == 1)
            {
                _secondRoomType = RoomType.CONFERENCE;
            }
            else if (secondRoomTypeComboBox.SelectedIndex == 2)
            {
                _secondRoomType = RoomType.SURGERY;
            }
        }

        private void PossibleAppoitments_Click(object sender, RoutedEventArgs e)
        {
                setFirstRoomType();
                setSecondRoomType();
                NavigationService.Navigate(new CreateSeparation(checkedRoomId, start, end, durationToSend, FirstRoomName, FirstRoomDescription, _firstRoomType, SecondRoomName, SecondRoomDescription, _secondRoomType));
        }

        private void GoBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void GoBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NavigationService.Navigate(new ChooseRenovationType());
        }

        private void RadioButtonList_Checked(object sender, RoutedEventArgs e)
        {
            int id = (int)((RadioButton)sender).Tag;

            foreach (Room r in Rooms)
            {
                if (r.Id == id)
                    checkedRoomId = id;
            }

        }



    }
}
