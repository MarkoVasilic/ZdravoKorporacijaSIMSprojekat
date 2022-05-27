using System.Windows.Navigation;

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
