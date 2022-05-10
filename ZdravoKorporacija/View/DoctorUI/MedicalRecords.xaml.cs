﻿using Controller;
using Service;
using Repository;
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
using ZdravoKorporacija.DTO;
using System.ComponentModel;

namespace ZdravoKorporacija.View.DoctorUI
{
    /// <summary>
    /// Interaction logic for MedicalRecords.xaml
    /// </summary>
    public partial class MedicalRecords : Page
    {
        private MedicalRecordController medicalRecordController;

        public ObservableCollection<MedicalRecordDTO> medicalrecords { get; set; }
        public MedicalRecords()
        {
            InitializeComponent();
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
        }
    }
}