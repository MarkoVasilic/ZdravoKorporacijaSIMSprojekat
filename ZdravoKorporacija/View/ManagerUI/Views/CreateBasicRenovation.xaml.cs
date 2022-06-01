using Controller;
using Repository;
using Service;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.View.RoomCRUD;

namespace ZdravoKorporacija.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for CreateBasicRenovation.xaml
    /// </summary>
    public partial class CreateBasicRenovation : Page
    {

        private PossibleAppointmentsDTO selectedPossibleAppointment { get; set; }
        private AppointmentController appointmentController;
        public ObservableCollection<PossibleAppointmentsDTO> PossibleAppointments { get; set; }
        private String descriptionForRenovation;
        private PossibleAppointmentsDTO possibleAppointment;
        private BasicRenovationController basicRenovationController;
        private int renovationRoomId;
        private int durationToSend;


        public CreateBasicRenovation(int roomId, DateTime dateFrom, DateTime dateUntil, int duration, String description)
        {
            InitializeComponent();
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            PatientRepository patientRepository = new PatientRepository();
            DoctorRepository doctorRepository = new DoctorRepository();
            RoomRepository roomRepository = new RoomRepository();
            AppointmentService appointmentService = new AppointmentService(appointmentRepository, patientRepository, doctorRepository, roomRepository);
            ScheduleService scheduleService = new ScheduleService();
            EmergencyService emergencyService = new EmergencyService();
            RoomService roomService = new RoomService(roomRepository);
            appointmentController = new AppointmentController(appointmentService, scheduleService, emergencyService);
            this.DataContext = this;
            PossibleAppointments = new ObservableCollection<PossibleAppointmentsDTO>(appointmentController.GetPossibleAppointmentsByManager(roomId, dateFrom, dateUntil, duration));
            setIndexesOfPossibleAppointments();
            descriptionForRenovation = description;
            BasicRenovationRepository basicRenovationRepository = new BasicRenovationRepository();
            BasicRenovationService basicRenovationService = new BasicRenovationService(basicRenovationRepository, roomRepository);
            basicRenovationController = new BasicRenovationController(basicRenovationService);
            renovationRoomId = roomId;
            durationToSend = duration;
        }

        private void setIndexesOfPossibleAppointments()
        {
            int index = 0;
            foreach (var pa in PossibleAppointments)
            {
                pa.AppointmentId = index;
                index++;
            }
        }

        private void createRenovation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                basicRenovationController.CreateBasicRenovation(renovationRoomId, selectedPossibleAppointment.StartTime, durationToSend, descriptionForRenovation);
                MessageBox.Show("Osnovno renoviranje je uspešno zakazano", "Obaveštenje", MessageBoxButton.OK);
                NavigationService.Navigate(new ManagerHomePage());
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška");
            }

        }

        private void GoBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void GoBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NavigationService.Navigate(new ChooseRenovationType());
        }


        private void RadioButtonList_Checked(object sender, RoutedEventArgs e)
        {
            int id = (int)((RadioButton)sender).Tag;

            foreach (PossibleAppointmentsDTO pa in PossibleAppointments)
            {
                if (pa.AppointmentId == id)
                    selectedPossibleAppointment = pa;
            }

        }

    }
}
