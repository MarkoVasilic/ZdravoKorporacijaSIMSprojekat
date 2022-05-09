using Controller;
using Model;
using Repository;
using Service;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ZdravoKorporacija.Repository;

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
        private string errorMessage;

        private Patient selectedPatient;

        public static ObservableCollection<Patient> Patients { get; set; }
        public PatientController PatientController;

        public event PropertyChangedEventHandler PropertyChanged;
        public EditBasicInfo(Patient selectedPatient)
        {
            InitializeComponent();
            SelectedPatient = selectedPatient;
            initializeBindingFields();
            DoctorRepository doctorRepository = new DoctorRepository();
            DoctorService doctorService = new DoctorService(doctorRepository);
            DoctorController doctorController = new DoctorController(doctorService);
            RoomRepository roomRepository = new RoomRepository();
            RoomService roomService = new RoomService(roomRepository);
            RoomController roomController = new RoomController(roomService);
            BasicRenovationRepository basicRenovationRepository = new BasicRenovationRepository();
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            PatientRepository patientRepository = new PatientRepository();
            AppointmentService appointmentService = new AppointmentService(appointmentRepository, patientRepository, doctorRepository,
                roomRepository, basicRenovationRepository);
            PatientService patientService = new PatientService(patientRepository);
            PatientController = new PatientController(patientService,appointmentService);
            this.DataContext = this;
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
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
            if ((int)SelectedPatient.Gender == 1)
            {
                Male.IsChecked = true;
                Female.IsChecked = false;
            }
            else if ((int)SelectedPatient.Gender == 2)
            {
                Male.IsChecked = false;
                Female.IsChecked = true;
            }
            else
            {
                Male.IsChecked = true;
                Female.IsChecked = false;
            }
            BloodTypeComboBox.SelectedIndex = (int)SelectedPatient.BloodTypeEnum;
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
            try
            {
                PatientController.ModifyPatient(false, selectedPatient.Allergens, (BloodType)BloodTypeComboBox.SelectedIndex, firstName, lastName,
                selectedPatient.Jmbg, dateOfBirth, newGender, email, phoneNumber, address);
                NavigationService.Navigate(new PatientsView());
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
