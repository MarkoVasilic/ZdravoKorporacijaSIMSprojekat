using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.View.PatientUI.ViewModels
{
    public class DeleteNotificationVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public NotificationService notificationService { get; set; }

        private Notification selectedNotification { get; set; }


        public ICommand deleteNotificationCommand { get; set; }



        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public Notification SelectedNotification
        {
            get { return selectedNotification; }
            set
            {
                selectedNotification = value;
                OnPropertyChanged("SelectedNotification");
            }


        }

        public DeleteNotificationVM()
        {
            NotificationRepository notificationRepository = new NotificationRepository();
            PrescriptionRepository prescriptionRepository = new PrescriptionRepository();
            PrescriptionService prescriptionService = new PrescriptionService();
            notificationService = new NotificationService(notificationRepository, prescriptionService);
        }


    }
}
