using Repository;
using Service;

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Navigation;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.View.PatientUI.Commands;
using System.Linq;
using System;

namespace ZdravoKorporacija.View.PatientUI.ViewModels
{
    public class NotificationsPageVM
    {
        #region Properties
        public PrescriptionService PrescriptionService { private set; get; }

        public NotificationService NotificationService { private set; get; }

        public ObservableCollection<Notification> Notifications { get; set; }

        public Notification Notification { get; set; }

        #endregion

        #region Constructors
        public NotificationsPageVM()
        {
            SetProperties();
            SetCommands();
        }
        #endregion

        #region Commands
        public RelayCommand BackCommand { get; set; }
        public RelayCommand CreateCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand DeleteAllCommand { get; set; }
        #endregion


        #region CommandActions
        private void BackExecute(object parameter)
        {
            PatientWindowVM.NavigationService.Navigate(new PatientHomePage());
        }

        private void CreateExecute(object parameter)
        {
            PatientWindowVM.NavigationService.Navigate(new CreateNotificationPage());
        }

        private void DeleteExecute(object parameter)
        {
            NotificationService.Delete((int)parameter);
            Notifications.Remove(Notifications.Where(notification => notification.Id == (int)parameter).Single());
            MessageBox.Show("Notifikacija uspješno obrisana! \n ID: " + parameter, "USPJEŠNO!", MessageBoxButton.OK, MessageBoxImage.None);
        }

        #endregion

        #region Methods
        public void SetProperties()
        {
            PrescriptionService = new PrescriptionService(new PrescriptionRepository(), new MedicalRecordRepository(), new PatientRepository(), new MedicationRepository());
            NotificationService = new NotificationService(new NotificationRepository(), PrescriptionService);
            Notifications = new ObservableCollection<Notification>(NotificationService.ShowPatientNotification());
        }

        public void SetCommands()
        {
            BackCommand = new RelayCommand(BackExecute);
            CreateCommand = new RelayCommand(CreateExecute);
            DeleteCommand = new RelayCommand(DeleteExecute);
            DeleteAllCommand = new RelayCommand(DeleteAllExecute);
        }

        private void DeleteAllExecute(object obj)
        {

           var result = MessageBox.Show("Da li ste sigurni da želite obrisati sve?","BRISANJE!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                App.notificationController.DeleteAll(App.loggedUser.Jmbg);
                Notifications.Clear();
               // PatientWindowVM.NavigationService.Refresh();
            }
        }
        #endregion
    }
}
