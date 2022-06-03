using Controller;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.View.PatientUI.Commands;

namespace ZdravoKorporacija.View.PatientUI.ViewModels
{
    public class CalendarVM : INotifyPropertyChanged
    {
        public ObservableCollection<PossibleAppointmentsDTO> appointments;

        private AppointmentController appointmentController;

        public DateTime selectedDate;

        public event PropertyChangedEventHandler? PropertyChanged;

        public CalendarVM()
        {
            setCommands();
            setProperties();
        }


        #region Commands
        public RelayCommand BackCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }

        public RelayCommand PostponeCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        #endregion


        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                OnPropertyChanged("SelectedDate");
            }
        }
        private void setCommands()
        {
            BackCommand = new RelayCommand(BackExecute);
            SearchCommand = new RelayCommand(SearchExecute);
            PostponeCommand = new RelayCommand(PostponeExecute);
            DeleteCommand = new RelayCommand(DeleteExecute);
        }

        private void DeleteExecute(object parameter)
        {
            
           var result = MessageBox.Show("Da li ste sigurni da zelite otkazati termin? \n Termin ID: " + (int)parameter,"OTKAZIVANJE TERMINA",MessageBoxButton.YesNo,MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                if(App.patientController.getTrollCounterByPatient(App.loggedUser.Jmbg) >= 4)
                {
                    MessageBox.Show("Blokirani ste zbog AntiTroll mehanizma \n TrollCounter: " + App.patientController.getTrollCounterByPatient(App.loggedUser.Jmbg),"BLOKIRAN!",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                    System.Environment.Exit(0);
                    
                }
                App.patientController.incrementTrollCounterByPatient(App.loggedUser.Jmbg);
                appointmentController.DeleteAppointment((int)parameter);
                appointments.Remove(appointments.Where(appointment => appointment.AppointmentId == (int)parameter).Single());
            }
        }

        private void PostponeExecute(object parameter)
        {
            
            var result = MessageBox.Show("Da li ste sigurni da zelite odloziti termin? \n Termin ID: " + (int)parameter, "ODLAGANJE TERMINA", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                PatientWindowVM.NavigationService.Navigate(new UpdateAppointmentPage((int)parameter));
            }
            
        }

        private void BackExecute(object parameter)
        {
            PatientWindowVM.NavigationService.Navigate(new PatientHomePage());
        }

        private void SearchExecute(object parameter)
        {
            Appointments = new ObservableCollection<PossibleAppointmentsDTO>(appointmentController.GetAllByJmbgAndDate(selectedDate));
            if (Appointments.Count == 0)
            {
                MessageBox.Show("Nemate zakazanih termina u odabranom periodu!", "UPOZORENJE", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                Console.WriteLine("S");
                //MessageBox.Show("Odabrali ste datum pretrage: " + selectedDate.Date,"USPJESNO",MessageBoxButton.OK,MessageBoxImage.None);
        }

        private void setProperties()
        {
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            RoomRepository roomRepository = new RoomRepository();
            DoctorRepository doctorRepository = new DoctorRepository();
            PatientRepository patientRepository = new PatientRepository();
            AppointmentService appointmentService = new AppointmentService(appointmentRepository, patientRepository, doctorRepository, roomRepository);
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
            Appointments = new ObservableCollection<PossibleAppointmentsDTO>(appointmentController.GetAllFutureAppointmentsByPatient());
            SelectedDate = new DateTime();
        }

        public ObservableCollection<PossibleAppointmentsDTO> Appointments
        {
            get { return appointments; }
            set
            {
                appointments = value;
                OnPropertyChanged("Appointments");
            }
        }
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
