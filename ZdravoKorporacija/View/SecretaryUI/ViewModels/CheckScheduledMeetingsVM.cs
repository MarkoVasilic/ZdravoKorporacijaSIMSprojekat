using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Controller;
using Model;
using Repository;
using Service;
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.View.SecretaryUI.Commands;

namespace ZdravoKorporacija.View.SecretaryUI.ViewModels
{
    public class CheckScheduledMeetingsVM: INotifyPropertyChanged
    {
        private DoctorController doctorController { get; set; }
        private ManagerController managerController { get; set; }
        private SecretaryController secretaryController { get; set; }
        private RoomController roomController { get; set; }
        private MeetingControler meetingControler { get; set; }
        private ObservableCollection<Doctor>  doctors;
        private Doctor selectedDoctor;
        private ObservableCollection<Room> rooms;
        private Room selectedRoom;
        private DateTime selectedDate;
        private ObservableCollection<PossibleMeetingDTO> meetings;
        private String meetingsVisibility;
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Doctor> Doctors
        {
            get => doctors;
            set
            {
                doctors = value;
                OnPropertyChanged("Doctors");
            }
        }
        public Doctor SelectedDoctor
        {
            get { return selectedDoctor; }
            set
            {
                selectedDoctor = value;
                OnPropertyChanged("SelectedDoctor");
            }
        }
        public ObservableCollection<Room> Rooms
        {
            get => rooms;
            set
            {
                rooms = value;
                OnPropertyChanged("Rooms");
            }
        }
        public Room SelectedRoom
        {
            get { return selectedRoom; }
            set
            {
                selectedRoom = value;
                OnPropertyChanged("SelectedRoom");
            }
        }
        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                OnPropertyChanged("SelectedDate");
            }
        }
        public ObservableCollection<PossibleMeetingDTO> Meetings
        {
            get => meetings;
            set
            {
                meetings = value;
                OnPropertyChanged("Meetings");
            }
        }
        public String MeetingsVisibility
        {
            get { return meetingsVisibility; }
            set
            {
                meetingsVisibility = value;
                OnPropertyChanged("MeetingsVisibility");
            }
        }
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public CheckScheduledMeetingsVM()
        {
            SecretaryWindowVM.setWindowTitle("Scheduled meetings");
            DoctorRepository doctorRepository = new DoctorRepository();
            DoctorService doctorService = new DoctorService(doctorRepository);
            doctorController = new DoctorController(doctorService);
            ManagerRepository managerRepository = new ManagerRepository();
            ManagerService managerService = new ManagerService(managerRepository);
            SecretaryRepository secretaryRepository = new SecretaryRepository();
            SecretaryService secretaryService = new SecretaryService(secretaryRepository);
            managerController = new ManagerController(managerService);
            secretaryController = new SecretaryController(secretaryService);
            RoomRepository roomRepository = new RoomRepository();
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);
            MeetingRepository meetingRepository = new MeetingRepository();
            MeetingService meetingService = new MeetingService();
            meetingControler = new MeetingControler();
            doctorsToDoctorsObservable(doctorController.GetAllDoctors(), managerController.GetAllManager(), secretaryController.GetAllSecretary());
            roomsToRoomsObservable(roomController.GetAllRooms());
            SearchMeetingCommand = new RelayCommand(searchMeetingExecute);
            MeetingsVisibility = "Hidden";
        }
        public ICommand SearchMeetingCommand { get; set; }

        private void doctorsToDoctorsObservable(List<Doctor> doctorsList, List<Manager> managerList, List<Secretary> secretaryList)
        {
            Doctors = new ObservableCollection<Doctor>();
            foreach (var doc in doctorsList)
            {
                doc.FirstName = doc.FirstName + " " + doc.LastName;
                Doctors.Add(doc);
            }

            foreach (var man in managerList)
            {
                Doctors.Add(new Doctor(false, "", -1, man.FirstName + " " + man.LastName, "", "", "", man.Jmbg,
                    DateTime.Now, Gender.NONE, "", "", ""));
            }
            foreach (var sec in secretaryList)
            {
                Doctors.Add(new Doctor(false, "", -1, sec.FirstName + " " + sec.LastName, "", "", "", sec.Jmbg,
                    DateTime.Now, Gender.NONE, "", "", ""));
            }
        }

        private void roomsToRoomsObservable(List<Room> roomsList)
        {
            Rooms = new ObservableCollection<Room>();
            foreach (var roo in roomsList)
            {
                Rooms.Add(roo);
            }
        }

        private void searchMeetingExecute(object parameter)
        {
            List<PossibleMeetingDTO> temp = meetingControler.GetAllMeetingsAsPossibleMeetingsDto();
            Meetings = new ObservableCollection<PossibleMeetingDTO>();
            Boolean visible = false;
            foreach (var me in temp)
            {
                Boolean shouldAdd = true;
                if (SelectedDoctor != null)
                {
                    if (!me.UserJmbgs.Contains(SelectedDoctor.Jmbg))
                        shouldAdd = false;
                }
                if (SelectedRoom != null)
                {
                    if (me.RoomId != SelectedRoom.Id)
                        shouldAdd = false;
                }
                if (SelectedDate.Year != 1)
                {
                    if (SelectedDate.Date != me.StartTime.Date)
                        shouldAdd = false;
                }
                if (me.StartTime < DateTime.Now)
                    shouldAdd = false;
                if (shouldAdd)
                {
                    visible = true;
                    Meetings.Add(me);
                }
            }
            if(visible)
                MeetingsVisibility = "Visible";
            else
                MeetingsVisibility = "Hidden";
        }
    }
}
