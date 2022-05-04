using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using ZdravoKorporacija.View.DoctorUI.Commands;

namespace ZdravoKorporacija.View.DoctorUI.ViewModel
{
    public class DoctorWindowVM
    {
        public static DoctorWindow? DoctorWindow;
        public static DoctorHomePage DoctorHomePage;
        public static NavigationService? NavigationService;

        public ICommand HomeCommand { get; set; }
        public ICommand MedicalRecordsCommand { get; set; }
        public ICommand AppointmentsCommand { get; set; }
        public ICommand VerificationsCommand { get; set; }
        public ICommand AbsenceRequestsCommand { get; set; }
        public ICommand LogOutCommand { get; set; }
        public ICommand ProfileCommand { get; set; }
        public ICommand NotificationsCommand { get; set; }

        public static void setWindowTitle(string newTitle)
        {
            DoctorWindow.WindowTitle.Text = newTitle;
        }

        public DoctorWindowVM(DoctorWindow doctorWindow)
        {
            DoctorWindow = doctorWindow;
            DoctorHomePage = new DoctorHomePage(this);
            DoctorWindow.DoctorMainFrame.Content = DoctorHomePage;
            NavigationService = DoctorWindow.DoctorMainFrame.NavigationService;
            HomeCommand = new RelayCommand(homeExecute);
            LogOutCommand = new RelayCommand(logOutExecute);
        }

        private void homeExecute(object parametar)
        {
            setWindowTitle("Appointment Schendule");
            NavigationService.Navigate(DoctorHomePage);
        }

        private void logOutExecute(object parameter)
        {
            MainWindow window = new MainWindow();
            DoctorWindow.Close();
            window.Show();
        }



    }
}
