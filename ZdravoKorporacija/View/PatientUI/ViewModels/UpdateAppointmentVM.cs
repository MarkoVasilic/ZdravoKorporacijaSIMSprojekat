using Controller;
using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.View.PatientUI.Commands;

namespace ZdravoKorporacija.View.PatientUI.ViewModels
{
    public class UpdateAppointmentVM : INotifyPropertyChanged
    {
        public PatientController patientController { get; set; }
        public DoctorController doctorController { get; set; }
        public RoomController roomController { get; set; }
        public AppointmentController appointmentController { get; set; }
        private String patientJmbg { get; set; }
        private Doctor? selectedDoctor { get; set; }
        private Room? selectedRoom { get; set; }

        private Patient? loggedPatient { get; set; }

        private int IdForModification { get; set; }

        private PossibleAppointmentsDTO? selectedAppointment { get; set; }

        private PossibleAppointmentsDTO? selectedAppointmentDelete { get; set; }
        private PossibleAppointmentsDTO? selectedNewAppointment { get; set; }
        private DateTime startDate;
        private String possibleAppointmentsVisibility;
        private String errorMessage;
        private ObservableCollection<Doctor> doctors;
        private ObservableCollection<Room> rooms;
        private ObservableCollection<PossibleAppointmentsDTO> appointments;
        private ObservableCollection<PossibleAppointmentsDTO> possibleAppointments;

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SearchAppointmentCommand { get; set; }
        public ICommand ModifyAppointmentCommand { get; set; }
        public ICommand DeleteAppointmentCommand { get; set; }
        public ICommand SelectNewAppointmentCommand { get; set; }


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

        public Doctor SelectedDoctor
        {
            get { return selectedDoctor; }
            set
            {
                selectedDoctor = value;
                OnPropertyChanged("SelectedDoctor");
            }
        }

        public Patient LoggedPatient
        {
            get { return loggedPatient; }
            set
            {
                loggedPatient = value;
                OnPropertyChanged("LoggedPatient");
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

        public PossibleAppointmentsDTO SelectedAppointmentDelete
        {
            get { return selectedAppointmentDelete; }
            set
            {
                selectedAppointmentDelete = value;
                OnPropertyChanged("SelectedAppointmentDelete");
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

        public String PossibleAppointmentsVisibility
        {
            get { return possibleAppointmentsVisibility; }
            set
            {
                possibleAppointmentsVisibility = value;
                OnPropertyChanged("PossibleAppointmentsVisibility");
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

        public UpdateAppointmentVM(int id)
        {
            PatientRepository patientRepository = new PatientRepository();
            PatientService patientService = new PatientService(patientRepository);
            DoctorRepository doctorRepository = new DoctorRepository();
            DoctorService doctorService = new DoctorService(doctorRepository);
            doctorController = new DoctorController(doctorService);
            RoomRepository roomRepository = new RoomRepository();
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);
            BasicRenovationRepository basicRenovationRepository = new BasicRenovationRepository();
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            AppointmentService appointmentService = new AppointmentService(appointmentRepository, patientRepository, doctorRepository,
                roomRepository);
            patientController = new PatientController(patientService, appointmentService);
            AdvancedRenovationJoiningRepository advancedRenovationJoining = new AdvancedRenovationJoiningRepository();
            AdvancedRenovationSeparationRepository advancedRenovationSeparation =
                new AdvancedRenovationSeparationRepository();
            ManagerRepository managerRepository = new ManagerRepository();
            SecretaryRepository secretaryRepository = new SecretaryRepository();
            MeetingRepository meetingRepository = new MeetingRepository();
            ScheduleService scheduleService = new ScheduleService(appointmentRepository, patientRepository,
                doctorRepository, roomRepository, basicRenovationRepository, advancedRenovationJoining,
                advancedRenovationSeparation, managerRepository, secretaryRepository, meetingRepository);
            EmergencyService emergencyService = new EmergencyService(appointmentRepository, patientRepository,
                doctorRepository, roomRepository, basicRenovationRepository, advancedRenovationJoining,
                advancedRenovationSeparation, scheduleService);
            appointmentController = new AppointmentController(appointmentService, scheduleService, emergencyService);
            roomsListToRoomList(roomController.GetAllRooms());
            doctorsListToDoctorList(doctorController.GetAllDoctors());
            // possibleAppointmentListToAppointmentList(appointmentController.GetAllAppointmentsBySecretary());
            possibleAppointmentListToAppointmentList(appointmentController.GetAllFutureAppointmentsByPatient());
            SearchAppointmentCommand = new RelayCommand(searchAppointmentExecute);
            ModifyAppointmentCommand = new RelayCommand(modifyAppointmentExecute);
            DeleteAppointmentCommand = new RelayCommand(deleteAppointmentExecute);
            SelectNewAppointmentCommand = new RelayCommand(selectNewAppointmentExecute);
            //  SelectedAppointment = appointmentController.GetOneById(id);
            IdForModification = id;

        }

        private void doctorsListToDoctorList(List<Doctor> doctors)
        {
            Doctors = new ObservableCollection<Doctor>();
            foreach (var d in doctors)
            {
                d.FirstName = d.FirstName + " " + d.LastName;
                Doctors.Add(d);
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


        }
        private void modifyAppointmentExecute(object parameter)
        {
            SelectedAppointment = parameter as PossibleAppointmentsDTO;
            appointmentListToAppointmentList();
            if (SelectedAppointment.StartTime.AddHours(-24) < System.DateTime.Now)
            {
                MessageBox.Show("Can't postpone an appointment 24 hours before initial date!");
            }
            else
                PatientWindowVM.NavigationService.Navigate(new UpdateAppointmentPage(SelectedAppointment.AppointmentId));
        }

        private void deleteAppointmentExecute(object parameter)
        {
            SelectedAppointmentDelete = parameter as PossibleAppointmentsDTO;
            appointmentController.DeleteAppointment(SelectedAppointmentDelete.AppointmentId);
            //logika za antiTroll

            if (patientController.getTrollCounterByPatient(App.loggedUser.Jmbg) >= 4)
            {

                MessageBox.Show("TROLL ALERT: " + patientController.getTrollCounterByPatient(App.loggedUser.Jmbg));
                PatientWindowVM.NavigationService.Navigate(new PatientHomePage());
                System.Environment.Exit(0);

            }
            patientController.incrementTrollCounterByPatient(App.loggedUser.Jmbg);
            Console.WriteLine("(DELETE)Current troll counter: " + patientController.getTrollCounterByPatient(App.loggedUser.Jmbg));
            possibleAppointmentListToAppointmentList(appointmentController.GetAllFutureAppointmentsByPatient());

        }

        private void selectNewAppointmentExecute(object parameter)
        {

            if (patientController.getTrollCounterByPatient(App.loggedUser.Jmbg) >= 4)
            {

                MessageBox.Show("TROLL ALERT: " + patientController.getTrollCounterByPatient(App.loggedUser.Jmbg));
                PatientWindowVM.NavigationService.Navigate(new PatientHomePage());
                System.Environment.Exit(0);

            }

            SelectedNewAppointment = parameter as PossibleAppointmentsDTO;
            Console.WriteLine("troll: " + patientController.getTrollCounterByPatient(App.loggedUser.Jmbg));
            patientController.incrementTrollCounterByPatient(App.loggedUser.Jmbg);
            Console.WriteLine("(UPDATE)Current troll counter: " + patientController.getTrollCounterByPatient(App.loggedUser.Jmbg));
            try
            {

                Console.WriteLine("troll: " + patientController.getTrollCounterByPatient(App.loggedUser.Jmbg));

                appointmentController.ModifyAppointment(IdForModification, SelectedNewAppointment.StartTime);
                MessageBox.Show("Uspješno zakazan pregled za " + SelectedNewAppointment.StartTime, "USPJEŠNO!", MessageBoxButton.OK,MessageBoxImage.Information);
                PatientWindowVM.NavigationService.Navigate(new GetAllAppointmentsPatient());

            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }

        }

    }
}
