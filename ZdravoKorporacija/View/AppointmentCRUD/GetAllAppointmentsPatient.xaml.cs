using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Controller;
using Model;
using Repository;
using Service;

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