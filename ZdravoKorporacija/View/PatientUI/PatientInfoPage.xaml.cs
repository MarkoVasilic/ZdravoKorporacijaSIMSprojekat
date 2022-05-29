using Controller;
using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
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

namespace ZdravoKorporacija.View.PatientUI
{
    /// <summary>
    /// Interaction logic for PatientInfoPage.xaml
    /// </summary>
    public partial class PatientInfoPage : Page
    {
        PatientController PatientController { get; set; }
        Patient modifiedPatient { get; set; }
        public PatientInfoPage()
        {
            InitializeComponent();
            modifiedPatient = App.patientController.GetOnePatient(App.loggedUser.Jmbg);
            /*    Ime.Text = App.loggedUser.FirstName;
                Prezime.Text = App.loggedUser.LastName;
                Adresa.Text = App.loggedUser.Address;
                Email.Text = App.loggedUser.Email;
                Username.Text = App.loggedUser.Username;
                Password.Password = App.loggedUser.Password;
                DateOfBirth.SelectedDate = App.loggedUser.DateOfBirth;
                PhoneNumber.Text = App.loggedUser.PhoneNumber;*/
            Ime.Text = modifiedPatient.FirstName;
            Prezime.Text = modifiedPatient.LastName;
            Adresa.Text = modifiedPatient.Address;
            Email.Text = modifiedPatient.Email;
            Username.Text = modifiedPatient.Username;
            Password.Password = modifiedPatient.Password;
            DateOfBirth.SelectedDate = modifiedPatient.DateOfBirth;
            PhoneNumber.Text = modifiedPatient.PhoneNumber;
            modifiedPatient = App.patientController.GetOnePatient(App.loggedUser.Jmbg);
        }

        private void ProfilNazadButton(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PatientHomePage());
        }

        private void modifyPatient(object sender, RoutedEventArgs e)
        {
            // App.patientController.ModifyPatient(false, null, 0, Ime.Text, Prezime.Text, App.loggedUser.Jmbg, DateOfBirth.SelectedDate, App.loggedUser.Gender, Email.Text, PhoneNumber.Text, Adresa.Text);

            App.patientController.ModifyInfo(Ime.Text, Prezime.Text, DateOfBirth.SelectedDate, Email.Text, PhoneNumber.Text, Adresa.Text, Username.Text, Password.Password, App.loggedUser.Jmbg);
            MessageBox.Show("Updated info saved, changes will be made on log in!");
            modifiedPatient = App.patientController.GetOnePatient(modifiedPatient.Jmbg);
            Ime.Text = modifiedPatient.FirstName;
            Prezime.Text = modifiedPatient.LastName;
            Adresa.Text = modifiedPatient.Address;
            Email.Text = modifiedPatient.Email;
            Username.Text = modifiedPatient.Username;
            Password.Password = modifiedPatient.Password;
            DateOfBirth.SelectedDate = modifiedPatient.DateOfBirth;
            PhoneNumber.Text = modifiedPatient.PhoneNumber;
            NavigationService.Refresh();
        }
    }
}
