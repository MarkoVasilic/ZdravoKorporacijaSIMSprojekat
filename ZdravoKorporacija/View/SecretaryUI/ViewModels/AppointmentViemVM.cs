using Controller;
using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.View.SecretaryUI.Commands;

namespace ZdravoKorporacija.View.SecretaryUI.ViewModels
{
    public class AppointmentViemVM : INotifyPropertyChanged
    {
        public PatientController patientController { get; set; }
        public DoctorController doctorController { get; set; }
        public RoomController roomController { get; set; }
        public AppointmentController appointmentController { get; set; }
        private String patientJmbg { get; set; }
        private Doctor? selectedDoctor { get; set; }
        private Room? selectedRoom { get; set; }
        private ObservableCollection<DateTime> newSelectedDates { get; set; }
        private PossibleAppointmentsDTO? selectedAppointment { get; set; }
        private PossibleAppointmentsDTO? selectedNewAppointment { get; set; }
        private DateTime startDate;
        private String errorMessage;
        private ObservableCollection<Doctor> doctors;
        private ObservableCollection<Room> rooms;
        private ObservableCollection<PossibleAppointmentsDTO> appointments;
        private ObservableCollection<PossibleAppointmentsDTO> possibleAppointments;
        private ListCollectionView lcv { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SearchAppointmentCommand { get; set; }
        public ICommand ModifyAppointmentCommand { get; set; }
        public ICommand DeleteAppointmentCommand { get; set; }
        public ICommand SelectNewAppointmentCommand { get; set; }
        public ICommand SelectDatesCommand { get; set; }
        public ICommand YesCommand { get; set; }
        public ICommand NoCommand { get; set; }

        public ObservableCollection<Doctor> Doctors
        {
            get => doctors;
            set
            {
                doctors = value;
                OnPropertyChanged("Doctors");
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

        public ObservableCollection<PossibleAppointmentsDTO> PossibleAppointments
        {
            get => possibleAppointments;
            set
            {
                possibleAppointments = value;
                OnPropertyChanged("PossibleAppointments");
            }
        }

        public ObservableCollection<DateTime> NewSelectedDates
        {
            get { return newSelectedDates; }
            set
            {
                newSelectedDates = value;
                OnPropertyChanged("NewSelectedDates");
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
        public Room SelectedRoom
        {
            get { return selectedRoom; }
            set
            {
                selectedRoom = value;
                OnPropertyChanged("SelectedRoom");
            }
        }

        public PossibleAppointmentsDTO SelectedAppointment
        {
            get { return selectedAppointment; }
            set
            {
                selectedAppointment = value;
                OnPropertyChanged("SelectedAppointment");
            }
        }

        public PossibleAppointmentsDTO SelectedNewAppointment
        {
            get { return selectedNewAppointment; }
            set
            {
                selectedNewAppointment = value;
                OnPropertyChanged("SelectedNewAppointment");
            }
        }

        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }

        public string PatientJmbg
        {
            get => patientJmbg;
            set
            {
                patientJmbg = value;
                OnPropertyChanged("PatientJmbg");
            }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                OnPropertyChanged("StartDate");
            }
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public ObservableCollection<PossibleAppointmentsDTO> Appointments
        {
            get => appointments;
            set
            {
                appointments = value;
                OnPropertyChanged("Appointments");
            }
        }

        public ListCollectionView Lcv
        {
            get => lcv;
            set
            {
                lcv = value;
                OnPropertyChanged("Lcv");
            }
        }

        public AppointmentViemVM()
        {
            SecretaryWindowVM.setWindowTitle("Scheduled appointments");
            PatientRepository patientRepository = new PatientRepository();
            PatientService patientService = new PatientService(patientRepository);
            DoctorRepository doctorRepository = new DoctorRepository();
            DoctorService doctorService = new DoctorService(doctorRepository);
            doctorController = new DoctorController(doctorService);
            RoomRepository roomRepository = new RoomRepository();
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            AppointmentService appointmentService = new AppointmentService(appointmentRepository, patientRepository, doctorRepository,
                roomRepository);
            doctorController = new DoctorController(doctorService);
            roomController = new RoomController(roomService);
            BasicRenovationRepository basicRenovationRepository = new BasicRenovationRepository();
            doctorController = new DoctorController(doctorService);
            roomController = new RoomController(roomService);
            patientController = new PatientController(patientService, appointmentService);
            doctorController = new DoctorController(doctorService);
            ManagerRepository managerRepository = new ManagerRepository();
            SecretaryRepository secretaryRepository = new SecretaryRepository();
            MeetingRepository meetingRepository = new MeetingRepository();
            roomController = new RoomController(roomService);
            doctorController = new DoctorController(doctorService);
            AdvancedRenovationJoiningRepository advancedRenovationJoining = new AdvancedRenovationJoiningRepository();
            AdvancedRenovationSeparationRepository advancedRenovationSeparation =
                new AdvancedRenovationSeparationRepository();
            ScheduleService scheduleService = new ScheduleService(appointmentRepository, patientRepository,
                doctorRepository, roomRepository, basicRenovationRepository, advancedRenovationJoining,
                advancedRenovationSeparation, managerRepository, secretaryRepository, meetingRepository);
            EmergencyService emergencyService = new EmergencyService(appointmentRepository, patientRepository,
                doctorRepository, roomRepository, basicRenovationRepository, advancedRenovationJoining,
                advancedRenovationSeparation, scheduleService);
            patientController = new PatientController(patientService, appointmentService);
            appointmentController = new AppointmentController(appointmentService, scheduleService, emergencyService);
            roomsListToRoomList(roomController.GetAllRooms());
            doctorsListToDoctorList(doctorController.GetAllDoctors());
            possibleAppointmentListToAppointmentList(appointmentController.GetPossibleAppointmentsDtos());
            SearchAppointmentCommand = new RelayCommand(searchAppointmentExecute);
            ModifyAppointmentCommand = new RelayCommand(modifyAppointmentExecute);
            DeleteAppointmentCommand = new RelayCommand(deleteAppointmentExecute);
            SelectNewAppointmentCommand = new RelayCommand(selectNewAppointmentExecute);
            SelectDatesCommand = new RelayCommand(selectDatesExecute);
            NewSelectedDates = new ObservableCollection<DateTime>();
        }

        private void doctorsListToDoctorList(List<Doctor> doctors)
        {
            Doctors = new ObservableCollection<Doctor>();
            Doctor doctor = new Doctor(false, "", -1, "- - -", "", "", "", "", new DateTime(), Gender.NONE, "", "", "");
            Doctors.Add(doctor);
            foreach (var d in doctors)
            {
                d.FirstName = d.FirstName + " " + d.LastName;
                Doctors.Add(d);
            }
        }
        private void roomsListToRoomList(List<Room> rooms)
        {
            Rooms = new ObservableCollection<Room>();
            Room room = new Room("- - -", -1, "", RoomType.NONE);
            Rooms.Add(room);
            foreach (var r in rooms)
            {
                Rooms.Add(r);
            }
        }

        private void possibleAppointmentListToAppointmentList(List<PossibleAppointmentsDTO> possibleAppointmentsDTOs)
        {
            Appointments = new ObservableCollection<PossibleAppointmentsDTO>();
            foreach (var pa in possibleAppointmentsDTOs)
            {
                if (pa.StartTime > DateTime.Now)
                    Appointments.Add(pa);
            }
        }

        private void appointmentListToAppointmentList()
        {
            try
            {
                List<PossibleAppointmentsDTO> possibleAppointmentsDTOs = appointmentController.GetPossibleAppointmentsBySecretary(SelectedAppointment.PatientJmbg,
                SelectedAppointment.DoctorJmbg, SelectedAppointment.RoomId, SelectedAppointment.StartTime, SelectedAppointment.StartTime.AddDays(4), SelectedAppointment.Duration,
                "time");
                PossibleAppointments = new ObservableCollection<PossibleAppointmentsDTO>();
                foreach (var pa in possibleAppointmentsDTOs)
                {
                    PossibleAppointments.Add(pa);
                }
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
        }

        private void searchAppointmentExecute(object parameter)
        {
            List<PossibleAppointmentsDTO> temp = appointmentController.GetPossibleAppointmentsDtos();
            Appointments = new ObservableCollection<PossibleAppointmentsDTO>();
            foreach (var ap in temp)
            {
                Boolean shouldAdd = true;
                if (PatientJmbg != null && PatientJmbg.Length > 0)
                {
                    if (ap.PatientJmbg != PatientJmbg)
                        shouldAdd = false;
                }
                if (SelectedDoctor != null && SelectedDoctor.Jmbg.Length > 1)
                {
                    if (ap.DoctorJmbg != SelectedDoctor.Jmbg)
                        shouldAdd = false;
                }
                if (SelectedRoom != null && SelectedRoom.Id > 0)
                {
                    if (ap.RoomId != SelectedRoom.Id)
                        shouldAdd = false;
                }
                if (NewSelectedDates != null && NewSelectedDates.Count > 0)
                {
                    Boolean isCorrectDate = false;
                    foreach (var sd in NewSelectedDates)
                    {
                        if (ap.StartTime.Date == sd.Date)
                        {
                            isCorrectDate = true;
                            break;
                        }
                    }
                    if (!isCorrectDate)
                        shouldAdd = false;
                }
                if (ap.StartTime < DateTime.Now)
                    shouldAdd = false;
                if (shouldAdd)
                    Appointments.Add(ap);
            }
            Lcv = new ListCollectionView(Appointments);
            Lcv.GroupDescriptions.Add(new PropertyGroupDescription("StartTime", new DateTimeToDateConverter()));
        }
        public class DateTimeToDateConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var dt = value as DateTime?;
                if (dt is null)
                    return "";
                else
                    return dt?.Date.ToShortDateString();
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
        private void modifyAppointmentExecute(object parameter)
        {
            SelectedAppointment = parameter as PossibleAppointmentsDTO;
            appointmentListToAppointmentList();
            SecretaryWindowVM.NavigationService.Navigate(new ModifyAppointmentView(this));
        }

        private void deleteAppointmentExecute(object parameter)
        {
            SelectedAppointment = parameter as PossibleAppointmentsDTO;
            appointmentController.DeleteAppointment(SelectedAppointment.AppointmentId);
            searchAppointmentExecute(null);
        }

        private void selectNewAppointmentExecute(object parameter)
        {
            SelectedNewAppointment = parameter as PossibleAppointmentsDTO;
            try
            {
                appointmentController.ModifyAppointment(SelectedAppointment.AppointmentId, SelectedNewAppointment.StartTime);
                SecretaryWindowVM.NavigationService.Navigate(new AppointmentView());
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }

        }

        private void selectDatesExecute(object parameter)
        {
            SelectedDatesCollection dates = parameter as SelectedDatesCollection;
            NewSelectedDates.Clear();
            foreach (var items in dates)
            {
                NewSelectedDates.Add(items);
            }
        }
    }
}
