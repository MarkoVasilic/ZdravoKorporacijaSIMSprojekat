using Controller;
using Model;
using Repository;
using Service;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ZdravoKorporacija.View.SecretaryUI.Commands;

namespace ZdravoKorporacija.View.SecretaryUI.ViewModels
{
    internal class AddAccountVM : INotifyPropertyChanged
    {
        private Patient patient;
        private Gender patientGender = Gender.NONE;
        private string allergen;
        private string errorMessage;
        public ObservableCollection<string> PatientAllergens { get; set; }
        public string SelectedAllergen { get; set; }
        public string Allergen
        {
            get => allergen;
            set
            {
                allergen = value;
                OnPropertyChanged("Allergen");
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
            DoctorRepository doctorRepository = new DoctorRepository();
            RoomRepository roomRepository = new RoomRepository();
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            PatientRepository patientRepository = new PatientRepository();
            AppointmentService appointmentService = new AppointmentService(appointmentRepository, patientRepository, doctorRepository, roomRepository);
            PatientService patientService = new PatientService(patientRepository);
            PatientController = new PatientController(patientService, appointmentService);
            PatientAllergens = new ObservableCollection<string>();
            SaveCommand = new RelayCommand(saveExecute);
            AddAllergenCommand = new RelayCommand(addAllergenExecute);
            RemoveAllergenCommand = new RelayCommand(removeAllergenExecute);
        }




        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public ICommand SaveCommand { get; set; }
        public ICommand AddAllergenCommand { get; set; }
        public ICommand RemoveAllergenCommand { get; set; }

        private void saveExecute(object parameter)
        {
            Patient.IsGuest = false;
            Patient.Gender = patientGender;
            if (ErrorMessage.Length == 0)
            {
                try
                {
                    PatientController.CreatePatient(Patient.IsGuest, Patient.Allergens, Patient.BloodTypeEnum, Patient.FirstName, Patient.LastName,
                        Patient.Username, Patient.Password, Patient.Jmbg, Patient.DateOfBirth, Patient.Gender, Patient.Email, Patient.PhoneNumber,
                        Patient.Address);
                    SecretaryWindowVM.NavigationService.Navigate(new PatientsView());
                }
                catch (Exception e)
                {
                    ErrorMessage = e.Message;
                }
            }
            
        }

        private void addAllergenExecute(object parameter)
        {
            if (Allergen.Length > 0)
            {
                Patient.Allergens.Add(Allergen);
                PatientAllergens.Add(Allergen);
                Allergen = "";
            }
        }

        private void removeAllergenExecute(object parameter)
        {
            Patient.Allergens.Remove(SelectedAllergen);
            PatientAllergens.Remove(SelectedAllergen);
        }

        public void setErrorMessage(String errorMsg)
        {
            ErrorMessage = errorMsg;
        }
    }
}
