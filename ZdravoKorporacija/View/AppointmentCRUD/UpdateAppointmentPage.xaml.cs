using Controller;
using Repository;
using Service;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ZdravoKorporacija.View
{
    /// <summary>
    /// Interaction logic for UpdateAppointmentPage.xaml
    /// </summary>
    public partial class UpdateAppointmentPage : Page
    {
        private AppointmentController appointmentController;
        private String errorMessage;
        public UpdateAppointmentPage()
        {
            InitializeComponent();
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            AppointmentService appointmentService = new AppointmentService();
            appointmentController = new AppointmentController(appointmentService);
        }

        private void UpdateButton(object sender, RoutedEventArgs e)
        {
            int appointmentId = int.Parse(textBoxId.Text);

            DateTime date = (DateTime)datePicker.SelectedDate;
            Console.WriteLine("Datum iz XAML-a" + date + " Trenutno Datum " + System.DateTime.Now);
            Console.WriteLine("ID iz XAML-a" + appointmentId);


            try
            {
                appointmentController.ModifyAppointment(appointmentId, date);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }
    }
}
