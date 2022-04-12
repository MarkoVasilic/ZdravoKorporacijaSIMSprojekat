using Controller;
using Repository;
using Service;
using System.Windows;

namespace ZdravoKorporacija
{
    public partial class App : Application
    {
        public PatientController? patientController { get; set; }
        public RoomController? roomController { get; set; }

        public App()
        {
            PatientRepository patientRepository = new PatientRepository();
            PatientService patientService = new PatientService(patientRepository);
            patientController = new PatientController(patientService);
        }

    }
}
