using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using ZdravoKorporacija.View;
using ZdravoKorporacija.View.PatientUI;

namespace ZdravoKorporacija.View.PatientUI.ViewModels
{
    internal class PatientWindowVM
    {
        public static PatientHomeWindow PatientHomeWindow;
        public static NavigationService? NavigationService { get; set; }
        
        public static PatientHomePage? PatientHomePage;


        public PatientWindowVM(PatientHomeWindow patientHomeWindow)
        {
            PatientHomePage = new PatientHomePage();
            PatientHomeWindow = patientHomeWindow;
            PatientHomeWindow.PatientMainFrame.Content = PatientHomePage;
            
            NavigationService = PatientHomeWindow.PatientMainFrame.NavigationService;
           
        }



    }
}
