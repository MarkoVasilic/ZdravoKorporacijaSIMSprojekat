using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Model;
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.View.DoctorUI.Commands;
using ZdravoKorporacija.Repository;
using Repository;
using ZdravoKorporacija.Service;
using ToastNotifications;
using ToastNotifications.Position;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;

namespace ZdravoKorporacija.View.DoctorUI.ViewModel
{
    public class AddPrescriptionVM : ViewModelBase
    {
        public MedicalRecordController medicalRecordController { get; set; }
        //public NotificationController notificationController { get; set; }
        private String patientJmbg;
        private String medication;

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

        private String amount;
        public String Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                OnPropertyChanged("Amount");
            }
        }

        private int frequency;
        public int Frequency
        {
            get { return frequency; }
            set
            {
                frequency = value;
                OnPropertyChanged("Frequency");
            }
        }

        private DateTime from;
        public DateTime From
        {
            get { return from; }
            set
            {
                from = value;
                OnPropertyChanged("From");
            }
        }

        private DateTime to;
        public DateTime To
        {
            get { return to; }
            set
            {
                to = value;
                OnPropertyChanged("To");
            }
        }

        private ObservableCollection<Medication> medications;
        public ObservableCollection<Medication> Medications
        {
            get => medications;
            set
            {
                medications = value;
                OnPropertyChanged("Medications");
            }
        }

        public ICommand ConfirmCommand { get; }

        public AddPrescriptionVM(String jmbg, String medication)
        {
            ConfirmCommand = new RelayCommand(confirmExecute);
            DoctorWindowVM.setWindowTitle("Add new prescription");
            this.patientJmbg = jmbg;
            this.ErrorMessage = "";
            this.medication = medication;

            MedicalRecordRepository medicalRecordRepository = new MedicalRecordRepository();
            AnamnesisRepository anamnesisRepository = new AnamnesisRepository();
            PrescriptionRepository prescriptionRepository = new PrescriptionRepository();
            PatientRepository patientRepository = new PatientRepository();
            MedicationRepository medicationRepository = new MedicationRepository();
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            AnamnesisService anamnesisService = new AnamnesisService(anamnesisRepository, medicalRecordRepository);
            PrescriptionService prescriptionService = new PrescriptionService(prescriptionRepository, medicalRecordRepository, patientRepository, medicationRepository);
            MedicalRecordService medicalRecordService = new MedicalRecordService(medicalRecordRepository, anamnesisRepository, prescriptionRepository, patientRepository, appointmentRepository);
            medicalRecordController = new MedicalRecordController(medicalRecordService, anamnesisService, prescriptionService);
            //NotificationRepository notificationRepository = new NotificationRepository();
            //NotificationService notificationService = new NotificationService(notificationRepository, prescriptionService);
           // notificationController = new NotificationController(notificationService);

        }

        private void confirmExecute(object parametar)
        {
            try
            {
                if (Amount == null || String.IsNullOrWhiteSpace(Amount))
                {
                    ErrorMessage = "Please insert amount of medication!";
                }else if (Frequency == null || Frequency < 1)
                {
                    ErrorMessage = "Please insert frequency of medication treatment!";
                }else if (To == null || To < DateTime.Now || To < From)
                {
                    ErrorMessage = "Date is not valid! Please choose another date!";
                }else if (From == null || From < DateTime.Now)
                {
                    ErrorMessage = "Date is not valid! Please choose another date!";
                }
                else
                {
                    medicalRecordController.CreatePrescription(patientJmbg, medication, Amount, Frequency, From, To);
                    // notificationController.CreatePatientNotification(patientJmbg);
                    notifier.ShowSuccess("Successfully created prescription!");
                    DoctorWindowVM.NavigationService.Navigate(new PrescriptionsPage(patientJmbg));
                }
            }
            catch(Exception e)
            {
                ErrorMessage = e.Message;
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
