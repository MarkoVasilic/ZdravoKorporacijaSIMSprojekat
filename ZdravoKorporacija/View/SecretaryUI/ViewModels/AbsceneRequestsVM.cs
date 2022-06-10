using Controller;
using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.View.SecretaryUI.Commands;
using ZdravoKorporacija.View.SecretaryUI.DTO;

namespace ZdravoKorporacija.View.SecretaryUI.ViewModels
{
    public class AbsceneRequestsVM : INotifyPropertyChanged
    {
        public DoctorController doctorController { get; set; }
        public AbsenceRequestController absenceRequestController { get; set; }
        public NotificationController notificationController { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<AbsceneRequestDetailsDto> absceneRequestDetailsDtos;
        private String errorMessageChangeState;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public ObservableCollection<AbsceneRequestDetailsDto> AbsceneRequestDetailsDtos
        {
            get => absceneRequestDetailsDtos;
            set
            {
                absceneRequestDetailsDtos = value;
                OnPropertyChanged("AbsceneRequestDetailsDtos");
            }
        }
        public string ErrorMessageChangeState
        {
            get => errorMessageChangeState;
            set
            {
                errorMessageChangeState = value;
                OnPropertyChanged("ErrorMessageChangeState");
            }
        }

        public AbsceneRequestsVM()
        {
            SecretaryWindowVM.setWindowTitle("Absence requests");
            ErrorMessageChangeState = "";
            DoctorRepository doctorRepository = new DoctorRepository();
            DoctorService doctorService = new DoctorService(doctorRepository);
            doctorController = new DoctorController(doctorService);
            AbsenceRequestRepository absenceRequestRepository = new AbsenceRequestRepository();
            ManagerRepository managerRepository = new ManagerRepository();
            RoomRepository roomRepository = new RoomRepository();
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            PatientRepository patientRepository = new PatientRepository();
            SecretaryRepository secretaryRepository = new SecretaryRepository();
            MeetingRepository meetingRepository = new MeetingRepository();
            AdvancedRenovationJoiningRepository advancedRenovationJoining = new AdvancedRenovationJoiningRepository();
            AdvancedRenovationSeparationRepository advancedRenovationSeparation =
                new AdvancedRenovationSeparationRepository();
            BasicRenovationRepository basicRenovationRepository = new BasicRenovationRepository();
            ScheduleService scheduleService = new ScheduleService(appointmentRepository, patientRepository,
                doctorRepository, roomRepository, basicRenovationRepository, advancedRenovationJoining,
                advancedRenovationSeparation, managerRepository, secretaryRepository, meetingRepository);
            AbsenceRequestService absenceRequestService = new AbsenceRequestService(absenceRequestRepository, scheduleService, doctorRepository, appointmentRepository);
            NotificationRepository notificationRepository = new NotificationRepository();
            PrescriptionRepository prescriptionRepository = new PrescriptionRepository();
            MedicalRecordRepository medicalRecordRepository = new MedicalRecordRepository();
            MedicationRepository medicationRepository = new MedicationRepository();
            PrescriptionService prescriptionService = new PrescriptionService(prescriptionRepository,
                medicalRecordRepository, patientRepository, medicationRepository);
            NotificationService notificationService =
                new NotificationService(notificationRepository, prescriptionService);
            notificationController = new NotificationController(notificationService);
            absenceRequestController = new AbsenceRequestController(absenceRequestService);
            absenceRequestToDto(absenceRequestController.GetOnHold());
            AcceptAbsenceCommand = new RelayCommand(acceptAbsenceExecute);
            RefuseAbsenceCommand = new RelayCommand(refuseAbsenceExecute);
        }

        public ICommand AcceptAbsenceCommand { get; set; }
        public ICommand RefuseAbsenceCommand { get; set; }

        public void absenceRequestToDto(List<AbsenceRequest> absenceRequests)
        {
            AbsceneRequestDetailsDtos = new ObservableCollection<AbsceneRequestDetailsDto>();
            foreach (var ar in absenceRequests)
            {
                if (ar.DateFrom > DateTime.Now)
                {
                    Doctor doctor = doctorController.GetOneDoctor(ar.DoctorJmbg);
                    String urgent = "";
                    if (ar.isUrgent)
                        urgent = "Yes";
                    else
                        urgent = "No";
                    AbsceneRequestDetailsDtos.Add(new AbsceneRequestDetailsDto(ar.Id, ar.DoctorJmbg, doctor.FirstName,
                        doctor.LastName, doctor.SpecialtyType,
                        ar.DateFrom, ar.DateTo, urgent, ar.Reason, ""));
                }
            }
        }

        private void acceptAbsenceExecute(object parameter)
        {
            AbsceneRequestDetailsDto absceneRequestDetailsDto = parameter as AbsceneRequestDetailsDto;
            try
            {
                absenceRequestController.ChangeState(absceneRequestDetailsDto.Id, AbsenceRequestState.ACCEPTED, absceneRequestDetailsDto.ReturnMessage);
                notificationController.CreateUserNotification("Absence request", "Your absence request has been accepted!",
                    absceneRequestDetailsDto.DoctorJmbg);
                absenceRequestToDto(absenceRequestController.GetOnHold());
                ErrorMessageChangeState = "";
            }
            catch (Exception e)
            {
                ErrorMessageChangeState = e.Message;
            }
        }

        private void refuseAbsenceExecute(object parameter)
        {
            AbsceneRequestDetailsDto absceneRequestDetailsDto = parameter as AbsceneRequestDetailsDto;
            try
            {
                absenceRequestController.ChangeState(absceneRequestDetailsDto.Id, AbsenceRequestState.REJECTED, absceneRequestDetailsDto.ReturnMessage);
                notificationController.CreateUserNotification("Absence request", "Your absence request has been declined!\nReason: " + absceneRequestDetailsDto.ReturnMessage,
                    absceneRequestDetailsDto.DoctorJmbg);
                absenceRequestToDto(absenceRequestController.GetOnHold());
                ErrorMessageChangeState = "";
            }
            catch (Exception e)
            {
                ErrorMessageChangeState = e.Message;
            }
        }
    }
}
