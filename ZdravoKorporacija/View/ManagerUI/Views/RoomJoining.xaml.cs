using Controller;
using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for RoomJoining.xaml
    /// </summary>
    public partial class RoomJoining : Page
    {
        private RoomController roomController;
        public ObservableCollection<Room> Rooms { get; set; }
        private DateTime start;
        private DateTime end;
        private String durationToSend;
        private String _newRoomName = " ";
        private String _newRoomDescription = " ";
        private RoomType _newRoomType;
        private List<Room> selectedRooms;
        private AdvancedRenovationJoiningController advancedRenovationJoiningController;
        private AppointmentController appointmentController;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public String NewRoomName
        {
            get { return _newRoomName; }
            set
            {
                _newRoomName = value;
                OnPropertyChanged("NewRoomName");
            }
        }


        public String NewRoomDescription
        {
            get { return _newRoomDescription; }
            set
            {
                _newRoomDescription = value;
                OnPropertyChanged("NewRoomDescription");
            }
        }

        public void setNewRoomType()
        {
            if (newRoomTypeComboBox.SelectedIndex == 0)
            {
                _newRoomType = RoomType.EXAMINATION;
            }
            else if (newRoomTypeComboBox.SelectedIndex == 1)
            {
                _newRoomType = RoomType.CONFERENCE;
            }
            else if (newRoomTypeComboBox.SelectedIndex == 2)
            {
                _newRoomType = RoomType.SURGERY;
            }
        }


        public RoomJoining(DateTime dateFrom, DateTime dateUntil, String duration)
        {
            InitializeComponent();
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
            AdvancedRenovationJoiningRepository advancedRenovationJoiningRepository = new AdvancedRenovationJoiningRepository();
            AdvancedRenovationJoiningService advancedRenovationJoiningService = new AdvancedRenovationJoiningService(advancedRenovationJoiningRepository, roomService, appointmentService, basicRenovationService, equipmentService, scheduleService, displacementService);
            advancedRenovationJoiningController = new AdvancedRenovationJoiningController(advancedRenovationJoiningService);
            roomController = new RoomController(roomService);
            this.DataContext = this;
            Rooms = new ObservableCollection<Room>(roomController.GetAllRooms());
            start = dateFrom;
            end = dateUntil;
            selectedRooms = new List<Room>();
            durationToSend = duration;
        }

        private void GoBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void GoBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NavigationService.Navigate(new ChooseRenovationType());
        }

        private void PossibleAppoitments_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                advancedRenovationJoiningController.GetPossibleAppointments(selectedRooms[0].Id, selectedRooms[1].Id, start, end, int.Parse(durationToSend));
                NavigationService.Navigate(new CreateJoining(selectedRooms[0].Id, selectedRooms[1].Id, start, end, durationToSend, NewRoomName, NewRoomDescription, _newRoomType));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška");
                ManagerHomeWindow.NavigationService.Navigate(new ChooseRenovationType());
            }

        }


        private void CheckedBox_Checked(object sender, RoutedEventArgs e)
        {
            int id = (int)((CheckBox)sender).Tag;

            foreach (Room r in Rooms)
            {
                if (r.Id == id)
                    selectedRooms.Add(r);
            }
        }

        private void CheckBoxList_Unchecked(object sender, RoutedEventArgs e)
        {
            int id = (int)((CheckBox)sender).Tag;

            foreach (Room r in Rooms)
            {
                if (r.Id == id)
                    selectedRooms.Remove(r);
            }
        }
    }
}
