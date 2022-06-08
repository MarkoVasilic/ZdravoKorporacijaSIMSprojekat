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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for NotificationWindow.xaml
    /// </summary>
    public partial class NotificationWindow : Window, INotifyPropertyChanged
    {
        private NotificationController notificationController;
        private ObservableCollection<Notification> notifications;
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<Notification> Notifications
        {
            get => notifications;
            set
            {
                if (notifications != value)
                {
                    notifications = value;
                    OnPropertyChanged("Notifications");
                }
            }
        }



        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }


        public NotificationWindow()
        {
            InitializeComponent();
            NotificationRepository notificationRepository = new NotificationRepository();
            PrescriptionRepository prescriptionRepository = new PrescriptionRepository();
            MedicalRecordRepository medicicalRecordRepository = new MedicalRecordRepository();
            PatientRepository patientRepository = new PatientRepository();
            MedicationRepository medicationRepository = new MedicationRepository();
            PrescriptionService prescriptionService = new PrescriptionService(prescriptionRepository, medicicalRecordRepository, patientRepository, medicationRepository);
            NotificationService notificationService = new NotificationService(notificationRepository, prescriptionService);
            notificationController = new NotificationController(notificationService);
            notifications = new ObservableCollection<Notification>(notificationController.GetAllByUserJmbg("3434343434343"));
            this.DataContext = this;

        }

        private void Notification_Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Delete_Room_Click(object sender, RoutedEventArgs e)
        {
            NotificationWindow notificationWindow = new NotificationWindow();
            int notificationId = (int)((Button)sender).Tag;
            if (notificationId == null) return;
            notificationController.Delete(notificationId);
            notifications.Remove(notifications.Where(notification => notification.Id == notificationId).Single());
            this.Close();
            NotificationWindow notificationWindow1 = new NotificationWindow();
            notificationWindow1.Show();
            

        }
    }
}
