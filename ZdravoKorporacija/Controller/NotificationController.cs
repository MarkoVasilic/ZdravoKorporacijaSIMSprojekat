using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.Controller
{
    public class NotificationController
    {

        private readonly NotificationService notificationService = new NotificationService();

       public  NotificationController(NotificationService Service)
        {
            this.notificationService = Service;
        }

        public List<Notification> GetAllNotifications()
        {
            return notificationService.GetAllNotifications();
        }

        public List<Notification> GetAllFutureAppointmentsByPatient()
        {
            return notificationService.GetNotificationsByPatientJmbg(App.loggedUser.Jmbg);
        }

        public void DeleteNotification(int notificationId)
        {
            notificationService.DeleteNotification(notificationId);
        }

        public Notification GetOneById(int notificationId)
        {
            return notificationService.GetOneById(notificationId);
        }
    }
}
