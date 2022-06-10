using ZdravoKorporacija.Controller;
using Model;
using Repository;
using Service;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.View.DoctorUI.ViewModel;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.View.DoctorUI
{
    /// <summary>
    /// Interaction logic for ViewMedicalRecordPage.xaml
    /// </summary>
    public partial class ViewMedicalRecordPage : Page
    {
        public MedicalRecordController MedicalRecordController { get; set; }

        public MedicalRecordDTO MedicalRecordDTO { get; set; }
        public ObservableCollection<Anamnesis> Anamensis { get; set; }

        public ViewMedicalRecordPage(String Jmbg)
        {
            DoctorWindowVM.setWindowTitle("Medical Record");
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
            this.MedicalRecordDTO = MedicalRecordController.GetOneMedicalRecorByPatientJmbg(Jmbg);
            this.Anamensis = new ObservableCollection<Anamnesis>(this.MedicalRecordDTO.Anamnesis);
            this.DataContext = this;
            InitializeComponent();

        }

        private void ViewPrescriptions(object sender, RoutedEventArgs e)
        {
            String Jmbg = (String)((Button)sender).CommandParameter;
            NavigationService.Navigate(new PrescriptionsPage(Jmbg));
        }

        private void AddAnamnesisButton_Click(object sender, RoutedEventArgs e)
        {
            String Jmbg = (String)((Button)sender).CommandParameter;
            NavigationService.Navigate(new AddAnamnesisPage(Jmbg));

        }

        private void ModifyAnamnesis(object sender, RoutedEventArgs e)
        {
            Anamnesis Anamnesis = (Anamnesis)((Button)sender).CommandParameter;
            int id = Anamnesis.Id;
            NavigationService.Navigate(new ModifyAnamnesisPage(id));
        }
    }
}
