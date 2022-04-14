using Controller;
using Repository;
using Service;
using System;
using System.Windows;

namespace ZdravoKorporacija.View
{
    /// <summary>
    /// Interaction logic for UpdateAppointmentPage.xaml
    /// </summary>
    public partial class UpdateAppointmentPage : Window
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




            errorMessage = appointmentController.ModifyAppointment(appointmentId, date);

            if (errorMessage.Length == 0)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show(appointmentController.ModifyAppointment(appointmentId, date), "Error");
                this.Close();
            }

        }
    }
}
