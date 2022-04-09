using Controller;
using System.Windows;

namespace ZdravoKorporacija
{
    public partial class App : Application
    {
        public PatientController patientController { get; set; }
        public App()
        {

        }

    }
}
