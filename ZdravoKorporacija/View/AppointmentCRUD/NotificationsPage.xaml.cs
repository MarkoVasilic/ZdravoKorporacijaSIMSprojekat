using Repository;
using Service;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.View.AppointmentCRUD
{
    /// <summary>
    /// Interaction logic for NotificationsPage.xaml
    /// </summary>
    public partial class NotificationsPage : Page
    {
        private PrescriptionService prescriptionService { set; get; }

        private NotificationService notificationService { set; get; }

        public List<Notification> NotificationListObservable { get; set; }
        public NotificationsPage()
        {
            InitializeComponent();
            DataContext = this;
            PrescriptionRepository prescriptionRepository = new PrescriptionRepository();
            MedicalRecordRepository medicalRecordRepository = new MedicalRecordRepository();
            NotificationRepository notificationRepository = new NotificationRepository();
            PatientRepository patientRepository = new PatientRepository();
            MedicationRepository medicationRepository = new MedicationRepository();
            prescriptionService = new PrescriptionService(prescriptionRepository, medicalRecordRepository, patientRepository, medicationRepository);
            notificationService = new NotificationService(notificationRepository, prescriptionService); ;

            NotificationListObservable = new List<Notification>(notificationService.ShowPatientNotification());

        }

        private void ButtonNazad(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
