using Controller;
using Repository;
using Service;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.View.DoctorUI.ViewModel;

namespace ZdravoKorporacija.View.DoctorUI
{
    /// <summary>
    /// Interaction logic for MedicalRecords.xaml
    /// </summary>
    public partial class MedicalRecords : Page
    {
        private MedicalRecordController medicalRecordController;

        private String patientJmbg { get; set; }
        public ObservableCollection<MedicalRecordDTO> medicalrecords { get; set; }
        public MedicalRecords()
        {
            InitializeComponent();
            DoctorWindowVM.setWindowTitle("Patients");
            MedicalRecordRepository medicalRecordRepository = new MedicalRecordRepository();
            AnamnesisRepository anamnesisRepository = new AnamnesisRepository();
            PrescriptionRepository prescriptionRepository = new PrescriptionRepository();
            PatientRepository patientRepository = new PatientRepository();
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            MedicationRepository medicationRepository = new MedicationRepository();
            MedicalRecordService medicalRecordService = new MedicalRecordService(medicalRecordRepository, anamnesisRepository, prescriptionRepository, patientRepository, appointmentRepository);
            AnamnesisService anamnesisService = new AnamnesisService(anamnesisRepository, medicalRecordRepository);
            PrescriptionService prescriptionService = new PrescriptionService(prescriptionRepository, medicalRecordRepository, patientRepository, medicationRepository);
            medicalRecordController = new MedicalRecordController(medicalRecordService, anamnesisService, prescriptionService);
            this.DataContext = this;
            medicalrecords = new ObservableCollection<MedicalRecordDTO>(medicalRecordController.GetAllMedicalRecords());

        }


        private void ViewMedicalRecord(object sender, RoutedEventArgs e)
        {
            String Jmbg = (String)((Button)sender).CommandParameter;
            this.patientJmbg = Jmbg;
            NavigationService.Navigate(new ViewMedicalRecordPage(Jmbg));
        }

        private void Tutorial_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPrescriptionTutorial());
        }
    }
}
