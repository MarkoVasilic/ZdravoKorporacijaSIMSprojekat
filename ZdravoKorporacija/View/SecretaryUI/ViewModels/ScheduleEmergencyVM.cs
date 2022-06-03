using Controller;
using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Input;
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.View.SecretaryUI.Commands;

namespace ZdravoKorporacija.View.SecretaryUI.ViewModels
{
    public class ScheduleEmergencyVM : INotifyPropertyChanged
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Jmbg { get; set; }
        public String SearchJmbg { get; set; }
        public Patient selectedPatient { get; set; }
        private String selectedSpeciality;
        private ObservableCollection<String> doctorSpecialities;
        private ObservableCollection<ModifyAppointmentForEmergencyDto> appointmentsToReschedule;
        public PossibleAppointmentsDTO selectedAppointment;
        public ModifyAppointmentForEmergencyDto selectedRescheduleAppointment;
        public PatientController patientController { get; set; }
        public DoctorController doctorController { get; set; }
        public RoomController roomController { get; set; }
        public NotificationController notificationController { get; set; }
        public AppointmentController appointmentController { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private string errorMessage;
        private string errorMessageSearch;
        private string errorMessagePossibleAppointments;
        private string errorMessageConfirmAppointment;

        public String SelectedSpeciality
        {
            get => selectedSpeciality;
            set
            {
                selectedSpeciality = value;
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
        public ObservableCollection<ModifyAppointmentForEmergencyDto> AppointmentsToReschedule
        {
            get => appointmentsToReschedule;
            set
            {
                appointmentsToReschedule = value;
                OnPropertyChanged("AppointmentsToReschedule");
            }
        }
        public ModifyAppointmentForEmergencyDto SelectedRescheduleAppointment
        {
            get => selectedRescheduleAppointment;
            set
            {
                selectedRescheduleAppointment = value;
                OnPropertyChanged("SelectedRescheduleAppointment");
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
        public string ErrorMessageSearch
        {
            get => errorMessageSearch;
            set
            {
                errorMessageSearch = value;
                OnPropertyChanged("ErrorMessageSearch");
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
            get => selectedPatient;
            set
            {
                selectedPatient = value;
                OnPropertyChanged("SelectedPatient");
            }
        }

        public PossibleAppointmentsDTO SelectedAppointment
        {
            get => selectedAppointment;
            set
            {
                selectedAppointment = value;
                OnPropertyChanged("SelectedAppointment");
            }
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public ScheduleEmergencyVM()
        {
            SecretaryWindowVM.setWindowTitle("Schedule emergency appointment");
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
            BasicRenovationRepository basicRenovationRepository = new BasicRenovationRepository();
            ScheduleService scheduleService = new ScheduleService(appointmentRepository, patientRepository,
                doctorRepository, roomRepository, basicRenovationRepository, advancedRenovationJoining,
                advancedRenovationSeparation, managerRepository, secretaryRepository, meetingRepository);
            EmergencyService emergencyService = new EmergencyService(appointmentRepository, patientRepository,
                doctorRepository, roomRepository, basicRenovationRepository, advancedRenovationJoining,
                advancedRenovationSeparation, scheduleService);
            appointmentController = new AppointmentController(appointmentService, scheduleService, emergencyService);
            NotificationRepository notificationRepository = new NotificationRepository();
            PrescriptionRepository prescriptionRepository = new PrescriptionRepository();
            MedicalRecordRepository medicalRecordRepository = new MedicalRecordRepository();
            MedicationRepository medicationRepository = new MedicationRepository();
            PrescriptionService prescriptionService = new PrescriptionService(prescriptionRepository, medicalRecordRepository, patientRepository, medicationRepository);
            NotificationService notificationService = new NotificationService(notificationRepository, prescriptionService);
            notificationController = new NotificationController(notificationService);
            specialitiesListToStringList(doctorController.GetAllSpecialities());
            CreateGuestAccountCommand = new RelayCommand(createGuestExecute);
            SearchPatientCommand = new RelayCommand(searchPatientExecute);
            SearchAppointmentCommand = new RelayCommand(searchEmergencyAppointmentExecute);
            ConfirmAppointmentCommand = new RelayCommand(confirmAppointmentExecute);
            SelectAppointmentCommand = new RelayCommand(selectAppointmentExecute);
        }
        public ICommand CreateGuestAccountCommand { get; set; }
        public ICommand SearchPatientCommand { get; set; }
        public ICommand SearchAppointmentCommand { get; set; }
        public ICommand SelectAppointmentCommand { get; set; }
        public ICommand ConfirmAppointmentCommand { get; set; }
        private void createGuestExecute(object parameter)
        {
            try
            {
                patientController.CreateGuestAccount(FirstName, LastName, Jmbg);
                SelectedPatient = patientController.GetOnePatient(Jmbg);
                SecretaryWindowVM.NavigationService.Navigate(new EmergencyPatientInformations(this));
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }

        }
        private void searchPatientExecute(object parameter)
        {
            SelectedPatient = patientController.GetOnePatient(SearchJmbg);
            if (SelectedPatient != null)
            {
                ErrorMessageSearch = "";
                SecretaryWindowVM.NavigationService.Navigate(new EmergencyPatientInformations(this));
            }
            else
            {
                Regex r = new Regex("^[0-9]+$");
                if (!r.IsMatch(SearchJmbg))
                {
                    ErrorMessageSearch = "You must enter only digits!";
                }
                else if (SearchJmbg.Length != 13)
                    ErrorMessageSearch = "Length must be 13!";
                else
                    ErrorMessageSearch = "Patient with that jmbg doesn't exist!";
            }

        }

        private void specialitiesListToStringList(List<string> specialities)
        {
            DoctorSpecialities = new ObservableCollection<string>();
            foreach (var ds in specialities)
            {
                DoctorSpecialities.Add(ds);
            }
        }
        private void appointmentListToAppointmentList(List<ModifyAppointmentForEmergencyDto> appointmentsDTOs)
        {
            AppointmentsToReschedule = new ObservableCollection<ModifyAppointmentForEmergencyDto>();
            foreach (var appDto in appointmentsDTOs)
            {
                AppointmentsToReschedule.Add(appDto);
            }
        }
        private void searchEmergencyAppointmentExecute(object parameter)
        {
            try
            {
                SelectedAppointment = appointmentController.FindPossibleEmergencyAppointment(SelectedPatient.Jmbg, SelectedSpeciality);
                if (SelectedAppointment != null)
                    SecretaryWindowVM.NavigationService.Navigate(new ConfirmAppointmentInformations(null, this));
                else
                {
                    appointmentListToAppointmentList(appointmentController.FindAppointmentsToRescheduleForEmergency(SelectedPatient.Jmbg, SelectedSpeciality));
                    SecretaryWindowVM.NavigationService.Navigate(new EmergencyRescheduleAppointments(this));
                }
            }
            catch (Exception e)
            {
                ErrorMessagePossibleAppointments = e.Message;
            }
        }

        private void selectAppointmentExecute(object parameter)
        {
            SelectedRescheduleAppointment = parameter as ModifyAppointmentForEmergencyDto;
            SelectedAppointment = new PossibleAppointmentsDTO(SelectedPatient.Jmbg, SelectedPatient.FirstName + " " + SelectedPatient.LastName,
                SelectedRescheduleAppointment.DoctorJmbg, SelectedRescheduleAppointment.DoctorFullName, SelectedRescheduleAppointment.DoctorSpeciality,
                SelectedRescheduleAppointment.RoomId, SelectedRescheduleAppointment.RoomName, DateTime.Now.AddMinutes(5), 60, -1);
            SecretaryWindowVM.NavigationService.Navigate(new ConfirmAppointmentInformations(null, this));
        }

        private void confirmAppointmentExecute(object parameter)
        {
            try
            {
                if (SelectedRescheduleAppointment != null)
                {
                    appointmentController.ModifyAppointment(SelectedRescheduleAppointment.AppointmentId, SelectedRescheduleAppointment.NewStartTime);
                    notificationController.CreatePatientNotificationForAppointmentReschedule(SelectedRescheduleAppointment.PatientJmbg, SelectedRescheduleAppointment.DoctorFullName,
                    SelectedRescheduleAppointment.OldStartTime, SelectedRescheduleAppointment.NewStartTime, SelectedRescheduleAppointment.RoomName);
                    notificationController.CreateDoctorNotificationForEmergency(SelectedRescheduleAppointment.DoctorJmbg, SelectedRescheduleAppointment.PatientFullName,
                        SelectedRescheduleAppointment.OldStartTime, SelectedRescheduleAppointment.NewStartTime, SelectedAppointment.StartTime, SelectedRescheduleAppointment.RoomName);
                }
                appointmentController.CreateAppointmentBySecretary(SelectedAppointment.PatientJmbg, SelectedAppointment.DoctorJmbg,
                    SelectedAppointment.RoomId, SelectedAppointment.StartTime, SelectedAppointment.Duration);
                SecretaryWindowVM.NavigationService.Navigate(new AppointmentView());
            }
            catch (Exception e)
            {
                ErrorMessageConfirmAppointment = e.Message;
            }

        }
    }
}
