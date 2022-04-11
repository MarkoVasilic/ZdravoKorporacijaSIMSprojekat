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
    /// <summary>
    /// Interaction logic for EditBasicInfo.xaml
    /// </summary>
    public partial class EditBasicInfo : Page, INotifyPropertyChanged
    {
        private string firstName;
        private string lastName;
        private string phoneNumber;
        private string email;
        private string address;
        private DateTime? dateOfBirth;
        private Gender gender;
        private BloodType bloodType;

        private Patient selectedPatient;

        public static ObservableCollection<Patient> Patients { get; set; }
        public PatientController PatientController;

        public event PropertyChangedEventHandler PropertyChanged;
        public EditBasicInfo(Patient selectedPatient)
        {
            InitializeComponent();
            SelectedPatient = selectedPatient;
            initializeBindingFields();
            PatientRepository patientRepository = new PatientRepository();
            PatientService patientService = new PatientService(patientRepository);
            PatientController = new PatientController(patientService);
            this.DataContext = this;
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

        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }
  
        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }
        public string Address
        {
            get => address;
            set
            {
                address = value;
                OnPropertyChanged("Address");
            }
        }
        public DateTime? DateOfBirth
        {
            get => dateOfBirth;
            set
            {
                dateOfBirth = value;
                OnPropertyChanged("DateOfBirth");
            }
        }

        public Gender Gender
        {
            get => gender;
            set
            {
                gender = (Gender)value;
                OnPropertyChanged("Gender");
            }
        }
        public BloodType BloodType
        {
            get => bloodType;
            set
            {
                bloodType = (BloodType)value;
                OnPropertyChanged("BloodType");
            }
        }

        public void initializeBindingFields()
        {
            FirstName = SelectedPatient.FirstName;
            LastName = SelectedPatient.LastName;
            PhoneNumber = SelectedPatient.PhoneNumber;
            Email = SelectedPatient.Email;
            Address = SelectedPatient.Address;
            if (SelectedPatient.DateOfBirth != null)
                DateOfBirth = SelectedPatient.DateOfBirth;
            if((int)SelectedPatient.Gender == 1)
            {
                Male.IsChecked = true;
                Female.IsChecked = false;
            }
            else if((int)SelectedPatient.Gender == 2)
            {
                Male.IsChecked = false;
                Female.IsChecked = true;
            }
            else
            {
                Male.IsChecked = false;
                Female.IsChecked = false;
            }
            BloodTypeComboBox.SelectedIndex = (int)SelectedPatient.BloodType;
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            Gender newGender;
            if (Male.IsChecked == true)
                newGender = Gender.MALE;
            else if (Female.IsChecked == true)
                newGender = Gender.FEMALE;
            else
                newGender = Gender.NONE;
            Patient newPatient = new Patient(selectedPatient.IsGuest, selectedPatient.Allergens, (BloodType)BloodTypeComboBox.SelectedIndex, firstName, lastName,
                selectedPatient.Username, selectedPatient.Password, selectedPatient.Jmbg, dateOfBirth, newGender, email, phoneNumber, address);
            PatientController.ModifyPatient(newPatient);
            NavigationService.Navigate(new PatientsView());
        }
    }
}
