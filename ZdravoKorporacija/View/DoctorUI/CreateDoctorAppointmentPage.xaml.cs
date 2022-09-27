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
using Controller;
using Model;
using Repository;
using Service;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.View.DoctorUI.ViewModel;

namespace ZdravoKorporacija.View.DoctorUI
{
    /// <summary>
    /// Interaction logic for CreateDoctorAppointmentPage.xaml
    /// </summary>
    public partial class CreateDoctorAppointmentPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string? propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private int RoomId { get; set; }
        public DoctorController doctorController { get; set; }

        private String priority;
        public String Priority
        {
            get { return priority; }
            set
            {
                priority = value;
                OnPropertyChanged("Priority");
            }
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

        private ObservableCollection<Doctor> doctors { get; set; }

        public ObservableCollection<Doctor> Doctors
        {
            get { return doctors; }
            set
            {
                doctors = value;
                OnPropertyChanged("Doctors");
            }
        }
        public CreateDoctorAppointmentPage()
        {
            InitializeComponent();
            DoctorWindowVM.setWindowTitle("Schedule new appointment");
            this.ErrorMessage = "";
            if (PriorityComboBox.SelectedIndex == 0)
                this.Priority = "doctor";
            else
                this.Priority = "time";

            DoctorRepository doctorRepository = new DoctorRepository();
            DoctorService doctorService = new DoctorService(doctorRepository);
            doctorController = new DoctorController(doctorService);
            RoomRepository roomRepository = new RoomRepository();
            PatientRepository patientRepository = new PatientRepository();
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            BasicRenovationRepository basicRenovationRepository = new BasicRenovationRepository();
            AdvancedRenovationJoiningRepository advancedRenovationJoining = new AdvancedRenovationJoiningRepository();
            AdvancedRenovationSeparationRepository advancedRenovationSeparation =
                new AdvancedRenovationSeparationRepository();
            ManagerRepository managerRepository = new ManagerRepository();
            SecretaryRepository secretaryRepository = new SecretaryRepository();
            AppointmentService appointmentService = new AppointmentService(appointmentRepository, patientRepository, doctorRepository, roomRepository);
            MeetingRepository meetingRepository = new MeetingRepository();
            ScheduleService scheduleService = new ScheduleService(appointmentRepository, patientRepository,
                doctorRepository, roomRepository, basicRenovationRepository, advancedRenovationJoining,
                advancedRenovationSeparation, managerRepository, secretaryRepository, meetingRepository);
            EmergencyService emergencyService = new EmergencyService(appointmentRepository, patientRepository,
                doctorRepository, roomRepository, basicRenovationRepository, advancedRenovationJoining,
                advancedRenovationSeparation, scheduleService);
            this.doctors = new ObservableCollection<Doctor>(doctorController.GetAllDoctors());
            this.DataContext = this;
        }

        private void Confirm_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Doctor doctor = (Doctor)((Button)sender).CommandParameter;
                if (String.IsNullOrWhiteSpace(PatientJmbg))
                {
                    ErrorMessage = "Please enter patient jmbg to schedule appointment!";
                }else if (PatientJmbg.Length < 13)
                {
                    ErrorMessage = "Please enter 13 digits for patient jmbg!";
                }
                else if (doctor == null)
                {
                    ErrorMessage = "Doctor must be selected!";
                }
                else
                {
                    String doctorJmbg = doctor.Jmbg;
                    this.RoomId = doctorController.GetOneDoctor(doctorJmbg).RoomId;
                    DoctorWindowVM.NavigationService.Navigate(new ChooseAppointmentPage(patientJmbg, doctorJmbg,
                        DateFrom, DateTo, 45, Priority, RoomId));
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
