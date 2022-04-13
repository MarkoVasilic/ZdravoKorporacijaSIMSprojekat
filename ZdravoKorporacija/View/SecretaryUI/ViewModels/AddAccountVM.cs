using Controller;
using Model;
using Repository;
using Service;
using System.ComponentModel;
using System.Windows.Input;
using ZdravoKorporacija.View.SecretaryUI.Commands;

namespace ZdravoKorporacija.View.SecretaryUI.ViewModels
{
    internal class AddAccountVM : INotifyPropertyChanged
    {
        private Patient patient;
        private Gender patientGender = Gender.NONE;
        private string errorMessage;

        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }

        public Patient Patient
        {
            get => patient;
            set
            {
                patient = value;
                OnPropertyChanged("Patient");
            }
        }

        public void setPatientGenderMale(BloodType bloodType)
        {
            patientGender = Gender.MALE;
            Patient.BloodTypeEnum = bloodType;
        }

        public void setPatientGenderFemale(BloodType bloodType)
        {
            patientGender = Gender.FEMALE;
            Patient.BloodTypeEnum = bloodType;
        }

        public void setBloodType(BloodType bloodType)
        {
            Patient.BloodTypeEnum = bloodType;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public PatientController PatientController { get; set; }

        public AddAccountVM()
        {
            Patient = new Patient();
            Patient.IsGuest = false;
            PatientRepository patientRepository = new PatientRepository();
            PatientService patientService = new PatientService(patientRepository);
            PatientController = new PatientController(patientService);
            SaveCommand = new RelayCommand(saveExecute);
        }


        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public ICommand SaveCommand { get; set; }

        private void saveExecute(object parameter)
        {
            Patient.IsGuest = false;
            Patient.Gender = patientGender;
            ErrorMessage = PatientController.CreatePatient(Patient.IsGuest, Patient.Allergens, Patient.BloodTypeEnum, Patient.FirstName, Patient.LastName,
                Patient.Username, Patient.Password, Patient.Jmbg, Patient.DateOfBirth, Patient.Gender, Patient.Email, Patient.PhoneNumber,
                Patient.Address);
            if (ErrorMessage.Length == 0)
                SecretaryWindowVM.NavigationService.Navigate(new PatientsView());
        }
    }
}
