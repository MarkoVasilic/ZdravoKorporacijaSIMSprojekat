using Controller;
using Model;
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
    /// Interaction logic for CreateSeparation.xaml
    /// </summary>
    public partial class CreateSeparation : Page
    {
        private PossibleAppointmentsDTO selectedPossibleAppointment { get; set; }
        private AppointmentController appointmentController;
        public ObservableCollection<PossibleAppointmentsDTO> PossibleAppointments { get; set; }
        private String descriptionForRenovation;
        private PossibleAppointmentsDTO possibleAppointment;
        private AdvancedRenovationSeparationController advancedRenovationSeparationController;
        private int renovationRoomId;
        private int durationToSend;
        private String roomName1;
        private String roomName2;
        private String roomDescription1;
        private String roomDescription2;
        private RoomType roomType1;
        private RoomType roomType2;
 

        public CreateSeparation(int roomId, DateTime startDate, DateTime dateUntil, String duration, String firstRoomName, String firstRoomDescription, RoomType firstRoomType, String secondRoomName, String secondRoomDescription, RoomType secondRoomType)
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
            RoomService roomService = new RoomService(roomRepository);
            appointmentController = new AppointmentController(appointmentService, scheduleService, emergencyService);
            this.DataContext = this;

            PossibleAppointments = new ObservableCollection<PossibleAppointmentsDTO>(appointmentController.GetPossibleAppointmentsByManager(roomId, startDate, dateUntil, int.Parse(duration)));
                       
            setIndexesOfPossibleAppointments();
            AdvancedRenovationSeparationRepository advancedRenovationSeparationRepository = new AdvancedRenovationSeparationRepository();
            AdvancedRenovationSeparationService advancedRenovationSeparationService = new AdvancedRenovationSeparationService(advancedRenovationSeparationRepository, roomService);
            advancedRenovationSeparationController = new AdvancedRenovationSeparationController(advancedRenovationSeparationService);
            roomName1 = firstRoomName;
            roomName2 = secondRoomName;
            roomDescription1 = firstRoomDescription;
            roomDescription2 = secondRoomDescription;
            roomType1 = firstRoomType;
            roomType2 = secondRoomType;
            renovationRoomId = roomId;
            durationToSend = int.Parse(duration);
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
                advancedRenovationSeparationController.Create(renovationRoomId, selectedPossibleAppointment.StartTime, durationToSend, roomName1, roomName2, roomDescription1, roomDescription2, roomType1, roomType2);
                MessageBox.Show("Razdvajanje prostorija je uspešno zakazano", "Obaveštenje", MessageBoxButton.OK);
                NavigationService.Navigate(new ManagerHomePage());
            }
            catch (Exception ex)
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
