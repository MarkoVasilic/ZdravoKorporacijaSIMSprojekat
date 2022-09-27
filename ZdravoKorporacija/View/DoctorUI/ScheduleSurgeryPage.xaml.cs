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
using Controller;
using Model;
using Repository;
using Service;
using ZdravoKorporacija.View.DoctorUI.ViewModel;

namespace ZdravoKorporacija.View.DoctorUI
{
    /// <summary>
    /// Interaction logic for ScheduleSurgeryPage.xaml
    /// </summary>
    public partial class ScheduleSurgeryPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string? propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private String errorMessage;
        public String ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }

        public RoomController roomController { get; set; }

        private String patientJmbg;
        public String PatientJmbg
        {
            get { return patientJmbg; }
            set
            {
                patientJmbg = value;
                OnPropertyChanged("PatientJmbg");
            }
        }

        private DateTime dateFrom;
        public DateTime DateFrom
        {
            get { return dateFrom; }
            set
            {
                dateFrom = value;
                OnPropertyChanged("DateFrom");
            }
        }

        private DateTime dateTo;
        public DateTime DateTo
        {
            get { return dateTo; }
            set
            {
                dateTo = value;
                OnPropertyChanged("DateTo");
            }
        }

        private int duration;
        public int Duration
        {
            get { return duration; }
            set
            {
                duration = value;
                OnPropertyChanged("Duration");
            }
        }

        private ObservableCollection<Room> rooms { get; set; }

        public ObservableCollection<Room> Rooms
        {
            get { return rooms; }
            set
            {
                rooms = value;
                OnPropertyChanged("Rooms");
            }
        }

        public ScheduleSurgeryPage()
        {
            InitializeComponent();
            DoctorWindowVM.setWindowTitle("Schedule surgery");
            this.ErrorMessage = "";
            RoomRepository roomRepository = new RoomRepository();
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);
            this.Rooms = new ObservableCollection<Room>(roomController.GetAllRooms());
            this.DataContext = this;

        }

        private void Confirm_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Room room = (Room)((Button)sender).CommandParameter;
                if (String.IsNullOrWhiteSpace(PatientJmbg))
                {
                    ErrorMessage = "Please enter patient jmbg to schedule appointment!";
                }else if (PatientJmbg.Length < 13)
                {
                    ErrorMessage = "Please enter 13 digits for patient jmbg!";
                }else if (room == null)
                {
                    ErrorMessage = "Room must be selected!";
                }
                else if (Duration < 1 || Duration == null)
                {
                    ErrorMessage = "Please insert duration in minutes!";
                }else
                {
                    int roomId = room.Id;
                    DoctorWindowVM.NavigationService.Navigate(new ChooseAppointmentPage(PatientJmbg, App.loggedUser.Jmbg,
                        DateFrom, DateTo, Duration, "doctor", roomId));
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
