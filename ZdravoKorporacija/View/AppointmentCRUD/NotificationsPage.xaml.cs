using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for NotificationsPage.xaml
    /// </summary>
    public partial class NotificationsPage : Page
    {
        private PrescriptionService prescriptionService { set; get; }

        private NotificationService notificationService { set; get; }

        private ObservableCollection<Notification> NotificationListObservable { get; set; }  
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

            NotificationListObservable = new ObservableCollection<Notification>(notificationService.ShowPatientNotification());

        }

        private void ButtonNazad(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
