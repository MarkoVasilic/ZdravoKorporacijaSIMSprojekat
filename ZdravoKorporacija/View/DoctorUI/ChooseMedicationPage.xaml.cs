using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using Controller;
using Service;
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using Medication = Model.Medication;
using ZdravoKorporacija.View.DoctorUI.ViewModel;

namespace ZdravoKorporacija.View.DoctorUI
{
    /// <summary>
    /// Interaction logic for ChooseMedicationPage.xaml
    /// </summary>
    public partial class ChooseMedicationPage : Page, INotifyPropertyChanged
    {
        private MedicationController medicationController;
        public ObservableCollection<Medication> medications { get; set; }
        public ObservableCollection<Medication> Medications
        {
            get { return medications; }
            set
            {
                medications = value;
                OnPropertyChanged("Medications");
            }
        }
        public String patientJmbg;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }

        }

        public ChooseMedicationPage(String jmbg)
        {
            InitializeComponent();
            this.DataContext = this;
            DoctorWindowVM.setWindowTitle("Choose medication to create prescription");
            this.patientJmbg = jmbg;
            MedicationRepository medicationRepository = new MedicationRepository();
            MedicationService medicationService = new MedicationService(medicationRepository);
            medicationController = new MedicationController(medicationService);
            this.medications = new ObservableCollection<Medication>(medicationController.GetAllVerified());

        }

        private void ChooseMedication(object sender, RoutedEventArgs e)
        {
            String Medication = (String)((Button)sender).CommandParameter;
            NavigationService.Navigate(new AddPrescriptionPage(patientJmbg, Medication));
        }

    }
}
