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
using Repository;
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.View.DoctorUI
{
    /// <summary>
    /// Interaction logic for NotificationsPage.xaml
    /// </summary>
    public partial class NotificationsPage : Page
    {
        private NotificationController notificationController { get; set; }
        public ObservableCollection<Notification> notifications { get; set; }
        public NotificationsPage()
        {
            InitializeComponent();
            NotificationRepository notificationRepository = new NotificationRepository();
            PrescriptionService prescriptionService = new PrescriptionService();
            NotificationService notificationService =
                new NotificationService(notificationRepository, prescriptionService);
            notificationController = new NotificationController(notificationService);
            this.DataContext = this;
            notifications = new ObservableCollection<Notification>(notificationController.GetAllByUserJmbg(App.loggedUser.Jmbg));
        }
    }
}
