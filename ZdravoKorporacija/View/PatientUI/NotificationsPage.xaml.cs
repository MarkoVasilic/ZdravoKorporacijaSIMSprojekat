using Repository;
using Service;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using Service;
using Repository;
using System.Data;

namespace ZdravoKorporacija.View.PatientUI
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
            NavigationService.Navigate(new PatientHomePage());
        }

        private void DeleteNotificationButton(object sender, RoutedEventArgs e)
        {
            /*  Console.WriteLine(((Button)sender).Tag as string);
              int ID = Int32.Parse(((Button)sender).Tag as string);
              Console.Write("ID = " + ID);
              notificationService.DeleteNotification(ID); */
            Notification obj = ((FrameworkElement)sender).DataContext as Notification;
            MessageBox.Show("Notifikacija uspjesno obrisana!");
            notificationService.DeleteNotification(obj.Id);
            NavigationService.Navigate(new NotificationsPage());
        }

        private void ButtonCreateNotification(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateNotificationPage());
        }
    }
}
