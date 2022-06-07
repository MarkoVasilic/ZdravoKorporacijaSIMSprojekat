using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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

namespace ZdravoKorporacija.View.DoctorUI.ViewModel
{
    public class AddPrescriptionVM : ViewModelBase
    {
        public MedicalRecordController medicalRecordController { get; set; }
        public NotificationController notificationController { get; set; }
        private String patientJmbg;
        private String medication;

        private String errorMessage;
        public String ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value;
                OnProperyChanged("ErrorMessage");
            }
        }

        private String amount;
        public String Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                OnProperyChanged("Amount");
            }
        }

        private int frequency;
        public int Frequency
        {
            get { return frequency; }
            set
            {
                frequency = value;
                OnProperyChanged("Frequency");
            }
        }

        private DateTime from;
        public DateTime From
        {
            get { return from; }
            set
            {
                from = value;
                OnProperyChanged("From");
            }
        }

        private DateTime to;
        public DateTime To
        {
            get { return to; }
            set
            {
                to = value;
                OnProperyChanged("To");
            }
        }

        private ObservableCollection<Medication> medications;
        public ObservableCollection<Medication> Medications
        {
            get => medications;
            set
            {
                medications = value;
                OnProperyChanged("Medications");
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
            DoctorRepository doctorRepository = new DoctorRepository();
            PatientRepository patientRepository = new PatientRepository();
            MedicationRepository medicationRepository = new MedicationRepository();
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            AnamnesisService anamnesisService = new AnamnesisService(anamnesisRepository, medicalRecordRepository, doctorRepository);
            PrescriptionService prescriptionService = new PrescriptionService(prescriptionRepository, medicalRecordRepository, patientRepository, medicationRepository);
            MedicalRecordService medicalRecordService = new MedicalRecordService(medicalRecordRepository, anamnesisRepository, prescriptionRepository, patientRepository, appointmentRepository);
            medicalRecordController = new MedicalRecordController(medicalRecordService, anamnesisService, prescriptionService);
            NotificationRepository notificationRepository = new NotificationRepository();
            NotificationService notificationService = new NotificationService(notificationRepository, prescriptionService);
            notificationController = new NotificationController(notificationService);

        }

        private void confirmExecute(object parametar)
        {
            try
            {
                medicalRecordController.CreatePrescription(patientJmbg, medication, Amount, Frequency, From, To);
                notificationController.CreatePatientNotification(patientJmbg);
                DoctorWindowVM.NavigationService.Navigate(new PrescriptionsPage(patientJmbg));
            }
            catch(Exception e)
            {
                ErrorMessage = e.Message;
            }
        }


    }
}
