using Controller;
using Repository;
using Service;
using System;
using System.Windows.Input;
using ZdravoKorporacija.View.DoctorUI.Commands;

namespace ZdravoKorporacija.View.DoctorUI.ViewModel
{
    public class ModifyAnamnesisVM : ViewModelBase
    {
        public MedicalRecordController MedicalRecordController { get; set; }
        private String diagnosis;
        private int Id;
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

        public ICommand ModifyCommand { get; }

        public ModifyAnamnesisVM(int id)
        {
            ModifyCommand = new RelayCommand(confirmExecute);
            DoctorWindowVM.setWindowTitle("Modify report");
            this.Id = id;
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
            this.Diagnosis = MedicalRecordController.GetOneAnamnesisById(Id).Diagnosis;
            this.Report = MedicalRecordController.GetOneAnamnesisById(Id).Report;
        }

        private void confirmExecute(object parametar)
        {
            try
            {
                MedicalRecordController.ModifyAnamnesis(Id, Diagnosis, Report);
                DoctorWindowVM.NavigationService.Navigate(new MedicalRecords());
            }
            catch
            {

            }
        }
    }
}
