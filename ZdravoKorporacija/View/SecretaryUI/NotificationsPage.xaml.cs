using Repository;
using Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.View.SecretaryUI
{
    /// <summary>
    /// Interaction logic for NotificationsPage.xaml
    /// </summary>
    public partial class NotificationsPage : Page, INotifyPropertyChanged
    {
        private NotificationController notificationController { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Notification> notifications;

        public ObservableCollection<Notification> Notifications
        {
            get => notifications;
            set
            {
                notifications = value;
                OnPropertyChanged("Notifications");
            }
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public NotificationsPage()
        {
            InitializeComponent();
            this.DataContext = this;
            NotificationRepository notificationRepository = new NotificationRepository();
            PrescriptionRepository prescriptionRepository = new PrescriptionRepository();
            MedicalRecordRepository medicalRecordRepository = new MedicalRecordRepository();
            MedicationRepository medicationRepository = new MedicationRepository();
            PatientRepository patientRepository = new PatientRepository();
            PrescriptionService prescriptionService = new PrescriptionService(prescriptionRepository,
                medicalRecordRepository, patientRepository, medicationRepository);
            NotificationService notificationService =
                new NotificationService(notificationRepository, prescriptionService);
            notificationController = new NotificationController(notificationService);
            notificationToObserableList(notificationController.GetAllByUserJmbg(App.loggedUser.Jmbg));
        }

        private void notificationToObserableList(List<Notification> notifications)
        {
            Notifications = new ObservableCollection<Notification>();
            foreach (var not in notifications)
            {
                Notifications.Add(not);
            }
        }
    }
}
