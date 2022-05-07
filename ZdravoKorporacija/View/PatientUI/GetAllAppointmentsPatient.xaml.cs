using Controller;
using Model;
using Repository;
using Service;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ZdravoKorporacija.View.AppointmentCRUD
{

    public partial class GetAllAppointmentsPatient : Page
    {
        private AppointmentController appointmentController;
        public ObservableCollection<Appointment> appointments { get; set; }

        public GetAllAppointmentsPatient()
        {
            InitializeComponent();
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            AppointmentService appointmentService = new AppointmentService();
            appointmentController = new AppointmentController(appointmentService);
            this.DataContext = this;
            appointments = new ObservableCollection<Appointment>(appointmentController.GetAppointmentsByPatientJmbg(App.loggedUser.Jmbg));
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}