using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public Regex onlyNumber { get; set; }

        public int hours;
        public int minutes;
        


        public CreateNotificationPageVM()
        {
            PrescriptionService = new PrescriptionService(new PrescriptionRepository(), new MedicalRecordRepository(), new PatientRepository(), new MedicationRepository());
            NotificationService = new NotificationService(new NotificationRepository(), PrescriptionService);
            CreateNotificationCommand = new RelayCommand(CreateExecute, CreateCanExecute);
            BackCommand = new RelayCommand(BackExecute);
            Notification = new Notification();
            dateTime = DateTime.Now;
            Notification.userJmbg = App.loggedUser.Jmbg;
            Hours = 14;
            Minutes = 59;
            onlyNumber = new Regex("[0-9]{1,2}");
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

        public int Hours
        {
            get => hours;
            set
            {
                hours = value;
                OnPropertyChanged("Hours");
            }
        }

        public int Minutes
        {
            get => minutes;
            set
            {
                minutes = value;
                OnPropertyChanged("Minutes");
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
            DateTime temp = new DateTime(dateTime.Year,dateTime.Month,dateTime.Day,hours,minutes,dateTime.Second);
            dateTime = temp;
            NotificationService.Create(title,description,dateTime,App.loggedUser.Jmbg,false);
            MessageBox.Show("Uspješno kreirana notifikacija! \n  "+dateTime, "USPJEŠNO!", MessageBoxButton.OK, MessageBoxImage.None);
            PatientWindowVM.NavigationService.Navigate(new NotificationsPage());

        }

        public bool CreateCanExecute(object parameter)
        {
            String hoursString = "" + Hours;
            String minutesString = "" + Minutes;
            
                if (Title == null
                    || Description == null
                    || Description.Length < 3
                    || Hours == null
                    || Minutes == null
                    || !onlyNumber.IsMatch(hoursString)
                    || !onlyNumber.IsMatch(minutesString)
                    || Hours < 0)
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
