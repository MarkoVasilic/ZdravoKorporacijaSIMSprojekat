using System.Windows;
using ZdravoKorporacija.View;
using ZdravoKorporacija.View.RoomCRUD;
using ZdravoKorporacija.View.SecretaryUI;
using Model;
using System;

namespace ZdravoKorporacija
{
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = null;
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Password;
            Manager manager = App.managerController.getManagerByUsername(username);
            Secretary secretary = App.secretaryController.getSecretaryByUsername(username);
            Patient patient = App.patientController.getPatientByUsername(username);
            Doctor doctor = App.doctorController.getDoctorByUsername(username);
            if (manager != null)
            {
                App.loggedUser = manager;
                App.userRole = "manager";
                window = new ManagerHomePage();
            }
            else if (secretary != null)
            {
                App.loggedUser = secretary;
                App.userRole = "secretary";
                window = new SecretaryWindow();
            }
            else if (patient != null)
            {
                App.loggedUser = patient;
                App.userRole = "patient";
                window = new PatientHomeWindow();
            }
            else if (doctor != null)
            {
                App.loggedUser = doctor;
                App.userRole = "doctor";
                //window = ;
            }
            else
            {
                MessageBox.Show("Username not registered!");
            }

            if (password == App.loggedUser.Password)
            {
                window.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong password!");
            }
        }
    }
}
