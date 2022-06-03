using Controller;
using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.View.SecretaryUI.Commands;

namespace ZdravoKorporacija.View.SecretaryUI.ViewModels
{
    public class ScheduleAppointmentVM : INotifyPropertyChanged
    {
        public PatientController patientController { get; set; }
        public DoctorController doctorController { get; set; }
        public RoomController roomController { get; set; }
        public AppointmentController appointmentController { get; set; }
        private Patient? selectedPatient { get; set; }
        private Doctor? selectedDoctor { get; set; }
        private Room? selectedRoom { get; set; }
        private int selectedDuration { get; set; }
        private String selectedPriority { get; set; }
        private String infoVisibility;
        private String possibleAppointmentsVisibility;
        private String errorMessage;
        private String errorMessagePossibleAppointments;
        private String errorMessageConfirmAppointment;
        private String selectedSpeciality;
        private PossibleAppointmentsDTO selectedAppointment;
        private DateTime dateFrom;
        private DateTime dateUntil;
        private ObservableCollection<String> doctorSpecialities;
        private ObservableCollection<Doctor> doctors;
        private ObservableCollection<Room> rooms;
        private ObservableCollection<PossibleAppointmentsDTO> possibleAppointments;


        public String PatientJmbg { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SearchPatientCommand { get; set; }
        public ICommand SelectPatientCommand { get; set; }
        public ICommand SearchAppointmentCommand { get; set; }
        public ICommand SelectAppointmentCommand { get; set; }
        public ICommand ConfirmAppointmentCommand { get; set; }


        public String SelectedSpeciality
        {
            get => selectedSpeciality;
            set
            {
                selectedSpeciality = value;
                doctorsListToDoctorList(doctorController.getAllBySpeciality(selectedSpeciality));
                OnPropertyChanged("SelecetedSpeciality");
            }
        }
        public ObservableCollection<String> DoctorSpecialities
        {
            get => doctorSpecialities;
            set
            {
                doctorSpecialities = value;
                OnPropertyChanged("DoctorSpecialities");
            }
        }
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

        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }

        public string ErrorMessagePossibleAppointments
        {
            get => errorMessagePossibleAppointments;
            set
            {
                errorMessagePossibleAppointments = value;
                OnPropertyChanged("ErrorMessagePossibleAppointments");
            }
        }
        public string ErrorMessageConfirmAppointment
        {
            get => errorMessageConfirmAppointment;
            set
            {
                errorMessageConfirmAppointment = value;
                OnPropertyChanged("ErrorMessageConfirmAppointment");
            }
        }


        public Patient SelectedPatient
        {
            get { return selectedPatient; }
            set
            {
                selectedPatient = value;
                OnPropertyChanged("SelectedPatient");
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
        public int SelectedDuration
        {
            get { return selectedDuration; }
            set
            {
                selectedDuration = value;
                OnPropertyChanged("SelectedDuration");
            }
        }

        public String SelectedPriority
        {
            get { return selectedPriority; }
            set
            {
                selectedPriority = value;
                OnPropertyChanged("SelectedPriority");
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

        public String InfoVisibility
        {
            get { return infoVisibility; }
            set
            {
                infoVisibility = value;
                OnPropertyChanged("InfoVisibility");
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


        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public ScheduleAppointmentVM()
        {
            SecretaryWindowVM.setWindowTitle("Schedule appointment");
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
            ManagerRepository managerRepository = new ManagerRepository();
            SecretaryRepository secretaryRepository = new SecretaryRepository();
            MeetingRepository meetingRepository = new MeetingRepository();
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
            specialitiesListToStringList(doctorController.GetAllSpecialities());
            roomsListToRoomList(roomController.GetAllRooms());
            SearchPatientCommand = new RelayCommand(searchPatientExecute);
            SelectPatientCommand = new RelayCommand(selectPatientExecute);
            SearchAppointmentCommand = new RelayCommand(searchAppointmentExecute);
            SelectAppointmentCommand = new RelayCommand(selectAppointmentExecute);
            ConfirmAppointmentCommand = new RelayCommand(confirmAppointmentExecute);
            InfoVisibility = "Hidden";
            PossibleAppointmentsVisibility = "Hidden";
        }

        private void specialitiesListToStringList(List<string> specialities)
        {
            DoctorSpecialities = new ObservableCollection<string>();
            foreach (var ds in specialities)
            {
                DoctorSpecialities.Add(ds);
            }
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

        private void appointmentListToAppointmentList(List<PossibleAppointmentsDTO> appointmentsDTOs)
        {
            PossibleAppointments = new ObservableCollection<PossibleAppointmentsDTO>();
            foreach (var pad in appointmentsDTOs)
            {
                PossibleAppointments.Add(pad);
            }
        }


        private void searchPatientExecute(object parameter)
        {
            SelectedPatient = patientController.GetOnePatient(PatientJmbg);
            if (SelectedPatient != null)
            {
                ErrorMessage = "";
                InfoVisibility = "Visible";
            }
            else
            {
                InfoVisibility = "Hidden";
                ErrorMessage = "Patient with that jmbg doesn't exist!";
            }
        }

        private void selectPatientExecute(object parameter)
        {
            SecretaryWindowVM.NavigationService.Navigate(new SelectAppointmentInfo(this));
        }


        private void searchAppointmentExecute(object parameter)
        {
            if (SelectedPatient == null || SelectedDoctor == null || SelectedRoom == null || DateFrom.Year == 1 || DateUntil.Year == 1
                    || SelectedDuration == 0)
            {
                ErrorMessagePossibleAppointments = "All fields are necessary!";
            }
            else
            {
                ErrorMessagePossibleAppointments = "";
                try
                {
                    appointmentListToAppointmentList(appointmentController.GetPossibleAppointmentsBySecretary(SelectedPatient.Jmbg, SelectedDoctor.Jmbg, SelectedRoom.Id,
                        DateFrom, DateUntil, SelectedDuration, SelectedPriority));
                    PossibleAppointmentsVisibility = "Visible";
                    ErrorMessagePossibleAppointments = "";
                }
                catch (Exception e)
                {
                    PossibleAppointmentsVisibility = "Hidden";
                    ErrorMessagePossibleAppointments = e.Message;
                }
            }
        }
        private void selectAppointmentExecute(object parameter)
        {
            SelectedAppointment = parameter as PossibleAppointmentsDTO;
            SecretaryWindowVM.NavigationService.Navigate(new ConfirmAppointmentInformations(this, null));
        }
        private void confirmAppointmentExecute(object parameter)
        {
            try
            {
                appointmentController.CreateAppointmentBySecretary(selectedAppointment.PatientJmbg, selectedAppointment.DoctorJmbg,
                    selectedAppointment.RoomId, selectedAppointment.StartTime, selectedAppointment.Duration);
                SecretaryWindowVM.NavigationService.Navigate(new AppointmentView());
            }
            catch (Exception e)
            {
                ErrorMessageConfirmAppointment = e.Message;
            }

        }


    }
}
