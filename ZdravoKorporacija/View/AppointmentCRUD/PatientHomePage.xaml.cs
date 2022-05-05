using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using Service;
using Repository;

namespace ZdravoKorporacija.View.AppointmentCRUD
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
            notificationService.CreatePatientNotifications(); // ova linija ce ici u prescriptionService CreatePrescription
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
    }
}
