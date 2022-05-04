using System.Windows;
using ZdravoKorporacija.View;
using ZdravoKorporacija.View.RoomCRUD;
using ZdravoKorporacija.View.SecretaryUI;
using Model;
using System;
using ZdravoKorporacija.View.ManagerUI.Views;

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
            bool correctUsername = false;
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
                correctUsername = true;
                window = new ManagerHomeWindow();
            }
            else if (secretary != null)
            {
                App.loggedUser = secretary;
                App.userRole = "secretary";
                correctUsername = true;
                window = new SecretaryWindow();
            }
            else if (patient != null)
            {
                App.loggedUser = patient;
                App.userRole = "patient";
                correctUsername = true;
                window = new PatientHomeWindow();
            }
            else if (doctor != null)
            {
                App.loggedUser = doctor;
                App.userRole = "doctor";
                correctUsername = true;
                //window = ;
            }
            else
            {
                MessageBox.Show("Wrong username!");
            }

            if (correctUsername && password == App.loggedUser.Password)
            {
                window.Show();
                this.Close();
            }
            else
            {
                if (correctUsername)
                    MessageBox.Show("Wrong password!");
            }
        }
    }
}
