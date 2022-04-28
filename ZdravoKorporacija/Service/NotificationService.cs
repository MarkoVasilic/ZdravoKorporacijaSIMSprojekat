using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;

namespace ZdravoKorporacija.Service
{
    internal class NotificationService
    {
       private readonly NotificationRepository notificationRepository = new NotificationRepository();


        public NotificationService() { }
        public NotificationService(NotificationRepository repository) { this.notificationRepository = repository; }

        private int GenerateNewId()
        {
            try
            {
                List<Notification> notifications = notificationRepository.FindAll();
                int currentMax = notifications.Max(obj => obj.Id);
                return currentMax + 1;
            }
            catch
            {
                return 1;
            }
        }

        public List<Notification> GetNotificationsByPatientJmbg(String userJmbg)
        {
            return notificationRepository.FindAllByUserJmbg(userJmbg);
        }

        public void DeleteNotification(int notificationId)
        {
            if (notificationRepository.FindOneById(notificationId) == null)
            {
                throw new Exception("Appointment with that id doesn't exist!");
            }
            else
            {
                notificationRepository.RemoveNotification(notificationId);
            }
        }

        public List<Notification> GetAllNotifications()
        {
            return notificationRepository.FindAll();
        }
        public Notification GetOneById(int notificationId)
        {
            return notificationRepository.FindOneById(notificationId);
        }

    }
}
