using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Controller;
using Repository;
using Service;
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

        private String response;

        public String Response
        {
            get { return response; }
            set
            {
                response = value;
                OnProperyChanged("Response");
            }
        }

        public ICommand ConfirmCommand { get; }

        public RejectMedicationVM(int id)
        {
            ConfirmCommand = new RelayCommand(confirmExecute);
            DoctorWindowVM.setWindowTitle("Insert reason of medication rejection");
            this.Id = id;
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
            medicationController.Reject(Id);
            String name = medicationController.GetOneById(Id).Name;
            notificationController.CreateNotification("Rejection of " + name + " medication", Response, DateTime.Now, "3434343434343", false);
            DoctorWindowVM.NavigationService.Navigate(new VerificationsPage());

        }
    }

}
