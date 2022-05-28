using Controller;
using Repository;
using Service;
using System;
using System.Windows;
using System.Windows.Controls;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.View
{
    /// <summary>
    /// Interaction logic for DeleteAppointmentPage.xaml
    /// </summary>
    public partial class DeleteAppointmentPage : Page
    {
        private AppointmentController appointmentController;
        private int Id;
        private String errorMessage;
        public DeleteAppointmentPage()
        {
            InitializeComponent();
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            AppointmentService appointmentService = new AppointmentService();
            ScheduleService scheduleService = new ScheduleService();
            EmergencyService emergencyService = new EmergencyService();
            appointmentController = new AppointmentController(appointmentService, scheduleService, emergencyService);
        }

        private void DeleteAppointmentButton(object sender, RoutedEventArgs e)
        {

            Id = int.Parse(textBoxDeleteAppointment.Text);
            try
            {
                appointmentController.DeleteAppointment(Id);
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                MessageBox.Show(errorMessage, "Error");
            }

        }
    }
}
