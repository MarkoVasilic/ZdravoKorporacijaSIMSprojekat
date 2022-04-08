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
            var patientRepository = new PatientRepository();
            var patientService = new PatientService(patientRepository);
            patientController = new PatientController(patientService);

            var patient = new Patient(null, BloodType.A_MINUS, "Marko", "Vasilic", "mare", "123", "1234567891111", System.DateTime.Now,
                Gender.MALE, "marko.cda@vnda.com", "000000000", "pere perica 5a");
            patientController.CreatePatient(patient);
            patientController.DeletePatient(patient.jmbg);
            patientController.CreatePatient(patient);
            patient.lastName = "Konj";
            patientController.ModifyPatient(patient);
            patientController.CreateGuestAccount("milos", "vas", "456456465456");
        }

    }
}
