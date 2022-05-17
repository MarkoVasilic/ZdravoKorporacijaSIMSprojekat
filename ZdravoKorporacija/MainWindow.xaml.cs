using System.Windows;
using ZdravoKorporacija.View;
using ZdravoKorporacija.View.RoomCRUD;
using ZdravoKorporacija.View.SecretaryUI;
using ZdravoKorporacija.View.DoctorUI;
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
            DateTime currentDate = System.DateTime.Now.Date;
            DateTime firstDayInMonth = new DateTime(currentDate.Year, currentDate.Month, 1);


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
                Console.WriteLine("Your current TrollCounter: " + App.patientController.getTrollCounterByPatient(patient.Jmbg));
                if (currentDate == firstDayInMonth.Date)
                {
                    // patient.trollCounter = 0; //patient.resetTrolLCounter();
                    App.patientController.resetTrollCounterByPatient(patient.Jmbg);
                    Console.WriteLine("iniitializing troll counter to 0 firstDayInMonth" + App.patientController.getTrollCounterByPatient(patient.Jmbg));
                }

                if (App.patientController.getTrollCounterByPatient(patient.Jmbg) >= 4)
                {
                    MessageBox.Show("Blocked - AntiTroll: " + App.patientController.getTrollCounterByPatient(patient.Jmbg) + " Tries");
                    this.Close();
                }
                
                window = new PatientHomeWindow();
            }
            else if (doctor != null)
            {
                App.loggedUser = doctor;
                App.userRole = "doctor";
                correctUsername = true;
                window = new DoctorWindow();
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
