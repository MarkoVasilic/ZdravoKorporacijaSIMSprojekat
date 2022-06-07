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
            ScheduleService scheduleService = new ScheduleService();
            DoctorRepository doctorRepository = new DoctorRepository();
            AbsenceRequestService absenceRequestService = new AbsenceRequestService(absenceRequestRepository, scheduleService, doctorRepository);
            absenceRequestController = new AbsenceRequestController(absenceRequestService);
            this.DataContext = this;
            absenceRequest = absenceRequestController.GetOneById(Id);
        }
    }
}
