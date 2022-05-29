using System;
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

        public String LoggedUser { get; set; }


        public PatientWindowVM(PatientHomeWindow patientHomeWindow)
        {
            PatientHomePage = new PatientHomePage();
            PatientHomeWindow = patientHomeWindow;
            PatientHomeWindow.PatientMainFrame.Content = PatientHomePage;
            LoggedUser = App.loggedUser.FirstName + " " + App.loggedUser.LastName;
            NavigationService = PatientHomeWindow.PatientMainFrame.NavigationService;

        }



    }
}
