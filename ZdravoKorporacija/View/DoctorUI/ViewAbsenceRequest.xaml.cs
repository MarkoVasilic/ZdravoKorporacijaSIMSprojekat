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
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.Repository;
using Repository;

namespace ZdravoKorporacija.View.DoctorUI
{
    /// <summary>
    /// Interaction logic for ViewAbsenceRequest.xaml
    /// </summary>
    public partial class ViewAbsenceRequest : Page
    {
        private int Id;
        private AbsenceRequestController absenceRequestController { get; set; }
        public AbsenceRequest absenceRequest { get; set; }
        public ViewAbsenceRequest(int id)
        {
            InitializeComponent();
            this.Id = id;
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
            absenceRequest = absenceRequestController.GetOneById(Id);
        }
    }
}
