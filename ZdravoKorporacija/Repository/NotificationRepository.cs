using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Newtonsoft.Json;

using System.IO;
using ZdravoKorporacija.Model;

namespace ZdravoKorporacija.Repository
{
    internal class NotificationRepository
    {
        private readonly String NotificationFilePath = @"..\..\..\Resources\Notifications.json";

        public NotificationRepository() { }
        public List<Notification> FindAllByUserJmbg(String Jmbg)
        {
            var values = GetValues();
            List<Notification> result = new List<Notification>();
            foreach (Notification notification in values)
                if (notification.userJmbg.Equals(Jmbg))
                    result.Add(notification);
            return result;
        }

        public void SaveNotification(Notification notificationToSave)
        {
            var values = GetValues();
            values.Add(notificationToSave);
            Save(values);
        }

        public void RemoveNotification(int notificationId)
        {
            var values = GetValues();
            values.RemoveAll(val => val.Id == notificationId);
            Save(values);
        }

        public List<Notification> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<Notification>>(File.ReadAllText(NotificationFilePath));
            if (values == null)
            {
                values = new List<Notification>();
            }

            return values;
        }

        public void Save(List<Notification> values)
        {
            File.WriteAllText(NotificationFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }

        public List<Notification> FindAll()
        {
            var values = GetValues();
            return values;
        }

        public Notification? FindOneById(int notificationId)
        {
            var values = GetValues();
            foreach (var val in values)
            {
                if (val.Id == notificationId)
                {
                    return val;
                }
            }

            return null;
        }

    }
}
