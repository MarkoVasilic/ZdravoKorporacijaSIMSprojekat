using Controller;
using Repository;
using Service;
using System;
using System.Windows;
using System.Windows.Controls;
using ZdravoKorporacija.Repository;
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
            PatientRepository patientRepository = new PatientRepository();
            DoctorRepository doctorRepository = new DoctorRepository();
            RoomRepository roomRepository = new RoomRepository();
            AdvancedRenovationJoiningRepository advancedRenovationJoining = new AdvancedRenovationJoiningRepository();
            AdvancedRenovationSeparationRepository advancedRenovationSeparation =
                new AdvancedRenovationSeparationRepository();
            BasicRenovationRepository basicRenovationRepository = new BasicRenovationRepository();
            ManagerRepository managerRepository = new ManagerRepository();
            SecretaryRepository secretaryRepository = new SecretaryRepository();
            MeetingRepository meetingRepository = new MeetingRepository();
            AppointmentService appointmentService = new AppointmentService(appointmentRepository, patientRepository, doctorRepository, roomRepository);
            ScheduleService scheduleService = new ScheduleService(appointmentRepository, patientRepository,
                doctorRepository, roomRepository, basicRenovationRepository, advancedRenovationJoining,
                advancedRenovationSeparation, managerRepository, secretaryRepository, meetingRepository);
            EmergencyService emergencyService = new EmergencyService(appointmentRepository, patientRepository,
                doctorRepository, roomRepository, basicRenovationRepository, advancedRenovationJoining,
                advancedRenovationSeparation, scheduleService);
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
