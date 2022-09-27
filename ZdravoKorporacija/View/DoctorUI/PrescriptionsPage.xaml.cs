using ZdravoKorporacija.Controller;
using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ZdravoKorporacija.View.DoctorUI.ViewModel;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.View.DoctorUI
{
    /// <summary>
    /// Interaction logic for PrescriptionsPage.xaml
    /// </summary>
    public partial class PrescriptionsPage : Page
    {
        private String jmbg;
        public MedicalRecordController MedicalRecordController { get; set; }
        public ObservableCollection<Prescription> Prescriptions { get; set; }
        public PrescriptionsPage(String Jmbg)
        {
            InitializeComponent();
            this.DataContext = this;
            this.jmbg = Jmbg;
            DoctorWindowVM.setWindowTitle("Prescriptions");
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
            List<Prescription> prescriptions = MedicalRecordController.GetOneMedicalRecorByPatientJmbg(Jmbg).Prescriptions;
            this.Prescriptions = new ObservableCollection<Prescription>(prescriptions);

        }

        private void AddPrescriptionButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ChooseMedicationPage(jmbg));
        }

    }
}
