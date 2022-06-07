using Model;
using Repository;
using Service;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.Repository;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace ZdravoKorporacija.View.PatientUI
{
    /// <summary>
    /// Interaction logic for PatientTherapyPage.xaml
    /// </summary>
    public partial class PatientTherapyPage : Page
    {
        public ObservableCollection<Prescription> prescriptions { get; set; }

        public static PrescriptionService prescriptionService { get; set; }
        public PatientTherapyPage()
        {
            InitializeComponent();
            DataContext = this;
         PrescriptionRepository PrescriptionRepository = new PrescriptionRepository();
         MedicalRecordRepository MedicalRecordRepository = new MedicalRecordRepository();
         PatientRepository PatientRepository = new PatientRepository();
        MedicationRepository MedicationRepository = new MedicationRepository();
        prescriptionService = new PrescriptionService(PrescriptionRepository, MedicalRecordRepository,PatientRepository, MedicationRepository);
            prescriptions = new ObservableCollection<Prescription>(prescriptionService.GetAllByPatient(App.loggedUser.Jmbg));
            //prescriptions = new ObservableCollection<Prescription>();
            Prescription p1 = new Prescription(1, "Paracetamol", "200mg", 3, System.DateTime.Now.Date, System.DateTime.Now.AddDays(3).Date);
            Prescription p2 = new Prescription(2, "Omeprol", "500mg", 5, System.DateTime.Now.Date, System.DateTime.Now.AddDays(3).Date);
            Prescription p3 = new Prescription(3, "Rapidol", "100mg", 3, System.DateTime.Now.Date, System.DateTime.Now.AddDays(3).Date);

            //prescriptions.Add(p1);
            //prescriptions.Add(p2);
            //prescriptions.Add(p3);
        }

        private void Button_ClickPDF(object sender, RoutedEventArgs e)
        {

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("../../../Resources/PDFs/PatientTherapyPDF.pdf", FileMode.Create));
            doc.Open();
            string header = "VAŠI RECEPTI \n";
            string text = "";
            iTextSharp.text.Paragraph p2 = new iTextSharp.text.Paragraph("");
            iTextSharp.text.Paragraph paragraph = new iTextSharp.text.Paragraph(header);
            doc.Add(paragraph);
            foreach (var p in prescriptions)
            {
                text += p.ToString() + " \n ";
                p2 = new iTextSharp.text.Paragraph(text);


            }
            doc.Add(p2);
            doc.Close();









            MessageBox.Show("Uspješno izgenerisan PDF!\n putanja:  Resources folder","USPJEŠNO!",MessageBoxButton.OK,MessageBoxImage.None);
        }

        private void GoBackButton(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
