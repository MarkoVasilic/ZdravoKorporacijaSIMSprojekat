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
    /// Interaction logic for CreateJoining.xaml
    /// </summary>
    public partial class CreateJoining : Page
    {
  
        private AppointmentController appointmentController;
        public ObservableCollection<PossibleAppointmentsDTO> PossibleAppointments { get; set; }
        private PossibleAppointmentsDTO possibleAppointment;
        private PossibleAppointmentsDTO selectedPossibleAppointment { get; set; }
        private AdvancedRenovationJoiningController advancedRenovationJoiningController;
        private int firstRenovationRoomId;
        private int secondRenovationRoomId;
        private int durationToSend;
        private String newName;
        private String newDescription;
        private RoomType roomType;

        public CreateJoining(int firstRoomId, int secondRoomId, DateTime start, DateTime end, String duration, String NewRoomName, string NewRoomDescription, RoomType newRoomType)
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
            DisplacementRepository displacementRepository = new DisplacementRepository();
            EquipmentRepository equipmentRepository = new EquipmentRepository();
            EquipmentService equipmentService = new EquipmentService(equipmentRepository, roomRepository);
            appointmentController = new AppointmentController(appointmentService, scheduleService, emergencyService);
            BasicRenovationService basicRenovationService = new BasicRenovationService(basicRenovationRepository, roomRepository);
            DisplacementService displacementService = new DisplacementService(displacementRepository, equipmentRepository, roomRepository);
            this.DataContext = this;
            AdvancedRenovationJoiningRepository advancedRenovationJoiningRepository = new AdvancedRenovationJoiningRepository();
            AdvancedRenovationJoiningService advancedRenovationJoiningService = new AdvancedRenovationJoiningService(advancedRenovationJoiningRepository, roomService, appointmentService, basicRenovationService, equipmentService, scheduleService, displacementService);
            advancedRenovationJoiningController = new AdvancedRenovationJoiningController(advancedRenovationJoiningService);
            PossibleAppointments = new ObservableCollection<PossibleAppointmentsDTO>(advancedRenovationJoiningController.GetPossibleAppointments(firstRoomId, secondRoomId, start, end, int.Parse(duration)));
            setIndexesOfPossibleAppointments();
            firstRenovationRoomId = firstRoomId;
            secondRenovationRoomId = secondRoomId;
            durationToSend = int.Parse(duration);
            newName = NewRoomName;
            newDescription = NewRoomDescription;
            roomType = newRoomType;
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
                advancedRenovationJoiningController.Create(firstRenovationRoomId, secondRenovationRoomId, selectedPossibleAppointment.StartTime, durationToSend, newName, newDescription, roomType);
                MessageBox.Show("Spajanje prostorija je uspešno zakazano", "Obaveštenje", MessageBoxButton.OK);
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
