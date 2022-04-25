using Controller;
using Repository;
using Service;
using System;
using System.Windows;
using System.Windows.Controls;

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
            appointmentController = new AppointmentController(appointmentService);
        }

        private void DeleteAppointmentButton(object sender, RoutedEventArgs e)
        {

            Id = int.Parse(textBoxDeleteAppointment.Text);

            errorMessage = appointmentController.DeleteAppointment(Id);

            if (errorMessage.Length == 0)
            {

            }
            else
            {
                MessageBox.Show(appointmentController.DeleteAppointment(Id), "Error");

            }

        }
    }
}
