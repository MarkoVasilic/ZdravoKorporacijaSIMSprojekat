using Controller;
using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ZdravoKorporacija.View.SecretaryUI.Commands;

namespace ZdravoKorporacija.View.SecretaryUI.ViewModels
{
    internal class PatientsViewVM : INotifyPropertyChanged
    {
        private ObservableCollection<Patient> patientsForTable;
        public PatientController patientController { get; set; }
        public Patient SelectedPatient { get; set; }
        private string patientJmbgFilter { get; set; }
        private string firstNameFilter { get; set; }
        private string lastNameFilter { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Patient> PatientsForTable
        {
            get => patientsForTable;
            set
            {
                patientsForTable = value;
                OnPropertyChanged("PatientsForTable");
            }
        }
        public string PatientJmbgFilter
        {
            get => patientJmbgFilter;
            set
            {
                patientJmbgFilter = value;
                OnPropertyChanged("PatientJmbgFilter");
            }
        }
        public string FirstNameFilter
        {
            get => firstNameFilter;
            set
            {
                firstNameFilter = value;
                OnPropertyChanged("FirstNameFilter");
            }
        }
        public string LastNameFilter
        {
            get => lastNameFilter;
            set
            {
                lastNameFilter = value;
                OnPropertyChanged("LastNameFilter");
            }
        }
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public PatientsViewVM()
        {
            SecretaryWindowVM.setWindowTitle("Patients accounts");
            PatientRepository patientRepository = new PatientRepository();
            PatientService patientService = new PatientService(patientRepository);
            DoctorRepository doctorRepository = new DoctorRepository();
            RoomRepository roomRepository = new RoomRepository();
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            AppointmentService appointmentService = new AppointmentService(appointmentRepository, patientRepository, doctorRepository,
                roomRepository);
            patientController = new PatientController(patientService, appointmentService);
            PatientsForTable = new ObservableCollection<Patient>(patientController.GetAllPatients());
            SelectedPatient = new Patient();
            initializeCommands();
        }


        public ICommand PatientDetailsCommand { get; set; }
        public ICommand AddAccountCommand { get; set; }
        public ICommand SearchPatientCommand { get; set; }

        private void initializeCommands()
        {
            PatientDetailsCommand = new RelayCommand(detailsPatientExecute);
            AddAccountCommand = new RelayCommand(addAccountExecute);
            SearchPatientCommand = new RelayCommand(searchPatientExecute);
        }

        private void detailsPatientExecute(object sender)
        {
            var selected = sender as Patient;
            SecretaryWindowVM.NavigationService.Navigate(new PatientDetailsPage(selected));
        }

        private void addAccountExecute(object sender)
        {
            SecretaryWindowVM.NavigationService.Navigate(new AddNewPatientPage());
        }

        private void searchPatientExecute(object parameter)
        {
            List<Patient> temp = patientController.GetAllPatients();
            PatientsForTable = new ObservableCollection<Patient>();
            foreach (var p in temp)
            {
                Boolean shouldAdd = true;
                if (PatientJmbgFilter != null && PatientJmbgFilter.Length > 0)
                {
                    if (p.Jmbg != PatientJmbgFilter)
                        shouldAdd = false;
                }
                if (FirstNameFilter != null && FirstNameFilter.Length > 0)
                {
                    if (p.FirstName != FirstNameFilter)
                        shouldAdd = false;
                }
                if (LastNameFilter != null && LastNameFilter.Length > 0)
                {
                    if (p.LastName != LastNameFilter)
                        shouldAdd = false;
                }
                if (shouldAdd)
                    PatientsForTable.Add(p);
            }
        }
    }
}
