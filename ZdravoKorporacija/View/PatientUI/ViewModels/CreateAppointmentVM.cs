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
using System.Windows.Input;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.View.AppointmentCRUD.Commands;

namespace ZdravoKorporacija.View.AppointmentCRUD.ViewModels
{
    public class CreateAppointmentVM : INotifyPropertyChanged
    {
        public DoctorController doctorController { get; set; }
        public AppointmentController appointmentController { get; set; }
        private Patient? selectedPatient { get; set; }
        private Doctor? selectedDoctor { get; set; }
        private String selectedPriority { get; set; }

        private String selectedSpeciality;

        private PossibleAppointmentsDTO selectedAppointment;
        private DateTime dateFrom;
        private DateTime dateUntil;
        private ObservableCollection<Doctor> doctors;
        private ObservableCollection<PossibleAppointmentsDTO> possibleAppointments;
        private ObservableCollection<PossibleAppointmentsDTO> futureAppointments;
        private String errorMessage;
        private String errorMessagePossibleAppointments;
        private String errorMessageConfirmAppointment;


        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand GetAllPossibleAppointmentsPatient { get; set; }

        public ICommand GetAllFutureAppointmentsPatient { get; set; }
        public ICommand SelectedAppointmentCommand { get; set; }

        public ICommand GoBackCommand { get; set; }



        public ObservableCollection<Doctor> Doctors
        {
            get => doctors;
            set
            {
                doctors = value;
                OnPropertyChanged("Doctors");
            }
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public ObservableCollection<PossibleAppointmentsDTO> PossibleAppointments
        {
            get => possibleAppointments;
            set
            {
                possibleAppointments = value;
                OnPropertyChanged("PossibleAppointments");
            }
        }

        public ObservableCollection<PossibleAppointmentsDTO> FutureAppointments
        {
            get => futureAppointments;
            set
            {
                futureAppointments = value;
                OnPropertyChanged("FutureAppointments");
            }
        }

        public DateTime DateFrom
        {
            get { return dateFrom; }
            set
            {
                dateFrom = value;
                OnPropertyChanged("DateFrom");
            }
        }
        public DateTime DateUntil
        {
            get { return dateUntil; }
            set
            {
                dateUntil = value;
                OnPropertyChanged("DateUntil");
            }
        }
        public String SelectedPriority
        {
            get { return selectedPriority; }
            set
            {
                selectedPriority = value;
                OnPropertyChanged("SelectedPriority");
            }


        }

        public PossibleAppointmentsDTO SelectedAppointment
        {
            get { return selectedAppointment; }
            set
            {
                selectedAppointment = value;
                OnPropertyChanged("SelectedAppointment");
            }


        }

        public Doctor SelectedDoctor
        {
            get { return selectedDoctor; }
            set
            {
                selectedDoctor = value;
                OnPropertyChanged("SelectedDoctor");
            }
        }

        public string ErrorMessagePossibleAppointments
        {
            get => errorMessagePossibleAppointments;
            set
            {
                errorMessagePossibleAppointments = value;
                OnPropertyChanged("ErrorMessagePossibleAppointments");
            }
        }
        public string ErrorMessageConfirmAppointment
        {
            get => errorMessageConfirmAppointment;
            set
            {
                errorMessageConfirmAppointment = value;
                OnPropertyChanged("ErrorMessageConfirmAppointment");
            }
        }




        public CreateAppointmentVM()
        {
            DoctorRepository doctorRepository = new DoctorRepository();
            DoctorService doctorService = new DoctorService(doctorRepository);
            doctorController = new DoctorController(doctorService);


            AppointmentRepository appointmentRepository = new AppointmentRepository();
            AppointmentService appointmentService = new AppointmentService();
            appointmentController = new AppointmentController(appointmentService);
            doctorsListToDoctorList(doctorController.getAllBySpeciality("Physician"));

            GetAllPossibleAppointmentsPatient = new RelayCommand(possibleAppointmentsPatientExecute);
            GetAllFutureAppointmentsPatient = new RelayCommand(futureAppointmentsPatientExecute); 

        }


        public void SelectAppointment()
        {
            appointmentController.CreateAppointmentByPatient(selectedAppointment.StartTime, selectedAppointment.DoctorJmbg);
            MessageBox.Show("Uspjesno zakazan pregled za " + selectedAppointment.StartTime);
            PatientWindowVM.NavigationService.Navigate(new GetAllAppointmentsPatient());
        }

        public void SelectFutureAppointment()
        {
            
            appointmentController.CreateAppointmentByPatient(selectedAppointment.StartTime, selectedAppointment.DoctorJmbg);
            MessageBox.Show("Uspjesno zakazan pregled za " + selectedAppointment.StartTime);
            PatientWindowVM.NavigationService.Navigate(new GetAllAppointmentsPatient());
        }

        private void possibleAppointmentsPatientExecute(object sender)
        {
            appointmentListToAppointmentList(appointmentController.GetPossibleAppointmentsBySecretary(App.loggedUser.Jmbg, SelectedDoctor.Jmbg, SelectedDoctor.RoomId,
                        DateFrom, DateUntil, 45, SelectedPriority));
            PatientWindowVM.NavigationService.Navigate(new PossibleAppointmentPatientPage(this));
        }


        private void futureAppointmentsPatientExecute(object sender)
        {
            Console.WriteLine("Usao u futureAppointmentsPatientExecute");
            futureAppointmentListToAppointmentList(appointmentController.GetAllFutureAppointmentsByPatient());
            PatientWindowVM.NavigationService.Navigate(new UpdateFutureAppointmentsPage());
        }


        private void doctorsListToDoctorList(List<Doctor> doctors)
        {
            Doctors = new ObservableCollection<Doctor>();
            foreach (var d in doctors)
            {
                d.FirstName = d.FirstName + " " + d.LastName;
                Doctors.Add(d);
            }
        }

        private void appointmentListToAppointmentList(List<PossibleAppointmentsDTO> appointmentsDTOs)
        {
            PossibleAppointments = new ObservableCollection<PossibleAppointmentsDTO>();
            foreach (var pad in appointmentsDTOs)
            {
                PossibleAppointments.Add(pad);
            }
        }

        private void futureAppointmentListToAppointmentList(List<PossibleAppointmentsDTO> appointmentsDTOs)
        {
            Console.WriteLine("Usao u futureAppointmentListToAppointmentList");
            FutureAppointments = new ObservableCollection<PossibleAppointmentsDTO>();
            foreach (var pad in appointmentsDTOs)
            {
                FutureAppointments.Add(pad);
            }
        }

        private void searchAppointmentExecute(object parameter)
        {
            if (SelectedDoctor == null || DateFrom.Year == 1 || DateUntil.Year == 1)
            {
                ErrorMessagePossibleAppointments = "All fields are necessary!";
            }
            else
            {
                ErrorMessagePossibleAppointments = "";
                try
                {
                    appointmentListToAppointmentList(appointmentController.GetPossibleAppointmentsBySecretary(App.loggedUser.Jmbg, SelectedDoctor.Jmbg, 1,
                        DateFrom, DateUntil, 45, SelectedPriority));
                    ErrorMessagePossibleAppointments = "";
                }
                catch (Exception e)
                {
                    ErrorMessagePossibleAppointments = e.Message;
                }
            }
        }

        private void searchFutureAppointmentExecute(object parameter)
        {
            Console.WriteLine("Usao u searchFutureAppointmentExecute");
            if (SelectedDoctor == null || DateFrom.Year == 1 || DateUntil.Year == 1)
            {
                ErrorMessagePossibleAppointments = "All fields are necessary!";
            }
            else
            {
                ErrorMessagePossibleAppointments = "";
                try
                {
                    futureAppointmentListToAppointmentList(appointmentController.GetAllFutureAppointmentsByPatient());
                    ErrorMessagePossibleAppointments = "";
                }
                catch (Exception e)
                {
                    ErrorMessagePossibleAppointments = e.Message;
                }
            }
        }
        private void selectAppointmentExecute(object parameter)
        {
            selectedAppointment = parameter as PossibleAppointmentsDTO;
          //  SecretaryWindowVM.NavigationService.Navigate(new ConfirmAppointmentInformations(this));
        }
        private void confirmAppointmentExecute(object parameter)
        {
            try
            {
                appointmentController.CreateAppointmentByPatient(DateFrom, selectedDoctor.Jmbg);

            //    SecretaryWindowVM.NavigationService.Navigate(new SecretaryHomePage());
            }
            catch (Exception e)
            {
                ErrorMessageConfirmAppointment = e.Message;
            }

        }

        private void selectFutureAppointmentExecute(object parameter)
        {
            Console.WriteLine("Usao u selectFutureAppointmentExecute");
            selectedAppointment = parameter as PossibleAppointmentsDTO;
            //  SecretaryWindowVM.NavigationService.Navigate(new ConfirmAppointmentInformations(this));
        }
        private void confirmFutureAppointmentExecute(object parameter)
        {
            try
            {
                Console.WriteLine("Usao u confirmFutureAppointmentExecute");
                appointmentController.CreateAppointmentByPatient(DateFrom, selectedDoctor.Jmbg);

                //    SecretaryWindowVM.NavigationService.Navigate(new SecretaryHomePage());
            }
            catch (Exception e)
            {
                ErrorMessageConfirmAppointment = e.Message;
            }

        }


    }
}
