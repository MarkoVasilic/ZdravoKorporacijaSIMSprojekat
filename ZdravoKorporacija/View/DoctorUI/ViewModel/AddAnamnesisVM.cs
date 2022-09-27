using ZdravoKorporacija.Controller;
using Repository;
using Service;
using System;
using System.Windows.Input;
using ZdravoKorporacija.View.DoctorUI.Commands;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ToastNotifications;
using ToastNotifications.Position;
using ToastNotifications.Lifetime;
using System.Windows;
using ToastNotifications.Messages;

namespace ZdravoKorporacija.View.DoctorUI.ViewModel
{
    public class AddAnamnesisVM : ViewModelBase
    {
        public MedicalRecordController MedicalRecordController { get; set; }
        private String diagnosis;
        private String patientJmbg;
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

        public ICommand ConfirmCmmand { get; }

        public AddAnamnesisVM(String Jmbg)
        {
            ConfirmCmmand = new RelayCommand(confirmExecute);
            DoctorWindowVM.setWindowTitle("Add new report");
            this.patientJmbg = Jmbg;
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
        }

        private void confirmExecute(object parametar)
        {
            try
            {
                if (Diagnosis == null || String.IsNullOrWhiteSpace(Diagnosis))
                {

                }else if ( Report == null || String.IsNullOrWhiteSpace(Report))
                {

                }
                else
                {
                    MedicalRecordController.CreateAnamnesis(patientJmbg, Diagnosis, Report);
                    notifier.ShowSuccess("Successfully created anamnesis!");
                    DoctorWindowVM.NavigationService.Navigate(new ViewMedicalRecordPage(patientJmbg));
                }
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
