using Controller;
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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZdravoKorporacija.DTO;

namespace ZdravoKorporacija.View.PatientUI
{
    /// <summary>
    /// Interaction logic for Calendar.xaml
    /// </summary>
    public partial class Calendar : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<PossibleAppointmentsDTO> appointments { get; set; }

        private  AppointmentController appointmentController;

        public DateTime selectedDate { get; set; }
        public Calendar()
        {
            InitializeComponent();
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            AppointmentService appointmentService = new AppointmentService();
            appointmentController = new AppointmentController(appointmentService);
            Appointments = new ObservableCollection<PossibleAppointmentsDTO>(appointmentController.GetAllFutureAppointmentsByPatient());
           //selectedDate = new DateTime(2022,5,10);
            //appointments = appointmentController.GetAllByJmbgAndDate(selectedDate);
            this.DataContext = this;
            
        }

        public ObservableCollection<PossibleAppointmentsDTO> Appointments
        {
            get { return appointments; }
            set
            {
                appointments = value;
                OnPropertyChanged("Appointments");
            }
        }

        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                OnPropertyChanged("SelectedDate");
            }
        }

        private void GoBackButton(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void pretarziButton(object sender, RoutedEventArgs e)
        {
            SelectedDate = (DateTime)calendarControl.SelectedDate;
            Appointments = new ObservableCollection<PossibleAppointmentsDTO>(appointmentController.GetAllByJmbgAndDate(selectedDate));
            MessageBox.Show("DATUM: " + selectedDate.Date);

        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }


        /*       private void onDateSelectCHANGE(object sender, SelectionChangedEventArgs e)
               {
                   selectedDate = (DateTime)calendarControl.SelectedDate;
                   appointments = (appointmentController.GetAllByJmbgAndDate(selectedDate));
                   MessageBox.Show("DateChange: " + selectedDate.Date);
               }*/
    }
}
