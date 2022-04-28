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
using System.Windows.Input;
using ZdravoKorporacija.DTO;
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

        private PossibleAppointmentsDTO? selectedAppointment { get; set; }
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

        public AppointmentViemVM()
        {
            SecretaryWindowVM.setWindowTitle("Scheduled appointments");
            PatientRepository patientRepository = new PatientRepository();
            PatientService patientService = new PatientService(patientRepository);
            patientController = new PatientController(patientService);
            DoctorRepository doctorRepository = new DoctorRepository();
            DoctorService doctorService = new DoctorService(doctorRepository);
            doctorController = new DoctorController(doctorService);
            RoomRepository roomRepository = new RoomRepository();
            RoomService roomService = new RoomService(roomRepository);
            roomController = new RoomController(roomService);
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            AppointmentService appointmentService = new AppointmentService(appointmentRepository, patientRepository, doctorRepository,
                roomRepository);
            appointmentController = new AppointmentController(appointmentService);
            roomsListToRoomList(roomController.GetAllRooms());
            doctorsListToDoctorList(doctorController.GetAllDoctors());
            possibleAppointmentListToAppointmentList(appointmentController.GetAllAppointmentsBySecretary());
            SearchAppointmentCommand = new RelayCommand(searchAppointmentExecute);
            ModifyAppointmentCommand = new RelayCommand(modifyAppointmentExecute);
            DeleteAppointmentCommand = new RelayCommand(deleteAppointmentExecute);
            SelectNewAppointmentCommand = new RelayCommand(selectNewAppointmentExecute);
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
            SecretaryWindowVM.NavigationService.Navigate(new ModifyAppointmentView(this));
        }

        private void deleteAppointmentExecute(object parameter)
        {
            SelectedAppointment = parameter as PossibleAppointmentsDTO;
            appointmentController.DeleteAppointment(SelectedAppointment.AppointmentId);
            possibleAppointmentListToAppointmentList(appointmentController.GetAllAppointmentsBySecretary());
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
    }
}
