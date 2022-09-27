using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Controller;
using Repository;
using Service;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.View.DoctorUI.Commands;

namespace ZdravoKorporacija.View.DoctorUI.ViewModel
{
    public class RejectMedicationVM : ViewModelBase
    {
        public MedicationController medicationController { get; set; }
        public NotificationController notificationController { get; set; }
        private int Id { get; set; }

        private String errorMessage;
        public String ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }

        private String response;

        public String Response
        {
            get { return response; }
            set
            {
                response = value;
                OnPropertyChanged("Response");
            }
        }

        public ICommand ConfirmCommand { get; }

        public RejectMedicationVM(int id)
        {
            ConfirmCommand = new RelayCommand(confirmExecute);
            DoctorWindowVM.setWindowTitle("Insert reason of medication rejection");
            this.Id = id;
            ErrorMessage = "";
            MedicationRepository medicationRepository = new MedicationRepository();
            MedicationService medicationService = new MedicationService(medicationRepository);
            medicationController = new MedicationController(medicationService);
            PrescriptionService prescriptionService = new PrescriptionService();
            NotificationRepository notificationRepository = new NotificationRepository();
            NotificationService notificationService =
                new NotificationService(notificationRepository, prescriptionService);
            notificationController = new NotificationController(notificationService);
        }

        private void confirmExecute(object parametar)
        {
            if (String.IsNullOrWhiteSpace(Response))
            {
                ErrorMessage = "Please insert reason of medication rejection!";
            }
            else
            {
                medicationController.Reject(Id);
                String name = medicationController.GetOneById(Id).Name;
                notificationController.Create("Rejection of " + name + " medication", Response, DateTime.Now,
                    "3434343434343", false);
                notifier.ShowSuccess("Medication rejected successfully!");
                DoctorWindowVM.NavigationService.Navigate(new VerificationsPage());
            }

        }
        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 30,
                offsetY: 90);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(7),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });
    }

}
