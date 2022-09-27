using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Navigation;
using Controller;
using Repository;
using Service;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.View.DoctorUI.Commands;

namespace ZdravoKorporacija.View.DoctorUI.ViewModel
{
    public class DoctorWindowVM
    {
        public static DoctorWindow? DoctorWindow;
        public static DoctorHomePage DoctorHomePage;
        public static MedicalRecords medicalRecords;
        public static NavigationService? NavigationService;
        public AppointmentController appointmentController;
        private ObservableCollection<AppointmentDTO> appointments { get; set; }

        public ICommand HomeCommand { get; set; }   //Appointments
        public ICommand PatientsCommand { get; set; }
        public ICommand VerificationsCommand { get; set; }
        public ICommand AbsenceRequestsCommand { get; set; }
        public ICommand LogOutCommand { get; set; }
        //public ICommand ProfileCommand { get; set; }
        public ICommand NotificationsCommand { get; set; }
        public ICommand ViewMedicalRecordCommand { get; set; }

        public static void setWindowTitle(string newTitle)
        {
            DoctorWindow.WindowTitle.Text = newTitle;
        }

        public DoctorWindowVM(DoctorWindow doctorWindow)
        {
            DoctorWindow = doctorWindow;
            DoctorHomePage = new DoctorHomePage(this);
            DoctorWindow.DoctorMainFrame.Content = DoctorHomePage;
            NavigationService = DoctorWindow.DoctorMainFrame.NavigationService;
            HomeCommand = new RelayCommand(homeExecute);
            LogOutCommand = new RelayCommand(logOutExecute);
            PatientsCommand = new RelayCommand(medicalRecordsExecute);
            ViewMedicalRecordCommand = new RelayCommand(viewMedicalRecordExecute);
            AbsenceRequestsCommand = new RelayCommand(viewAbsenceRequestsExecute);
            VerificationsCommand = new RelayCommand(verificationsExecute);
            NotificationsCommand = new RelayCommand(notificationsExecute);
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            DoctorRepository doctorRepository = new DoctorRepository();
            PatientRepository patientRepository = new PatientRepository();
            RoomRepository roomRepository = new RoomRepository();
            AppointmentService appointmentService = new AppointmentService(appointmentRepository, patientRepository, doctorRepository, roomRepository);
        }

        public DoctorWindowVM()
        {
        }

        private void homeExecute(object parametar)
        {
            setWindowTitle("Appointment Schedule");
            DoctorWindowVM doctorWindowVm = new DoctorWindowVM();
            NavigationService.Navigate(new DoctorHomePage(doctorWindowVm));
        }

        private void medicalRecordsExecute(object parametar)
        {
            setWindowTitle("Patients");
            NavigationService.Navigate(new MedicalRecords());
        }

        private void viewMedicalRecordExecute(object parametar)
        {
            setWindowTitle("Medical Record");
            NavigationService.Navigate(new ViewMedicalRecordPage(parametar as String));
        }

        private void viewAbsenceRequestsExecute(object parametar)
        {
            setWindowTitle("Absence Requests");
            NavigationService.Navigate(new AbsenceRequestsPage());
        }
        private void verificationsExecute(object parametar)
        {
            setWindowTitle("Verifications");
            NavigationService.Navigate(new VerificationsPage());
        }
        private void notificationsExecute(object parametar)
        {
            setWindowTitle("Notifications");
            NavigationService.Navigate(new NotificationsPage());
        }

        private void logOutExecute(object parameter)
        {
            MainWindow window = new MainWindow();
            DoctorWindow.Close();
            window.Show();
        }






    }
}
