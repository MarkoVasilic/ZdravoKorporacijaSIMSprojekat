﻿using Controller;
using Repository;
using Service;
using System;
using System.Windows.Input;
using ZdravoKorporacija.View.DoctorUI.Commands;

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
                OnProperyChanged("Diagnosis");
            }
        }

        private String report;
        public String Report
        {
            get { return report; }
            set
            {
                report = value;
                OnProperyChanged("Report");
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
            DoctorRepository doctorRepository = new DoctorRepository();
            PatientRepository patientRepository = new PatientRepository();
            MedicationRepository medicationRepository = new MedicationRepository();
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            AnamnesisService anamnesisService = new AnamnesisService(anamnesisRepository, medicalRecordRepository, doctorRepository);
            PrescriptionService prescriptionService = new PrescriptionService(prescriptionRepository, medicalRecordRepository, patientRepository, medicationRepository);
            MedicalRecordService medicalRecordService = new MedicalRecordService(medicalRecordRepository, anamnesisRepository, prescriptionRepository, patientRepository, appointmentRepository);
            MedicalRecordController = new MedicalRecordController(medicalRecordService, anamnesisService, prescriptionService);
        }

        private void confirmExecute(object parametar)
        {
            try
            {
                MedicalRecordController.CreateAnamnesis(patientJmbg, Diagnosis, Report);
                DoctorWindowVM.NavigationService.Navigate(new ViewMedicalRecordPage(patientJmbg));
            }
            catch
            {

            }
        }
    }
}
