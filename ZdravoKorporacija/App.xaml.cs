using Controller;
using Repository;
using Service;
using Model;
using System.Windows;
using System.Collections.Generic;

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
