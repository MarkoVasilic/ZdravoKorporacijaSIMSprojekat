using Controller;
using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.View.PatientUI.Commands;

namespace ZdravoKorporacija.View.PatientUI.ViewModels
{
    public class CreateAppointmentVM : INotifyPropertyChanged
    {
        public DoctorController doctorController { get; set; }
        public AppointmentController appointmentController { get; set; }
        private Patient? selectedPatient { get; set; }
        private Doctor? selectedDoctor { get; set; }
        private String? selectedPriority { get; set; }

        private String timePriority { get; set; }

        private String doctorPriority { get; set; }

        private CreateAppointmentPage createAppointmentPage;

        private PossibleAppointmentsDTO selectedAppointment;
        private DateTime dateFrom;
        private DateTime dateUntil;
        private ObservableCollection<Doctor> doctors;
        private ObservableCollection<PossibleAppointmentsDTO> possibleAppointments;
        private ObservableCollection<PossibleAppointmentsDTO> futureAppointments;
        private String errorMessagePossibleAppointments;
        private String errorMessageConfirmAppointment;


        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand GetAllPossibleAppointmentsPatient { get; set; }

        public ICommand GetAllFutureAppointmentsPatient { get; set; }
        public ICommand SelectedAppointmentCommand { get; set; }

        public ICommand GoBackCommand { get; set; }



        public ObservableCollection<Doctor> Doctors
        {
            get => doctors;
            set
            {
                doctors = value;
                OnPropertyChanged("Doctors");
            }
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
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

        public ObservableCollection<PossibleAppointmentsDTO> FutureAppointments
        {
            get => futureAppointments;
            set
            {
                futureAppointments = value;
                OnPropertyChanged("FutureAppointments");
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
        public String SelectedPriority
        {
            get { return selectedPriority; }
            set
            {
                selectedPriority = value;
                OnPropertyChanged("SelectedPriority");
            }
        }
        public String DoctorPriority
        {
            get { return doctorPriority; }
            set
            {
                doctorPriority = value;
                OnPropertyChanged("DoctorPriority");
            }
        }

        public String TimePriority
        {
            get { return timePriority; }
            set
            {
                timePriority = value;
                OnPropertyChanged("TimePriority");
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

        public Doctor SelectedDoctor
        {
            get { return selectedDoctor; }
            set
            {
                selectedDoctor = value;
                OnPropertyChanged("SelectedDoctor");
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




        public CreateAppointmentVM()
        {
            DoctorRepository doctorRepository = new DoctorRepository();
            DoctorService doctorService = new DoctorService(doctorRepository);
            doctorController = new DoctorController(doctorService);
            PatientRepository patientRepository = new PatientRepository();
            RoomRepository roomRepository = new RoomRepository();
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            AppointmentService appointmentService = new AppointmentService(appointmentRepository,patientRepository,doctorRepository,roomRepository);
            ManagerRepository managerRepository = new ManagerRepository();
            SecretaryRepository secretaryRepository = new SecretaryRepository();
            MeetingRepository meetingRepository = new MeetingRepository();
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
            doctorsListToDoctorList(doctorController.getAllBySpeciality("Physician"));

            GetAllPossibleAppointmentsPatient = new RelayCommand(possibleAppointmentsPatientExecute);
            GetAllFutureAppointmentsPatient = new RelayCommand(futureAppointmentsPatientExecute);
            selectedPriority = null;

        }


        public void SelectAppointment()
        {
            //ISKLJUCIVO ZA POTREBE ODBRANE
            /*
            selectedPriority = null;

            if (timePriority == null)
                selectedPriority = "doctor";
            else
                selectedPriority = "time";
            MessageBox.Show("selected = " + selectedPriority);*/
            try
            {
                appointmentController.CreateAppointmentByPatient(selectedAppointment.StartTime, selectedAppointment.DoctorJmbg);
                
                 MessageBox.Show("Uspješno zakazan pregled za \n Datum: " + selectedAppointment.StartTime, "USPJEŠNO!", MessageBoxButton.OK,MessageBoxImage.Information);
                Thread.Sleep(500);
                PatientWindowVM.NavigationService.Navigate(new Calendar());

            }
            catch (Exception ex)
            {
                ErrorMessagePossibleAppointments = "Molim Vas unesite validan datum!";
            }
        }

        public void SelectFutureAppointment()
        {
            try
            {
                appointmentController.CreateAppointmentByPatient(selectedAppointment.StartTime, selectedAppointment.DoctorJmbg);
                //  MessageBox.Show("Uspjesno zakazan pregled za " + selectedAppointment.StartTime);
                ErrorMessageConfirmAppointment = "Uspješno zakazan pregled! " + selectedAppointment.StartTime;
                PatientWindowVM.NavigationService.Navigate(new GetAllAppointmentsPatient());

            }
            catch (Exception ex)
            {
                ErrorMessageConfirmAppointment = "Molim Vas unesite validan datum!";
            }
        }

        private void possibleAppointmentsPatientExecute(object sender)
        {
            selectedPriority = "";

            if (timePriority == null)
            {
                selectedPriority = "doctor";
            }
            else
                selectedPriority = "time";
            // MessageBox.Show("selected = " + selectedPriority);
            ErrorMessagePossibleAppointments = "";
            ErrorMessageConfirmAppointment = "";
            if ((SelectedDoctor == null) || (DateFrom.Equals(null)) || (DateUntil.Equals(null)) || (dateFrom.Equals(null)) || (dateUntil.Equals(null)) || DateFrom.Year == 1 || DateUntil.Year == 1)
            {
                ErrorMessagePossibleAppointments = "Sva polja moraju biti popunjena, pokušajte ponovo!";
            }
            else
            {
                try
                {

                    appointmentListToAppointmentList(appointmentController.GetPossibleAppointmentsBySecretary(App.loggedUser.Jmbg, SelectedDoctor.Jmbg, SelectedDoctor.RoomId,
                                DateFrom, DateUntil, 45, SelectedPriority));
                    PatientWindowVM.NavigationService.Navigate(new PossibleAppointmentPatientPage(this));
                    ErrorMessageConfirmAppointment = "";
                }
                catch (Exception e)
                {
                    ErrorMessagePossibleAppointments = "Molim Vas unesite validan datum!";
                }
            }
        }


        private void futureAppointmentsPatientExecute(object sender)
        {
            try
            {
                futureAppointmentListToAppointmentList(appointmentController.GetAllFutureAppointmentsByPatient());
                PatientWindowVM.NavigationService.Navigate(new UpdateFutureAppointmentsPage());
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.ToString());

            }
        }


        private void doctorsListToDoctorList(List<Doctor> doctors)
        {
            try
            {
                Doctors = new ObservableCollection<Doctor>();
                foreach (var d in doctors)
                {
                    d.FirstName = d.FirstName + " " + d.LastName;
                    Doctors.Add(d);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.ToString());
            }
        }

        private void appointmentListToAppointmentList(List<PossibleAppointmentsDTO> appointmentsDTOs)
        {
            try
            {
                PossibleAppointments = new ObservableCollection<PossibleAppointmentsDTO>();
                foreach (var pad in appointmentsDTOs)
                {
                    PossibleAppointments.Add(pad);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.ToString());
            }
        }

        private void futureAppointmentListToAppointmentList(List<PossibleAppointmentsDTO> appointmentsDTOs)
        {
            try
            {
                FutureAppointments = new ObservableCollection<PossibleAppointmentsDTO>();
                foreach (var pad in appointmentsDTOs)
                {
                    FutureAppointments.Add(pad);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.ToString());
            }
        }


        private void searchAppointmentExecute(object parameter)
        {
            if (SelectedDoctor == null || DateFrom.Year == 1 || DateUntil.Year == 1)
            {
                ErrorMessagePossibleAppointments = "Unesite sva polja!";
            }
            else
            {
                ErrorMessagePossibleAppointments = "";
                try
                {
                    appointmentListToAppointmentList(appointmentController.GetPossibleAppointmentsBySecretary(App.loggedUser.Jmbg, SelectedDoctor.Jmbg, 1,
                        DateFrom, DateUntil, 45, SelectedPriority));
                    ErrorMessagePossibleAppointments = "";
                }
                catch (Exception e)
                {
                    ErrorMessagePossibleAppointments = e.Message;
                }
            }
        }

        private void searchFutureAppointmentExecute(object parameter)
        {
            if (SelectedDoctor == null || DateFrom.Year == 1 || DateUntil.Year == 1)
            {
                ErrorMessagePossibleAppointments = "All fields are necessary!";
            }
            else
            {
                ErrorMessagePossibleAppointments = "";
                try
                {
                    futureAppointmentListToAppointmentList(appointmentController.GetAllFutureAppointmentsByPatient());
                    ErrorMessagePossibleAppointments = "";
                }
                catch (Exception e)
                {
                    ErrorMessagePossibleAppointments = e.Message;
                }
            }
        }
        private void selectAppointmentExecute(object parameter)
        {
            selectedAppointment = parameter as PossibleAppointmentsDTO;

        }
        private void confirmAppointmentExecute(object parameter)
        {
            try
            {
                appointmentController.CreateAppointmentByPatient(DateFrom, selectedDoctor.Jmbg);

            }
            catch (Exception e)
            {
                ErrorMessageConfirmAppointment = e.Message;
            }

        }

        private void selectFutureAppointmentExecute(object parameter)
        {
            selectedAppointment = parameter as PossibleAppointmentsDTO;
        }
        private void confirmFutureAppointmentExecute(object parameter)
        {
            try
            {
                appointmentController.CreateAppointmentByPatient(DateFrom, selectedDoctor.Jmbg);
                errorMessageConfirmAppointment = "Uspješno zakazan pregled! ";

            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.ToString());
            }

        }


    }
}
