using Model;
using System;
using System.Collections.Generic;
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
    public partial class PatientDetailsPage : Page
    {
        public event PropertyChangedEventHandler? PropertyChanged;

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
    }
}
