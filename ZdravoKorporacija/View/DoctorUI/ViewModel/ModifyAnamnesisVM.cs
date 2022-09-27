using Controller;
using Repository;
using Service;
using System;
using System.Windows.Input;
using ZdravoKorporacija.View.DoctorUI.Commands;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.Controller;
using ToastNotifications;
using ToastNotifications.Position;
using System.Windows;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;

namespace ZdravoKorporacija.View.DoctorUI.ViewModel
{
    public class ModifyAnamnesisVM : ViewModelBase
    {
        private String PatientJmbg { get; set; }
        public MedicalRecordController MedicalRecordController { get; set; }
        private String diagnosis;
        private int Id;
        public String Diagnosis
        {
            get { return diagnosis; }
            set
            {
                diagnosis = value;
                OnPropertyChanged("Diagnosis");
            }
        }

        private String report;
        public String Report
        {
            get { return report; }
            set
            {
                report = value;
                OnPropertyChanged("Report");
            }
        }

        public ICommand ModifyCommand { get; }

        public ModifyAnamnesisVM(int id, String patientJmbg)
        {
            ModifyCommand = new RelayCommand(confirmExecute);
            DoctorWindowVM.setWindowTitle("Modify report");
            this.PatientJmbg = patientJmbg;
            this.Id = id;
            MedicalRecordRepository medicalRecordRepository = new MedicalRecordRepository();
            AnamnesisRepository anamnesisRepository = new AnamnesisRepository();
            PrescriptionRepository prescriptionRepository = new PrescriptionRepository();
            PatientRepository patientRepository = new PatientRepository();
            MedicationRepository medicationRepository = new MedicationRepository();
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            AnamnesisService anamnesisService = new AnamnesisService(anamnesisRepository, medicalRecordRepository);
            PrescriptionService prescriptionService = new PrescriptionService(prescriptionRepository, medicalRecordRepository, patientRepository, medicationRepository);
            MedicalRecordService medicalRecordService = new MedicalRecordService(medicalRecordRepository, anamnesisRepository, prescriptionRepository, patientRepository, appointmentRepository);
            MedicalRecordController = new MedicalRecordController(medicalRecordService, anamnesisService, prescriptionService);
            this.Diagnosis = MedicalRecordController.GetOneAnamnesisById(Id).Diagnosis;
            this.Report = MedicalRecordController.GetOneAnamnesisById(Id).Report;
        }

        private void confirmExecute(object parametar)
        {
            try
            {
                MedicalRecordController.ModifyAnamnesis(Id, Diagnosis, Report);
                notifier.ShowSuccess("Successfully modified anamnesis!");
                DoctorWindowVM.NavigationService.Navigate(new ViewMedicalRecordPage(PatientJmbg));
            }
            catch
            {

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
