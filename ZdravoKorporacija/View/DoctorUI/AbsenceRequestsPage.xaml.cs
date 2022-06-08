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
using Model;
using Repository;
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.View.DoctorUI
{
    /// <summary>
    /// Interaction logic for AbsenceRequestsPage.xaml
    /// </summary>
    public partial class AbsenceRequestsPage : Page
    {
        private AbsenceRequestController absenceRequestController;
        public ObservableCollection<AbsenceRequest> absenceRequests { get; set; }
        public AbsenceRequestsPage()
        {
            InitializeComponent();
            AbsenceRequestRepository absenceRequestRepository = new AbsenceRequestRepository();
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            PatientRepository patientRepository = new PatientRepository();
            RoomRepository roomRepository = new RoomRepository();
            BasicRenovationRepository basicRenovation = new BasicRenovationRepository();
            AdvancedRenovationJoiningRepository advancedRenovationJoining = new AdvancedRenovationJoiningRepository();
            AdvancedRenovationSeparationRepository advancedRenovationSeparation =
                new AdvancedRenovationSeparationRepository();
            ManagerRepository managerRepository = new ManagerRepository();
            SecretaryRepository secretaryRepository = new SecretaryRepository();
            MeetingRepository meetingRepository = new MeetingRepository();
            DoctorRepository doctorRepository = new DoctorRepository();
            ScheduleService scheduleService = new ScheduleService(appointmentRepository, patientRepository,
                doctorRepository, roomRepository, basicRenovation, advancedRenovationJoining,
                advancedRenovationSeparation, managerRepository, secretaryRepository, meetingRepository);
            AbsenceRequestService absenceRequestService = new AbsenceRequestService(absenceRequestRepository, scheduleService, doctorRepository, appointmentRepository);
            absenceRequestController = new AbsenceRequestController(absenceRequestService);
            this.DataContext = this;
            absenceRequests = new ObservableCollection<AbsenceRequest>(absenceRequestController.GetAllByDoctorJmbg(App.loggedUser.Jmbg));

        }
        private void ViewDetailsOfAbsenceRequest(object sender, RoutedEventArgs e)
        {
            int Id = (int)((Button)sender).CommandParameter;
            NavigationService.Navigate(new ViewAbsenceRequest(Id));


        }

        private void CreateAbsenceRequestClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateAbsenceRequest());

        }
    }
}
