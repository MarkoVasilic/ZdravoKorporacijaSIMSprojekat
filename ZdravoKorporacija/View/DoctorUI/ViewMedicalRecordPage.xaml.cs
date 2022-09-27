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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using ZdravoKorporacija.View.PatientUI.DTO;
using System.Collections.Generic;

namespace ZdravoKorporacija.View.DoctorUI
{
    /// <summary>
    /// Interaction logic for ViewMedicalRecordPage.xaml
    /// </summary>
    public partial class ViewMedicalRecordPage : Page
    {
        private String PatientJmbg { get; set; }
        public ObservableCollection<AnamnesisDTO> AnamensisDTOs { get; set; }
        public DoctorRepository doctorRepository = new DoctorRepository();

        public MedicalRecordController MedicalRecordController { get; set; }

        public MedicalRecordDTO MedicalRecordDTO { get; set; }
        public ObservableCollection<Anamnesis> Anamensis { get; set; }

        public ViewMedicalRecordPage(String Jmbg)
        {
            DoctorWindowVM.setWindowTitle("Medical Record");
            this.PatientJmbg = Jmbg;
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
            this.AnamensisDTOs = convertAnamnesisToDTO(this.MedicalRecordDTO.Anamnesis);
            this.DataContext = this;
            InitializeComponent();

        }
        private ObservableCollection<AnamnesisDTO> convertAnamnesisToDTO(List<Anamnesis> anamneses)
        {
            AnamensisDTOs = new ObservableCollection<AnamnesisDTO>();
            foreach (Anamnesis anamnesis in anamneses)
            {
                Doctor doctor = doctorRepository.FindOneByJmbg(anamnesis.DoctorJmbg);
                AnamensisDTOs.Add(new AnamnesisDTO(anamnesis.Id, anamnesis.Diagnosis, anamnesis.Report, anamnesis.DateTime, anamnesis.DoctorJmbg, doctor.FirstName + " " + doctor.LastName));
            }

            return AnamensisDTOs;
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
            AnamnesisDTO Anamnesis = (AnamnesisDTO)((Button)sender).CommandParameter;
            if (Anamnesis == null)
            {
                notifier.ShowError("Please select anamnesis to modify!");
            }
            else
            {
                int id = Anamnesis.Id;
                NavigationService.Navigate(new ModifyAnamnesisPage(id, PatientJmbg));
            }


        }

        private void PrintReportButton_Click(object sender, RoutedEventArgs e)
        {

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("../../../Resources/PDFs/Anamnesisprescriptions.pdf", FileMode.Create));
            doc.Open();
            string header1 =
                "---------------------------------------------------------------------------------------------------------------------------------------------------";
            string header = "                                                                               IZVESTAJ ";
            string text = "";
            iTextSharp.text.Paragraph p2 = new iTextSharp.text.Paragraph("");
            iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph(header);
            iTextSharp.text.Paragraph paragraph1 = new iTextSharp.text.Paragraph(header1);

            doc.Add(paragraph1);
            doc.Add(paragraph);
            doc.Add(paragraph1);
            text += this.MedicalRecordDTO.toPDF();
            p2 = new iTextSharp.text.Paragraph(text);

            doc.Add(p2);
            doc.Close();

            notifier.ShowInformation("Izveštaj o anamnezama i receptima je kreiran. Izveštaj se nalazi u ../../ZdravoKorporacija/Resources/PDFs");
            //MessageBox.Show("Izveštaj o anamnezama i receptima je kreiran. Izveštaj se nalazi u ../../ZdravoKorporacija/Resources/PDFs", "Obaveštenje");

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
