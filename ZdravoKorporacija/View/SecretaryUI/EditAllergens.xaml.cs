using Controller;
using Model;
using Repository;
using Service;
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

namespace ZdravoKorporacija.View.SecretaryUI
{
    
    public partial class EditAllergens : Page, INotifyPropertyChanged
    {
        private Patient selectedPatient;

        public static ObservableCollection<string> PatientAllergens { get; set; }
        public PatientController PatientController;
        private string allergen;
        private string errorMessage;
        public string SelectedAllergen { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public EditAllergens(Patient selectedPatient)
        {
            InitializeComponent();
            SelectedPatient = selectedPatient;
            allergenListToStringList(selectedPatient.Allergens);
            PatientRepository patientRepository = new PatientRepository();
            PatientService patientService = new PatientService(patientRepository);
            PatientController = new PatientController(patientService);
            this.DataContext = this;
        }

        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }

        public string Allergen
        {
            get => allergen;
            set
            {
                allergen = value;
                OnPropertyChanged("Allergen");
            }
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public Patient SelectedPatient
        {
            get { return selectedPatient; }
            set
            {
                selectedPatient = value;
                OnPropertyChanged("SelectedPatient");
            }
        }

        private void allergenListToStringList(List<string> allergens)
        {
            PatientAllergens = new ObservableCollection<string>();
            foreach (var al in allergens)
            {
                PatientAllergens.Add(al);
            }
        }

        private void Remove_Allergen_Button_Click(object sender, RoutedEventArgs e)
        {
            SelectedPatient.Allergens.Remove(SelectedAllergen);
            PatientAllergens.Remove(SelectedAllergen);
        }

        private void Add_Allergen_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Allergen.Length > 0)
            {
                SelectedPatient.Allergens.Add(Allergen);
                PatientAllergens.Add(Allergen);
                Allergen = "";
            }
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PatientController.ModifyPatient(false, SelectedPatient.Allergens, SelectedPatient.BloodTypeEnum, SelectedPatient.FirstName, SelectedPatient.LastName,
                SelectedPatient.Jmbg, SelectedPatient.DateOfBirth, SelectedPatient.Gender, SelectedPatient.Email, SelectedPatient.PhoneNumber, SelectedPatient.Address);
                NavigationService.Navigate(new PatientsView());
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
