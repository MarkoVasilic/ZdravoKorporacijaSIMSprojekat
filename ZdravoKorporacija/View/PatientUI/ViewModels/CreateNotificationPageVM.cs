using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.View.PatientUI.Commands;

namespace ZdravoKorporacija.View.PatientUI.ViewModels
{
    public  class CreateNotificationPageVM : INotifyPropertyChanged
    {
        public Notification Notification { get; set; }
        public PrescriptionService PrescriptionService { private set; get; }
        public NotificationService NotificationService { private set; get; }
        


        public CreateNotificationPageVM()
        {
            PrescriptionService = new PrescriptionService(new PrescriptionRepository(), new MedicalRecordRepository(), new PatientRepository(), new MedicationRepository());
            NotificationService = new NotificationService(new NotificationRepository(), PrescriptionService);
            CreateNotificationCommand = new RelayCommand(CreateExecute, CreateCanExecute);
            BackCommand = new RelayCommand(BackExecute);
            Notification = new Notification();
            dateTime = DateTime.Now;
            Notification.userJmbg = App.loggedUser.Jmbg;
        }

        public RelayCommand CreateNotificationCommand { get; private set; }
        public RelayCommand BackCommand { get; set; }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private DateTime dateTime;
        public DateTime SelectedDateTime
        {
            get => dateTime;
            set
            {
                dateTime = value;
                OnPropertyChanged("SelectedDateTime");
            }
        }

        private String description;
        public String Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }

        private String title;

        public event PropertyChangedEventHandler? PropertyChanged;

        public String Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }


        public void CreateExecute(object parameter)
        {
            NotificationService.CreateNotification(title,description,dateTime,App.loggedUser.Jmbg,false);
            MessageBox.Show("Uspješno kreirana notifikacija! \n  "+Notification.Description, "USPJEŠNO!", MessageBoxButton.OK, MessageBoxImage.None);
            PatientWindowVM.NavigationService.Navigate(new NotificationsPage());

        }

        public bool CreateCanExecute(object parameter)
        {
            if (Title == null || Description == null || Description.Length < 3)
            {
                return false;
            }
            return true;
        }

        private void BackExecute(object parameter)
        {
            PatientWindowVM.NavigationService.Navigate(new NotificationsPage());
        }

        public bool validateNote()
        {
            if(Title == null)
            {
                return false;
            }   
            if(Description == null)
            {
                return false;
            }
          //  if(notification.StartTime < System.DateTime.Now)
            //{
              //  return false;
            //}
            return true;
        }



    }
}
