using System.Windows;
using System.Windows.Controls;
using ZdravoKorporacija.View.SecretaryUI.ViewModels;

namespace ZdravoKorporacija.View.SecretaryUI
{
    /// <summary>
    /// Interaction logic for SelectAppointmentInfo.xaml
    /// </summary>
    public partial class SelectAppointmentInfo : Page
    {
        private ScheduleAppointmentVM scheduleAppointmentVM;
        public SelectAppointmentInfo(ScheduleAppointmentVM scheduleAppointmentVM)
        {
            InitializeComponent();
            this.scheduleAppointmentVM = scheduleAppointmentVM;
            DataContext = scheduleAppointmentVM;
        }

        private void Search_Appointment_Button_Click(object sender, RoutedEventArgs e)
        {
            if (DurationComboBox.SelectedIndex == 0)
                scheduleAppointmentVM.SelectedDuration = 30;
            else if (DurationComboBox.SelectedIndex == 1)
                scheduleAppointmentVM.SelectedDuration = 45;
            else if (DurationComboBox.SelectedIndex == 2)
                scheduleAppointmentVM.SelectedDuration = 60;
            else if (DurationComboBox.SelectedIndex == 3)
                scheduleAppointmentVM.SelectedDuration = 90;
            else if (DurationComboBox.SelectedIndex == 4)
                scheduleAppointmentVM.SelectedDuration = 120;
            else if (DurationComboBox.SelectedIndex == 5)
                scheduleAppointmentVM.SelectedDuration = 150;
            else if (DurationComboBox.SelectedIndex == 6)
                scheduleAppointmentVM.SelectedDuration = 180;
            else if (DurationComboBox.SelectedIndex == 7)
                scheduleAppointmentVM.SelectedDuration = 240;
            else if (DurationComboBox.SelectedIndex == 8)
                scheduleAppointmentVM.SelectedDuration = 300;
            else if (DurationComboBox.SelectedIndex == 9)
                scheduleAppointmentVM.SelectedDuration = 360;
            else if (DurationComboBox.SelectedIndex == 10)
                scheduleAppointmentVM.SelectedDuration = 420;
            else
                scheduleAppointmentVM.SelectedDuration = 0;

            if (DoctorPriority.IsChecked == true)
                scheduleAppointmentVM.SelectedPriority = "doctor";
            else
                scheduleAppointmentVM.SelectedPriority = "time";
        }
    }
}
