using Controller;
using Model;
using Repository;
using Service;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ZdravoKorporacija.View.ManagerUI.Help;
using ZdravoKorporacija.View.RoomCRUD;

namespace ZdravoKorporacija.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for GradesPreview.xaml
    /// </summary>
    public partial class GradesPreview : Page
    {

        private DoctorController doctorController;
        public ObservableCollection<Doctor> doctors { get; set; }
        private string selectedDoctorJmbg;

        public GradesPreview()
        {
            InitializeComponent();
            DoctorRepository doctorRepository = new DoctorRepository();
            DoctorService doctorService = new DoctorService(doctorRepository);
            doctorController = new DoctorController(doctorService);

            doctors = new ObservableCollection<Doctor>(doctorController.GetAllDoctors());
            this.DataContext = this;
        }

        private void HospitalRatings_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            HospitalRatings hospitalRatings = new HospitalRatings();
            hospitalRatings.Show();
        }


        private void RatingHelp_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void RatingHelp_Executed(object sender, ExecutedRoutedEventArgs e)
        {
           RatingsHelp ratingsHelp = new RatingsHelp();
            ratingsHelp.Show();
        }

        public void GoBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void GoBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerHomePage());
        }

        private void RadioButtonList_Checked(object sender, RoutedEventArgs e)
        {


            var id = (string)((RadioButton)sender).Tag;
            selectedDoctorJmbg = id; 
        }

        private void DoctorRatings_Click(object sender, RoutedEventArgs e)
        {
            DoctorRatings doctorRatings = new DoctorRatings(selectedDoctorJmbg);
            doctorRatings.Show();
        }
    }
}
