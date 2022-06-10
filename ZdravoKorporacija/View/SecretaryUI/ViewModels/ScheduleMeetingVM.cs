using Controller;
using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.View.SecretaryUI.Commands;
using ZdravoKorporacija.View.SecretaryUI.DTO;

namespace ZdravoKorporacija.View.SecretaryUI.ViewModels
{
    public class ScheduleMeetingVM : INotifyPropertyChanged
    {
        public DoctorController doctorController { get; set; }
        public ManagerController managerController { get; set; }
        public SecretaryController secretaryController { get; set; }
        public RoomController roomController { get; set; }
        public MeetingControler meetingControler { get; set; }
        public NotificationController notificationController { get; set; }
        public AppointmentController appointmentController { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<String> doctors;
        private String selectedDoctorsNames;
        private ObservableCollection<String> doctorsNames;
        private List<String> selectedDoctors;
        private DateTime dateFrom;
        private DateTime dateUntil;
        private String errorMessagePossibleMeetings;
        private String errorMessageConfirmMeeting;
        private int selectedDuration { get; set; }
        private String possibleMeetingsVisibility;
        private ObservableCollection<PossibleMeetingDTO> possibleMeetings;
        private ObservableCollection<Room> rooms;
        private Room? selectedRoom { get; set; }
        private PossibleMeetingDTO selectedMeeting;
        private ObservableCollection<UserDetailsDto> userDetails;

        public ObservableCollection<String> Doctors
        {
            get => doctors;
            set
            {
                doctors = value;
                OnPropertyChanged("Doctors");
            }
        }
        public String SelectedDoctorsNames
        {
            get => selectedDoctorsNames;
            set
            {
                selectedDoctorsNames = value;
                OnPropertyChanged("SelectedDoctorsNames");
            }
        }
        public ObservableCollection<String> DoctorsNames
        {
            get => doctorsNames;
            set
            {
                doctorsNames = value;
                OnPropertyChanged("DoctorsNames");
            }
        }
        public List<String> SelectedDoctors
        {
            get => selectedDoctors;
            set
            {
                selectedDoctors = value;
                OnPropertyChanged("SelectedDoctors");
            }
        }
        public DateTime DateFrom
        {
            get { return dateFrom; }
            set
            {
                dateFrom = value;
                OnPropertyChanged("DateFrom");
            }
        }
        public DateTime DateUntil
        {
            get { return dateUntil; }
            set
            {
                dateUntil = value;
                OnPropertyChanged("DateUntil");
            }
        }
        public int SelectedDuration
        {
            get { return selectedDuration; }
            set
            {
                selectedDuration = value;
                OnPropertyChanged("SelectedDuration");
            }
        }
        public string ErrorMessagePossibleMeetings
        {
            get => errorMessagePossibleMeetings;
            set
            {
                errorMessagePossibleMeetings = value;
                OnPropertyChanged("ErrorMessagePossibleMeetings");
            }
        }
        public string ErrorMessageConfirmMeeting
        {
            get => errorMessageConfirmMeeting;
            set
            {
                errorMessageConfirmMeeting = value;
                OnPropertyChanged("ErrorMessageConfirmMeeting");
            }
        }
        public String PossibleMeetingsVisibility
        {
            get { return possibleMeetingsVisibility; }
            set
            {
                possibleMeetingsVisibility = value;
                OnPropertyChanged("PossibleMeetingsVisibility");
            }
        }
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
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
        public ObservableCollection<PossibleMeetingDTO> PossibleMeetings
        {
            get => possibleMeetings;
            set
            {
                possibleMeetings = value;
                OnPropertyChanged("PossibleMeetings");
            }
        }

        public PossibleMeetingDTO SelectedMeeting
        {
            get { return selectedMeeting; }
            set
            {
                selectedMeeting = value;
                OnPropertyChanged("SelectedMeeting");
            }
        }

        public ObservableCollection<UserDetailsDto> UserDetails
        {
            get { return userDetails; }
            set
            {
                userDetails = value;
                OnPropertyChanged("UserDetails");
            }
        }

        public ScheduleMeetingVM()
        {
            SecretaryWindowVM.setWindowTitle("Schedule meeting");
            PatientRepository patientRepository = new PatientRepository();
            DoctorRepository doctorRepository = new DoctorRepository();
            DoctorService doctorService = new DoctorService(doctorRepository);
            doctorController = new DoctorController(doctorService);
            RoomRepository roomRepository = new RoomRepository();
            RoomService roomService = new RoomService(roomRepository);
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            AppointmentService appointmentService = new AppointmentService(appointmentRepository, patientRepository, doctorRepository,
                roomRepository);
            ManagerRepository managerRepository = new ManagerRepository();
            ManagerService managerService = new ManagerService(managerRepository);
            SecretaryRepository secretaryRepository = new SecretaryRepository();
            SecretaryService secretaryService = new SecretaryService(secretaryRepository);
            MeetingRepository meetingRepository = new MeetingRepository();
            roomController = new RoomController(roomService);
            doctorController = new DoctorController(doctorService);
            managerController = new ManagerController(managerService);
            secretaryController = new SecretaryController(secretaryService);
            AdvancedRenovationJoiningRepository advancedRenovationJoining = new AdvancedRenovationJoiningRepository();
            AdvancedRenovationSeparationRepository advancedRenovationSeparation =
                new AdvancedRenovationSeparationRepository();
            BasicRenovationRepository basicRenovationRepository = new BasicRenovationRepository();
            ScheduleService scheduleService = new ScheduleService(appointmentRepository, patientRepository,
                doctorRepository, roomRepository, basicRenovationRepository, advancedRenovationJoining,
                advancedRenovationSeparation, managerRepository, secretaryRepository, meetingRepository);
            EmergencyService emergencyService = new EmergencyService(appointmentRepository, patientRepository,
                doctorRepository, roomRepository, basicRenovationRepository, advancedRenovationJoining,
                advancedRenovationSeparation, scheduleService);
            UserService userService = new UserService(doctorRepository, managerRepository, secretaryRepository);
            MeetingService meetingService = new MeetingService(meetingRepository, roomRepository, scheduleService, userService);
            meetingControler = new MeetingControler(meetingService);
            appointmentController = new AppointmentController(appointmentService, scheduleService, emergencyService);
            NotificationRepository notificationRepository = new NotificationRepository();
            PrescriptionRepository prescriptionRepository = new PrescriptionRepository();
            MedicalRecordRepository medicalRecordRepository = new MedicalRecordRepository();
            MedicationRepository medicationRepository = new MedicationRepository();
            PrescriptionService prescriptionService = new PrescriptionService(prescriptionRepository,
                medicalRecordRepository, patientRepository, medicationRepository);
            NotificationService notificationService =
                new NotificationService(notificationRepository, prescriptionService);
            notificationController = new NotificationController(notificationService);
            doctorsListToStringList(doctorController.GetAllDoctors(), managerController.GetAllManager(), secretaryController.GetAllSecretary());
            roomsListToRoomList(roomController.GetAllRooms());
            AddDoctorCommand = new RelayCommand(addDoctorExecute);
            RemoveDoctorCommand = new RelayCommand(removeDoctorExecute);
            SearchMeetingCommand = new RelayCommand(searchMeetingExecute);
            SelectMeetingCommand = new RelayCommand(selectMeetingExecute);
            ConfirmMeetingCommand = new RelayCommand(confirmMeetingExecute);
            DoctorsNames = new ObservableCollection<string>();
            PossibleMeetingsVisibility = "Hidden";
        }

        public ICommand AddDoctorCommand { get; set; }
        public ICommand RemoveDoctorCommand { get; set; }
        public ICommand SearchMeetingCommand { get; set; }
        public ICommand SelectMeetingCommand { get; set; }
        public ICommand ConfirmMeetingCommand { get; set; }

        private void doctorsListToStringList(List<Doctor> doctors, List<Manager> managers, List<Secretary> secretaries)
        {
            Doctors = new ObservableCollection<String>();
            foreach (var d in doctors)
            {
                Doctors.Add(d.FirstName + " " + d.LastName);
            }
            foreach (var m in managers)
            {
                Doctors.Add(m.FirstName + " " + m.LastName);
            }
            foreach (var s in secretaries)
            {
                Doctors.Add(s.FirstName + " " + s.LastName);
            }
        }

        private void roomsListToRoomList(List<Room> rooms)
        {
            Rooms = new ObservableCollection<Room>();
            foreach (var r in rooms)
            {
                Rooms.Add(r);
            }
        }

        private void doctorsStringsToDoctorsList()
        {
            SelectedDoctors = new List<String>();
            List<Doctor> doctors = doctorController.GetAllDoctors();
            List<Manager> managers = managerController.GetAllManager();
            List<Secretary> secretaries = secretaryController.GetAllSecretary();
            foreach (var d in DoctorsNames)
            {
                foreach (var doc in doctors)
                {
                    if (doc.FirstName + " " + doc.LastName == d)
                    {
                        SelectedDoctors.Add(doc.Jmbg);
                        break;
                    }
                }

                foreach (var man in managers)
                {
                    if (man.FirstName + " " + man.LastName == d)
                    {
                        SelectedDoctors.Add(man.Jmbg);
                        break;
                    }
                }
                foreach (var sec in secretaries)
                {
                    if (sec.FirstName + " " + sec.LastName == d)
                    {
                        SelectedDoctors.Add(sec.Jmbg);
                        break;
                    }
                }
            }
        }

        private void meetingsListToMeetingsList(List<PossibleMeetingDTO> meetingDtos)
        {
            PossibleMeetings = new ObservableCollection<PossibleMeetingDTO>();
            foreach (var pad in meetingDtos)
            {
                PossibleMeetings.Add(pad);
            }
        }

        private void createUserDetailsList()
        {
            char[] separator = { ' ' };
            for (int i = 0; i < SelectedMeeting.UserJmbgs.Count; i++)
            {
                string[] strs = SelectedMeeting.UserFullNames[i].Split(separator);
                UserDetailsDto userDetailsDto = new UserDetailsDto(strs[0], strs[1], SelectedMeeting.UserJmbgs[i]);
                UserDetails.Add(userDetailsDto);
            }
        }

        private void addDoctorExecute(object parameter)
        {
            if (!DoctorsNames.Contains(SelectedDoctorsNames))
                DoctorsNames.Add(SelectedDoctorsNames);

        }
        private void removeDoctorExecute(object parameter)
        {
            if (DoctorsNames.Contains(SelectedDoctorsNames))
                DoctorsNames.Remove(SelectedDoctorsNames);
        }
        private void searchMeetingExecute(object parameter)
        {
            if (DoctorsNames == null || DoctorsNames.Count < 1 || DateFrom.Year == 1 || DateUntil.Year == 1
                || SelectedDuration == 0)
            {
                PossibleMeetingsVisibility = "Hidden";
                ErrorMessagePossibleMeetings = "All fields are necessary!";
            }
            else
            {
                ErrorMessagePossibleMeetings = "";
                try
                {
                    doctorsStringsToDoctorsList();
                    meetingsListToMeetingsList(meetingControler.GetPossibleMeetingAppointments(SelectedDoctors, SelectedRoom.Id, DateFrom, DateUntil, SelectedDuration));
                    PossibleMeetingsVisibility = "Visible";
                    ErrorMessagePossibleMeetings = "";
                }
                catch (Exception e)
                {
                    PossibleMeetingsVisibility = "Hidden";
                    ErrorMessagePossibleMeetings = e.Message;
                }
            }
        }

        private void selectMeetingExecute(object parameter)
        {
            SelectedMeeting = parameter as PossibleMeetingDTO;
            UserDetails = new ObservableCollection<UserDetailsDto>();
            createUserDetailsList();
            SelectedRoom = roomController.GetRoomById(SelectedMeeting.RoomId);
            SecretaryWindowVM.NavigationService.Navigate(new ConfirmMeetingInformantions(this));
        }

        private void confirmMeetingExecute(object parameter)
        {
            try
            {
                meetingControler.CreateMeeting(SelectedMeeting.UserJmbgs, SelectedMeeting.RoomId, SelectedMeeting.StartTime, SelectedMeeting.Duration);
                foreach (var user in SelectedMeeting.UserJmbgs)
                {
                    String description = "Meeting that requires your presence will start at " +
                                         SelectedMeeting.StartTime.ToString() + " in room " + SelectedMeeting.RoomName;
                    notificationController.CreateUserNotification("Scheduled meeting", description,
                        user);
                }
                SecretaryWindowVM.NavigationService.Navigate(new ScheduleMeetingPage());
            }
            catch (Exception e)
            {
                ErrorMessageConfirmMeeting = e.Message;
            }

        }
    }
}
