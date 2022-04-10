using Controller;
using Repository;
using Service;
using Model;
using System.Windows;

namespace ZdravoKorporacija
{
    public partial class App : Application
    {
        public PatientController patientController { get; set; }
        public App()
        {
            PatientRepository patientRepository = new PatientRepository();
            PatientService patientService = new PatientService(patientRepository);
            patientController = new PatientController(patientService);

            Patient patient = new Patient(false, null, BloodType.A_PLUS, "Marko", "Vasilic", "mare", "konj", "123456789", System.DateTime.Now,
                Gender.MALE, "marko.12@gmail.com", "456132", "periceva 5");
            Patient patient1 = new Patient(false, null, BloodType.A_PLUS, "Marko", "Vasilic", "mare", "konj", "88888888", System.DateTime.Now,
                Gender.MALE, "marko.12@gmail.com", "456132", "periceva 5");
            Patient patient2 = new Patient(false, null, BloodType.A_PLUS, "Marko", "Vasilic", "mare", "konj", "99999999", System.DateTime.Now,
                Gender.MALE, "marko.12@gmail.com", "456132", "periceva 5");
            Patient patient3 = new Patient(false, null, BloodType.A_PLUS, "Marko", "Vasilic", "mare", "konj", "00000000", System.DateTime.Now,
                Gender.MALE, "marko.12@gmail.com", "456132", "periceva 5");
            Patient patient4 = new Patient(false, null, BloodType.A_PLUS, "Marko", "Vasilic", "mare", "konj", "1111111111", System.DateTime.Now,
                Gender.MALE, "marko.12@gmail.com", "456132", "periceva 5");
            Patient patient5 = new Patient(false, null, BloodType.A_PLUS, "Marko", "Vasilic", "mare", "konj", "222222222", System.DateTime.Now,
                Gender.MALE, "marko.12@gmail.com", "456132", "periceva 5");
            Patient patient6 = new Patient(false, null, BloodType.A_PLUS, "Marko", "Vasilic", "mare", "konj", "3333333333", System.DateTime.Now,
                Gender.MALE, "marko.12@gmail.com", "456132", "periceva 5");
            Patient patient7 = new Patient(false, null, BloodType.A_PLUS, "Marko", "Vasilic", "mare", "konj", "963741", System.DateTime.Now,
                Gender.MALE, "marko.12@gmail.com", "456132", "periceva 5");
            Patient patient8 = new Patient(false, null, BloodType.A_PLUS, "Marko", "Vasilic", "mare", "konj", "7532147", System.DateTime.Now,
                Gender.MALE, "marko.12@gmail.com", "456132", "periceva 5");
            patientController.CreatePatient(patient);
            patientController.CreatePatient(patient1);
            patientController.CreatePatient(patient2);
            patientController.CreatePatient(patient3);
            patientController.CreatePatient(patient4);
            patientController.CreatePatient(patient5);
            patientController.CreatePatient(patient6);
            patientController.CreatePatient(patient7);
            patientController.CreatePatient(patient8);
            patient8.Address = "jbgaa";
            patientController.ModifyPatient(patient8);
        }

    }
}
