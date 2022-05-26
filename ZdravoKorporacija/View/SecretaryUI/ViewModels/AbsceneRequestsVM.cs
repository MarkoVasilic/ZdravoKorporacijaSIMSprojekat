﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Controller;
using Model;
using Repository;
using Service;
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;
using ZdravoKorporacija.View.SecretaryUI.Commands;
using ZdravoKorporacija.View.SecretaryUI.DTO;

namespace ZdravoKorporacija.View.SecretaryUI.ViewModels
{
    public class AbsceneRequestsVM: INotifyPropertyChanged
    {
        public DoctorController doctorController { get; set; }
        public AbsenceRequestController absenceRequestController{ get; set; }
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
            AbsenceRequestService absenceRequestService = new AbsenceRequestService(absenceRequestRepository, doctorRepository);
            NotificationRepository notificationRepository = new NotificationRepository();
            PrescriptionRepository prescriptionRepository = new PrescriptionRepository();
            MedicalRecordRepository medicalRecordRepository = new MedicalRecordRepository();
            MedicationRepository medicationRepository = new MedicationRepository();
            PatientRepository patientRepository = new PatientRepository();
            PrescriptionService prescriptionService = new PrescriptionService(prescriptionRepository,
                medicalRecordRepository, patientRepository, medicationRepository);
            NotificationService notificationService =
                new NotificationService(notificationRepository, prescriptionService);
            notificationController = new NotificationController(notificationService);
            absenceRequestController = new AbsenceRequestController(absenceRequestService);
            absenceRequestToDto(absenceRequestController.GetOnHoldAbsceneRequests());
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
                Doctor doctor = doctorController.GetOneDoctor(ar.DoctorJmbg);
                String urgent = "";
                if (ar.isUgent)
                    urgent = "Yes";
                else
                    urgent = "No";
                AbsceneRequestDetailsDtos.Add(new AbsceneRequestDetailsDto(ar.Id, ar.DoctorJmbg, doctor.FirstName, doctor.LastName, doctor.SpecialtyType,
                    ar.DateFrom, ar.DateTo, urgent, ar.Reason, ""));
            }
        }

        private void acceptAbsenceExecute(object parameter)
        {
            AbsceneRequestDetailsDto absceneRequestDetailsDto = parameter as AbsceneRequestDetailsDto;
            try
            {
                absenceRequestController.ChangeAbsceneRequestState(absceneRequestDetailsDto.Id, AbsenceRequestState.ACCEPTED);
                notificationController.CreateUserNotification("Absence request", "Your absence request has been accepted!",
                    absceneRequestDetailsDto.DoctorJmbg);
                absenceRequestToDto(absenceRequestController.GetOnHoldAbsceneRequests());
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
                absenceRequestController.ChangeAbsceneRequestState(absceneRequestDetailsDto.Id, AbsenceRequestState.REJECTED);
                notificationController.CreateUserNotification("Absence request", "Your absence request has been declined!\nReason: " + absceneRequestDetailsDto.ReturnMessage,
                    absceneRequestDetailsDto.DoctorJmbg);
                absenceRequestToDto(absenceRequestController.GetOnHoldAbsceneRequests());
                ErrorMessageChangeState = "";
            }
            catch (Exception e)
            {
                ErrorMessageChangeState = e.Message;
            }
        }
    }
}