using Controller;
using Repository;
using Service;
using System;
using System.ComponentModel;
using System.Windows.Input;
using ZdravoKorporacija.View.SecretaryUI.Commands;

namespace ZdravoKorporacija.View.SecretaryUI.ViewModels
{
    internal class ScheduleEmergencyVM : INotifyPropertyChanged
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Jmbg { get; set; }
        public PatientController patientController;
        public event PropertyChangedEventHandler PropertyChanged;

        private string errorMessage;

        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public ScheduleEmergencyVM()
        {
            PatientRepository patientRepository = new PatientRepository();
            PatientService patientService = new PatientService(patientRepository);
            patientController = new PatientController(patientService);
            CreateGuestAccountCommand = new RelayCommand(createGuestExecute);

        }
        public ICommand CreateGuestAccountCommand { get; set; }
        private void createGuestExecute(object parameter)
        {
            try
            {
                patientController.CreateGuestAccount(FirstName, LastName, Jmbg);
                SecretaryWindowVM.NavigationService.Navigate(new PatientsView());
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }

        }
    }
}
