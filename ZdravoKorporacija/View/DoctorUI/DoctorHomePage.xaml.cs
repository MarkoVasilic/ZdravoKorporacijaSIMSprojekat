using System;
using System.Collections.Generic;
using Controller;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Repository;
using Service;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.View.DoctorUI.ViewModel;

namespace ZdravoKorporacija.View.DoctorUI
{
    /// <summary>
    /// Interaction logic for DoctorHomePage.xaml
    /// </summary>
    public partial class DoctorHomePage : Page, INotifyPropertyChanged
    {
        private AppointmentController appointmentController;
        private String jmbg { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnProperyChanged(string? propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DateTime dateFrom;
        public DateTime DateFrom
        {
            get { return dateFrom; }
            set
            {
                dateFrom = value;
                OnProperyChanged("DateFrom");

            }
        }

        public DateTime dateTo;
        public DateTime DateTo
        {
            get { return dateTo; }
            set
            {
                dateTo = value;
                OnProperyChanged("DateTo");
            }
        }

        public ObservableCollection<AppointmentDTO> appointments { get; set; }
        /*public ObservableCollection<AppointmentDTO> Appointments
        {
            get => appointments;
            set
            {
                appointments = value;
                OnPropertyChanged("Appointments");
            }
        }*/
        public DoctorHomePage(DoctorWindowVM doctorWindowVM)
        {
            InitializeComponent();
            DoctorWindowVM.setWindowTitle("Appointment Schedule");
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            DoctorRepository doctorRepository = new DoctorRepository();
            PatientRepository patientRepository = new PatientRepository();
            RoomRepository roomRepository = new RoomRepository();
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
            //this.DataContext = doctorWindowVM;
            this.DataContext = this;
            appointmentController = new AppointmentController(appointmentService, scheduleService, emergencyService);
            appointments = new ObservableCollection<AppointmentDTO>(appointmentController.GetAppointmentsByDoctorJmbgDTO(App.loggedUser.Jmbg));
        }


        private void ViewMedicalRecordButton_OnClick(object sender, RoutedEventArgs e)
        {
            String Jmbg = (String)((Button)sender).CommandParameter;
            this.jmbg = Jmbg;
            NavigationService.Navigate(new ViewMedicalRecordPage(Jmbg));
        }

        private void CreateAppointmentButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateDoctorAppointmentPage());
        }

        private void ScheduleSurgeryButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ScheduleSurgeryPage());
        }

        private void EditButton_OnClick(object sender, RoutedEventArgs e)
        {
            AppointmentDTO appoinmeDTO = (AppointmentDTO)((Button)sender).CommandParameter;
            int id = appoinmeDTO.Id;
            NavigationService.Navigate(new ModifyDoctorAppointmentPage(id));
        }

        private void DeleteAppointmnetButton_OnClick(object sender, RoutedEventArgs e)
        {
            AppointmentDTO appoinmeDTO = (AppointmentDTO)((Button)sender).CommandParameter;
            int id = appoinmeDTO.Id;
            appointmentController.DeleteAppointment(id);
        }

        private void FilterAppointmnetsButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.appointments = new ObservableCollection<AppointmentDTO>(appointmentController.FilterByTime(DateFrom, DateTo));
        }
    }
}
