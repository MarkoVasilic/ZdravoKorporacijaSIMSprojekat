using Controller;
using Model;
using Repository;
using Service;
using System.Collections.ObjectModel;
using System.Windows;

namespace ZdravoKorporacija.View.AppointmentCRUD
{

    public partial class GetAllAppointmentsPatient : Window
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
            appointments = new ObservableCollection<Appointment>(appointmentController.GetAppointmentsByPatientJmbg("1111111111111"));
        }
    }
}