using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using Repository;
using Service;
using ZdravoKorporacija.View.DoctorUI.ViewModel;

namespace ZdravoKorporacija.View.DoctorUI
{
    /// <summary>
    /// Interaction logic for VerificationsPage.xaml
    /// </summary>
    public partial class VerificationsPage : Page, INotifyPropertyChanged
    {
        private MedicationController medicationController;
        public ObservableCollection<Medication> medications { get; set; }
        public ObservableCollection<Medication> Medications
        {
            get { return medications; }
            set
            {
                medications = value;
                OnPropertyChanged("Medications");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }

        }

        public VerificationsPage()
        {
            InitializeComponent();
            this.DataContext = this;
            DoctorWindowVM.setWindowTitle("Verifications");
            MedicationRepository medicationRepository = new MedicationRepository();
            MedicationService medicationService = new MedicationService(medicationRepository);
            medicationController = new MedicationController(medicationService);
            this.medications = new ObservableCollection<Medication>(medicationController.GetAllUnverified());
        }
        private void VerifyMedication(object sender, RoutedEventArgs e)
        {
            int Id = (int)((Button)sender).CommandParameter;
            medicationController.Verify(Id);
            NavigationService.Navigate(new VerificationsPage());

        }

        private void RejectMedication(object sender, RoutedEventArgs e)
        {
            int Id = (int)((Button)sender).CommandParameter;
            NavigationService.Navigate(new RejectMedicationPage(Id));

        }

    }
}
