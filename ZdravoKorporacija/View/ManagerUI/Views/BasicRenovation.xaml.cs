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
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for BasicRenovation.xaml
    /// </summary>
    public partial class BasicRenovation : Page
    {

        private int checkedRoomId;
        private RoomController roomController;
        public ObservableCollection<Room> Rooms { get; set; }
        private DateTime start;
        private DateTime end;
        private String durationToSend;
        private String description;
        private AppointmentController appointmentController;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public String Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }



        public BasicRenovation(DateTime dateFrom, DateTime dateUntil, String duration)
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
            roomController = new RoomController(roomService);
            this.DataContext = this;
            Rooms = new ObservableCollection<Room>(roomController.GetAllRooms());
            start = dateFrom;
            end = dateUntil;
            durationToSend = duration;
        }

        private void PossibleAppoitments_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                appointmentController.GetPossibleAppointmentsByManager(checkedRoomId, start, end, int.Parse(durationToSend));
                NavigationService.Navigate(new CreateBasicRenovation(checkedRoomId, start, end, durationToSend, Description));
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška");
                ManagerHomeWindow.NavigationService.Navigate(new ChooseRenovationType());
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

            foreach (Room r in Rooms)
            {
                if (r.Id == id)
                    checkedRoomId = id;
            }

        }

    }
}
