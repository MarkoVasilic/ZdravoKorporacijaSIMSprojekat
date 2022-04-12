using Controller;
using Model;
using Repository;
using Service;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ZdravoKorporacija.View.SecretaryUI.ViewModels;

namespace ZdravoKorporacija.View.SecretaryUI
{
    public partial class PatientDetailsPage : Page
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public PatientController PatientController { get; set; }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public Patient? Patient { get; set; }
        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged("password");
            }
        }
        public PatientDetailsPage(Patient patient)
        {
            InitializeComponent();
            PatientRepository patientRepository = new PatientRepository();
            PatientService patientService = new PatientService(patientRepository);
            PatientController = new PatientController(patientService);
            this.Patient = patient;
            this.Password = patient.Password;
            this.hidePassword();
            this.DataContext = this;

        }

        private void hidePassword()
        {
            StringBuilder passwordBuilder = new StringBuilder();
            for (int i = 0; i < password.Length; ++i)
            {
                passwordBuilder.Append("*");
            }
            PasswordTextBlock.Text = passwordBuilder.ToString();
            ShowPasswordIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Show;
        }

        private void showPassword()
        {
            PasswordTextBlock.Text = password;
            ShowPasswordIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Hide;
        }

        private void Show_Password_Button_Click(object sender, RoutedEventArgs e)
        {
            if (ShowPasswordIcon.Kind == MaterialDesignThemes.Wpf.PackIconKind.Show)
            {
                showPassword();
            }
            else
            {
                hidePassword();
            }

        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            YesNoQuestion.Visibility = Visibility.Visible;
            DeletePatientButton.Visibility = Visibility.Hidden;
        }

        private void Yes_Button_Click(object sender, RoutedEventArgs e)
        {
            PatientController.DeletePatient(Patient.Jmbg);
            NavigationService.Navigate(new PatientsView());
        }

        private void No_Button_Click(object sender, RoutedEventArgs e)
        {
            YesNoQuestion.Visibility = Visibility.Hidden;
            DeletePatientButton.Visibility = Visibility.Visible;
        }

        private void Edit_Basic_Info_Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditBasicInfo(Patient));
        }

        private void Reset_Password_Button_Click(object sender, RoutedEventArgs e)
        {
            Patient.Password = "sifra123";
            password = "sifra123";
            showPassword();
            PatientController.ModifyPatient(Patient);
        }
    }
}
