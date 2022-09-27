using Controller;
using Model;
using Repository;
using Service;
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
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.View.PatientUI.DTO;

namespace ZdravoKorporacija.View.PatientUI
{
    /// <summary>
    /// Interaction logic for PatientMedicalRecordPage.xaml
    /// </summary>
    public partial class PatientMedicalRecordPage : Page
    {
        public MedicalRecordController MedicalRecordController { get; set; }

        public MedicalRecordDTO MedicalRecordDTO { get; set; }
        public ObservableCollection<AnamnesisDTO> AnamensisDTOs { get; set; }

        public static PrescriptionService prescriptionService { get; set; }

        public ObservableCollection<Prescription> prescriptions { get; set; }

        private DoctorRepository doctorRepository { get; set; }

        public PatientMedicalRecordPage()
        {
            InitializeComponent();
            MedicalRecordRepository medicalRecordRepository = new MedicalRecordRepository();
            AnamnesisRepository anamnesisRepository = new AnamnesisRepository();
            PrescriptionRepository prescriptionRepository = new PrescriptionRepository();
            doctorRepository = new DoctorRepository();
            PatientRepository patientRepository = new PatientRepository();
            
            MedicationRepository medicationRepository = new MedicationRepository();
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            AnamnesisService anamnesisService = new AnamnesisService(anamnesisRepository, medicalRecordRepository);
            prescriptionService = new PrescriptionService(prescriptionRepository, medicalRecordRepository, patientRepository, medicationRepository);
            MedicalRecordService medicalRecordService = new MedicalRecordService(medicalRecordRepository, anamnesisRepository, prescriptionRepository, patientRepository, appointmentRepository);
            MedicalRecordController = new MedicalRecordController(medicalRecordService, anamnesisService, prescriptionService);
            this.MedicalRecordDTO = MedicalRecordController.GetOneMedicalRecorByPatientJmbg(App.loggedUser.Jmbg);
            convertAnamnesisToDTO(this.MedicalRecordDTO.Anamnesis);
            prescriptions = new ObservableCollection<Prescription>(prescriptionService.GetAllByPatient(App.loggedUser.Jmbg));
  
            this.DataContext = this;

        }

        private void convertAnamnesisToDTO(List<Anamnesis> anamneses)
        {
            AnamensisDTOs = new ObservableCollection<AnamnesisDTO>();
            foreach (Anamnesis anamnesis in anamneses)
            {
                Doctor doctor = doctorRepository.FindOneByJmbg(anamnesis.DoctorJmbg);
                AnamensisDTOs.Add(new AnamnesisDTO(anamnesis.Id, anamnesis.Diagnosis, anamnesis.Report, anamnesis.DateTime, anamnesis.DoctorJmbg, doctor.FirstName + " " + doctor.LastName));
            }
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
