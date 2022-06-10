using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using ZdravoKorporacija.View.PatientUI.Commands;

namespace ZdravoKorporacija.View.PatientUI.ViewModels
{
    public class PatientInfoPageVM : INotifyPropertyChanged
    {
        public Patient loggedPatient;

        public string firstName;
        public string lastName;
        public string username;
        public string password;
        public string email;
        public string address;
        public string phoneNumber;
        public DateTime? dateOfBirth;

        public String errorMessage;

        public bool PatientChanged { get; set; }

        public bool FirstNameChanged { get; set; }

        public bool LastNameChanged { get; set; }
        public bool UsernameChanged { get; set; }
        public bool PasswordChanged { get; set; }
        public bool EmailChanged { get; set; }
        public bool AddressChanged { get; set; }
        public bool PhoneNumberChanged { get; set; }
        public bool DateOfBirthChanged { get; set; }

        private static Regex usernameRegex { get; set; }
        private static Regex onlyNumberRegex { get; set; }
        private static Regex emailRegex { get; set; }

        private static Regex onlyText { get; set; }

        private static Regex numberText { get; set; }

        private static Regex specialCharacters { get; set; }
        private static Regex firstNameReg { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }



        public PatientInfoPageVM()
        {
            SetProperties();
            SetCommands();
        }

        public RelayCommand UpdateCommand { get; private set; }
        public RelayCommand BackCommand { get; set; }

        private void SetCommands()
        {
            BackCommand = new RelayCommand(BackExecute);
            UpdateCommand = new RelayCommand(UpdateExecute, UpdateCanExecute);
        }

        private void SetProperties()
        {
          loggedPatient =  App.patientController.GetOnePatient(App.loggedUser.Jmbg);
            PatientChanged = false;
            FirstNameChanged = false;
            LastNameChanged = false;
            UsernameChanged = false;
            PasswordChanged = false;
            EmailChanged = false;
            AddressChanged = false;
            PhoneNumberChanged = false;
            DateOfBirthChanged = false;
            ErrorMessage = "";

            usernameRegex = new Regex("^$|[a-zA-Z]+[a-zA-Z0-9_\\.\\s]*$");
            onlyNumberRegex = new Regex("^[0-9]+$");
            emailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            onlyText = new Regex("[A-Z]{1}[a-z]+");
            numberText = new Regex("[A-Z][a-z]*[0-9]+");
            specialCharacters = new Regex("^([a-zA-Z0-9/ ])+$");
            firstNameReg = new Regex("^([A-Z]{1}[a-z]+)$");

            firstName = loggedPatient.FirstName;
            lastName = loggedPatient.LastName;
            username = loggedPatient.Username;
            password = loggedPatient.Password;
            email = loggedPatient.Email;
            address = loggedPatient.Address;
            phoneNumber = loggedPatient.PhoneNumber;
            dateOfBirth = loggedPatient.DateOfBirth;
            String newPW = "";
            int pwLenght = password.Length;

            for (int i = 0; i < pwLenght; i++)
            {
                newPW += "*";
            }
            password = newPW;
        }

        public Patient LoggedPatient
        {
            get => loggedPatient;
            set
            {
                PatientChanged = true;
                loggedPatient = value;
                OnPropertyChanged("LoggedPatient");
            }
        }

        public String FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                FirstNameChanged = true;
                OnPropertyChanged("FirstName");
            }
        }

        public String ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }
        public String LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                LastNameChanged = true;
                OnPropertyChanged("LastName");
            }
        }

        public String Username
        {
            get => username;
            set
            {
                username = value;
                UsernameChanged = true;
                OnPropertyChanged("Username");
            }
        }

        public String Password
        {
            get => password;
            set
            {
                password = value;
                PasswordChanged = true;
                OnPropertyChanged("Password");
            }
        }

        public String Email
        {
            get => email;
            set
            {
                email = value;
                EmailChanged = true;
                OnPropertyChanged("Email");
            }
        }

        public String Address
        {
            get => address;
            set
            {
                address = value;
                AddressChanged = true;
                OnPropertyChanged("Address");
            }
        }

        public String PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;
                PhoneNumberChanged = true;
                OnPropertyChanged("PhoneNumber");
            }
        }

        public DateTime? DateOfBirth
        {
            get => dateOfBirth;
            set
            {
                dateOfBirth = value;
                DateOfBirthChanged = true;
                OnPropertyChanged("DateOfBirth");
            }
        }


        public bool UpdateCanExecute(object obj)
        {
            if ((FirstNameChanged == true) || (LastNameChanged == true) || (UsernameChanged == true) || (PasswordChanged == true) || (EmailChanged == true) || (AddressChanged == true) || (PhoneNumberChanged == true) || (DateOfBirthChanged == true))
            {
                if (Username == null || Username.Length < 3 || !usernameRegex.IsMatch(Username))
                {
                    ErrorMessage = "Korisničko ime nije validno!";
                    return false;
                }
                else if (Password == null || Password.Length < 8)
                {
                    ErrorMessage = "Lozinka nije validna, molim Vas unesite minimum 8 karaktera!";
                    return false;
                }
                else if (DateOfBirth == null || DateOfBirth > DateTime.Now)
                {
                    ErrorMessage = "Datum nije validan, Pokušajte ponovo!";
                    return false;
                }
                else if (Email == null || Email.Length == 0 || !emailRegex.IsMatch(Email))
                {
                    ErrorMessage = "Email nije validan, mora biti u obliku  'gmail.com!'";
                    return false;
                }
                else if (PhoneNumber == null || PhoneNumber.Length > 0 && !onlyNumberRegex.IsMatch(PhoneNumber))
                {
                    ErrorMessage = "Broj Telefona nije validan, dozvoljeni isključivo brojevi!";
                    return false;
                }
                else if (Address == null || Address.Length == 0 || !specialCharacters.IsMatch(Address))
                {
                    ErrorMessage = "Adresa nije validna, ne smije sadržati specijalne karaktere!";
                    return false;
                }
                else if (FirstName == null || FirstName.Length == 0 || FirstName.Length < 2 || !firstNameReg.IsMatch(FirstName))
                {
                    ErrorMessage = "Ime nije validno, mora sadržati tačno jedno veliko slovo i ne smije sadržati brojeve!"; return false;
                    return false;
                }
                else if (LastName == null || LastName.Length == 0 || LastName.Length < 2 || !firstNameReg.IsMatch(LastName))
                {
                    ErrorMessage = "Prezime nije validno, mora sadržati tačno jedno veliko slovo i ne smije sadržati brojeve!"; return false;
                }
                ErrorMessage = "";
                return true;
            }
            else return false;


        }

        private void UpdateExecute(object obj)
        {
            int pwLenght = Password.Length;
            String newPW = "";
            for (int i = 0; i < pwLenght; i++)
            {
                newPW = newPW.Replace("*", "");
            }
            Patient patient = App.patientController.GetOnePatient(App.loggedUser.Jmbg);
            App.patientController.ModifyPatient(false, patient.Allergens, patient.BloodTypeEnum, FirstName, LastName,
                patient.Jmbg, DateOfBirth, patient.Gender, Email, PhoneNumber, Address);
            MessageBox.Show("Uspješno izmjenjeni lični podaci!", "USPJEŠNO", MessageBoxButton.OK, MessageBoxImage.None);

        }

        private void BackExecute(object obj)
        {
            PatientWindowVM.NavigationService.Navigate(new PatientHomePage());
            int pwLenght = Password.Length;
            String newPW = "";
            for (int i = 0; i < pwLenght; i++)
            {
                newPW = newPW.Replace("*", "");
            }
        }


    }
}
