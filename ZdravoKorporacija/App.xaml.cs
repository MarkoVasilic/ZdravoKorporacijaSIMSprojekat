using Controller;
using System.Windows;
using System;
using System.Diagnostics;

namespace ZdravoKorporacija
{
    public partial class App : Application
    {
        public PatientController? patientController { get; set; }
        public RoomController? roomController { get; set; }

        public App()
        {

        }

    }
}
