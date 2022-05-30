using System.Windows.Input;
using System.Windows.Navigation;
using ZdravoKorporacija.View.SecretaryUI.Commands;

namespace ZdravoKorporacija.View.SecretaryUI.ViewModels
{
    public class SecretaryWindowVM
    {
        public static SecretaryWindow? SecretaryWindow;
        public static SecretaryHomePage SecretaryHomePage;
        public static NavigationService? NavigationService { get; set; }
        public ICommand HomeCommand { get; set; }
        public ICommand LogOutCommand { get; set; }
        public ICommand CheckSheduledAppointmentsCommand { get; set; }
        public ICommand SheduleAppointmentCommand { get; set; }
        public ICommand PatientAccountsCommand { get; set; }
        public ICommand ScheduleEmergencyCommand { get; set; }
        public ICommand OrderEquipmentCommand { get; set; }
        public ICommand ScheduleMeetingCommand { get; set; }
        public ICommand AbsenceRequestCommand { get; set; }
        public ICommand ScheduledMeetingsCommand { get; set; }
        public ICommand NotificationCommand { get; set; }
        public ICommand WeeklyReportCommand { get; set; }
        public static void setWindowTitle(string newTitle)
        {
            SecretaryWindow.WindowTitle.Text = newTitle;
        }

        public SecretaryWindowVM(SecretaryWindow secretaryWindow)
        {
            SecretaryWindow = secretaryWindow;
            SecretaryHomePage = new SecretaryHomePage(this);
            SecretaryWindow.SecretaryMainFrame.Content = SecretaryHomePage;
            NavigationService = SecretaryWindow.SecretaryMainFrame.NavigationService;
            HomeCommand = new RelayCommand(homeExecute);
            LogOutCommand = new RelayCommand(logOutExecute);
            CheckSheduledAppointmentsCommand = new RelayCommand(checkSheduledAppointmentsExecute);
            SheduleAppointmentCommand = new RelayCommand(sheduleAppointmentExecute);
            PatientAccountsCommand = new RelayCommand(patientAccountsExecute);
            ScheduleEmergencyCommand = new RelayCommand(scheduleEmergencyExecute);
            OrderEquipmentCommand = new RelayCommand(orderEquipmentExecute);
            ScheduleMeetingCommand = new RelayCommand(scheduleMeetingExecute);
            AbsenceRequestCommand = new RelayCommand(absenceRequestExecute);
            ScheduledMeetingsCommand = new RelayCommand(scheduledMeetingsExecute);
            NotificationCommand = new RelayCommand(notificationExecute);
            WeeklyReportCommand = new RelayCommand(weeklyReportExecute);
        }
        private void notificationExecute(object parameter)
        {
            setWindowTitle("Notifications");
            NavigationService.Navigate(new NotificationsPage());
        }

        private void homeExecute(object parameter)
        {
            setWindowTitle("Dashboard");
            NavigationService.Navigate(SecretaryHomePage);
        }

        private void logOutExecute(object parameter)
        {
            MainWindow window = new MainWindow();
            SecretaryWindow.Close();
            window.Show();
        }

        private void checkSheduledAppointmentsExecute(object parameter)
        {
            NavigationService.Navigate(new AppointmentView());
        }

        private void sheduleAppointmentExecute(object parameter)
        {
            NavigationService.Navigate(new ScheduleAppointmentView());
        }

        private void patientAccountsExecute(object parameter)
        {
            NavigationService.Navigate(new PatientsView());
        }

        private void scheduleEmergencyExecute(object parameter)
        {
            NavigationService.Navigate(new ScheduleEmergencyView());
        }

        private void orderEquipmentExecute(object parameter)
        {
            NavigationService.Navigate(new OrderEquipmentPage(this));
        }

        private void scheduleMeetingExecute(object parameter)
        {
            NavigationService.Navigate(new ScheduleMeetingPage());
        }
        private void scheduledMeetingsExecute(object parameter)
        {
            NavigationService.Navigate(new CheckScheduledMeetingsPage());
        }
        private void absenceRequestExecute(object parameter)
        {
            NavigationService.Navigate(new AbsceneRequestsPage());
        }
        private void weeklyReportExecute(object parameter)
        {
            NavigationService.Navigate(new CurrentWeekReportPage());
        }
    }
}
