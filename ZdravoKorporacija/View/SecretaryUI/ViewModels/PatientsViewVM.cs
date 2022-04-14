using Controller;
using Model;
using Repository;
using Service;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ZdravoKorporacija.View.SecretaryUI.Commands;

namespace ZdravoKorporacija.View.SecretaryUI.ViewModels
{
    internal class PatientsViewVM
    {
        private ObservableCollection<Patient> _patientsForTable;
        public ObservableCollection<Patient> PatientsForTable { get => _patientsForTable; set => _patientsForTable = value; }
        public PatientController patientController { get; set; }
        public Patient SelectedPatient { get; set; }

        public PatientsViewVM()
        {
            PatientRepository patientRepository = new PatientRepository();
            PatientService patientService = new PatientService(patientRepository);
            patientController = new PatientController(patientService);
            SelectedPatient = new Patient();
            PatientsForTable = new ObservableCollection<Patient>(patientController.GetAllPatients());
            initializeCommands();
        }


        public ICommand PatientDetailsCommand { get; set; }
        public ICommand AddAccountCommand { get; set; }

        private void initializeCommands()
        {
            PatientDetailsCommand = new RelayCommand(detailsPatientExecute);
            AddAccountCommand = new RelayCommand(addAccountExecute);
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
    }
}
