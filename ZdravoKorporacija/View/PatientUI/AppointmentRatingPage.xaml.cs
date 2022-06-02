using Repository;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.View.PatientUI
{
    /// <summary>
    /// Interaction logic for AppointmentRatingPage.xaml
    /// </summary>
    public partial class AppointmentRatingPage : Page
    {
        private int SelectedDoctorRating { get; set; }
        private int SelectedHospitalRating { get; set; }
        private String Comment { get; set; }

        private GetAllAppointmentsPatient GetAllAppointmentsPatient { get; set; }

        private RatingController ratingController { get; set; }
        public AppointmentRatingPage()
        {
            GetAllAppointmentsPatient = new GetAllAppointmentsPatient();
            RatingRepository ratingRepository = new RatingRepository();
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            RatingService ratingService = new RatingService(ratingRepository, appointmentRepository);
            ratingController = new RatingController(ratingService);
            InitializeComponent();
        }

        private void OcijeniButtonClick(object sender, RoutedEventArgs e)
        {
            if (doctorComboBox.SelectedIndex == 0)
                SelectedDoctorRating = 1;
            if (doctorComboBox.SelectedIndex == 1)
                SelectedDoctorRating = 2;
            if (doctorComboBox.SelectedIndex == 2)
                SelectedDoctorRating = 3;
            if (doctorComboBox.SelectedIndex == 3)
                SelectedDoctorRating = 4;
            if (doctorComboBox.SelectedIndex == 4)
                SelectedDoctorRating = 5;

            if (hospitalComboBox.SelectedIndex == 0)
                SelectedHospitalRating = 1;
            if (hospitalComboBox.SelectedIndex == 1)
                SelectedHospitalRating = 2;
            if (hospitalComboBox.SelectedIndex == 2)
                SelectedHospitalRating = 3;
            if (hospitalComboBox.SelectedIndex == 3)
                SelectedHospitalRating = 4;
            if (hospitalComboBox.SelectedIndex == 4)
                SelectedHospitalRating = 5;



            Comment = commentTextBox.Text;

            if (commentTextBox.Text == "//Unesite komentar... ")
                Comment = "";

            MessageBox.Show("Uspješno ocijenjen pregled! \n ID: "+GetAllAppointmentsPatient.AppointmentToBeRatedId, "USPJEŠNO!", MessageBoxButton.OK, MessageBoxImage.None);
            ratingController.Create(GetAllAppointmentsPatient.AppointmentToBeRatedId, SelectedHospitalRating, SelectedDoctorRating, Comment);
            NavigationService.Navigate(new GetAllAppointmentsPatient());
        }

        private void NazadButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GetAllAppointmentsPatient());
        }

        private void onFocus(object sender, RoutedEventArgs e)
        {
            commentTextBox.Text = "";
        }
    }
}
