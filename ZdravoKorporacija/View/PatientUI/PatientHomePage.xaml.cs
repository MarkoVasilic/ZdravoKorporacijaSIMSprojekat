using Repository;
using Service;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.View.PatientUI
{
    /// <summary>
    /// Interaction logic for PatientHomePage.xaml
    /// </summary>
    public partial class PatientHomePage : Page
    {
        private PrescriptionService prescriptionService { set; get; }

        private NotificationService notificationService { set; get; }
        public PatientHomePage()
        {
            InitializeComponent();
            PrescriptionRepository prescriptionRepository = new PrescriptionRepository();
            MedicalRecordRepository medicalRecordRepository = new MedicalRecordRepository();
            NotificationRepository notificationRepository = new NotificationRepository();
            PatientRepository patientRepository = new PatientRepository();
            MedicationRepository medicationRepository = new MedicationRepository();
            prescriptionService = new PrescriptionService(prescriptionRepository, medicalRecordRepository, patientRepository, medicationRepository);
            notificationService = new NotificationService(notificationRepository, prescriptionService);
        }

        private void AppointmentsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AppointmentPage());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Notification> notificationList = new List<Notification>();
            // notificationList = notificationService.ShowPatientNotification();
            // foreach (Notification notification in notificationList)
            //   notification.ToStringNotification();
            NavigationService.Navigate(new NotificationsPage());
        }

        private void logOutButton(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(null);
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

        }

        private void FutureAppointmentButton(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Calendar());
        }

        private void PastAppointmentsButton(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GetAllAppointmentsPatient ());
        }

        private void CreateAppointmentButton(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateAppointmentPage());
        }

        private void TherapyButton(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PatientTherapyPage());
        }

        private void NotesButton(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PatientNotesPage());
        }

        private void KartonButton(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PatientMedicalRecordPage());
        }
    }
}
