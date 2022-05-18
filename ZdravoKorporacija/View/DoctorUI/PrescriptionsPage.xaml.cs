using Controller;
using Service;
using Repository;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZdravoKorporacija.View.DoctorUI.ViewModel;

namespace ZdravoKorporacija.View.DoctorUI
{
    /// <summary>
    /// Interaction logic for PrescriptionsPage.xaml
    /// </summary>
    public partial class PrescriptionsPage : Page
    {
        public MedicalRecordController MedicalRecordController { get; set; }
        public ObservableCollection<Prescription> Prescriptions { get; set; }
        public PrescriptionsPage(String Jmbg)
        {
            InitializeComponent();
            this.DataContext = this;
            DoctorWindowVM.setWindowTitle("Prescriptions");
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
            MedicalRecordController = new MedicalRecordController(medicalRecordService, anamnesisService, prescriptionService);
            List<Prescription> prescriptions = MedicalRecordController.GetOneMedicalRecorByPatientJmbg(Jmbg).Prescriptions;
            this.Prescriptions = new ObservableCollection<Prescription>(prescriptions);
            
        }

        private void AddPrescriptionButton_Click(object sender, RoutedEventArgs e)
        {
            //
        }
    }
}
