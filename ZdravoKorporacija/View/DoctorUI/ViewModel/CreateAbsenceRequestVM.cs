using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.View.DoctorUI.Commands;

namespace ZdravoKorporacija.View.DoctorUI.ViewModel
{
    public class CreateAbsenceRequestVM : ViewModelBase
    {
        public AbsenceRequestController absenceRequestController { get; set; }

        private String errorMessage;
        public String ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                errorMessage = value;
                OnProperyChanged("ErrorMessage");
            }
        }

        private String absenceReason;
        public String AbsenceReason
        {
            get { return absenceReason; }
            set
            {
                absenceReason = value;
                OnProperyChanged("AbsenceReason");
            }
        }

        private DateTime dateFrom;
        public DateTime DateFrom
        {
            get { return dateFrom; }
            set
            {
                dateFrom = value;
                OnProperyChanged("DateFrom");
            }
        }

        private DateTime dateUntil;
        public DateTime DateUntil
        {
            get { return dateUntil; }
            set
            {
                dateUntil = value;
                OnProperyChanged("DateUntil");
            }
        }

        private Boolean isUrgent;
        public Boolean IsUrgent
        {
            get { return isUrgent; }
            set
            {
                isUrgent = value;
                OnProperyChanged("IsUrgent");
            }
        }

        public ICommand ConfirmCommand { get; }

        public CreateAbsenceRequestVM()
        {
            ConfirmCommand = new RelayCommand(confirmExecute);
            DoctorWindowVM.setWindowTitle("Create absence request");
            this.errorMessage = "";
            AbsenceRequestRepository absenceRequestRepository = new AbsenceRequestRepository();
            ScheduleService scheduleService = new ScheduleService();
            DoctorRepository doctorRepository = new DoctorRepository();
            AbsenceRequestService absenceRequestService = new AbsenceRequestService(absenceRequestRepository, scheduleService, doctorRepository);
            absenceRequestController = new AbsenceRequestController(absenceRequestService);
        }

        private void confirmExecute(object parametar)
        {
            try
            {
                absenceRequestController.CreateAbsenceRequest(DateFrom, DateUntil, IsUrgent, AbsenceReason);
                DoctorWindowVM.NavigationService.Navigate(new AbsenceRequestsPage());
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
        }

        public void urgentChecked(Boolean isChecked)
        {
            IsUrgent = isChecked;
        }

    }
}
