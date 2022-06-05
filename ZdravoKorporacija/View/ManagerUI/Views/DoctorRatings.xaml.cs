using Repository;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for DoctorRatings.xaml
    /// </summary>
    public partial class DoctorRatings : Window
    {

        private double averageDoctorRating;
        private int fives;
        private int fours;
        private int threes;
        private int twos;
        private int ones;
        public List<int> doctorRatings { get; set; }
        private RatingController ratingController;

        public double AverageDoctorRating
        {
            get { return averageDoctorRating; }
            set { averageDoctorRating = value; }
        }

        public int Fives
        {
            get { return fives; }
            set { fives = value; }
        }

        public int Fours
        {
            get { return fours; }
            set { fours = value; }
        }

        public int Threes
        {
            get { return threes; }
            set { threes = value; }
        }


        public int Twos
        {
            get { return twos; }
            set { twos = value; }
        }

        public int Ones
        {
            get { return ones; }
            set { ones = value; }
        }


        public DoctorRatings(String doctorJmbg)
        {
            InitializeComponent();
            this.DataContext = this;
            RatingRepository ratingRepository = new RatingRepository();
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            RatingService ratingService = new RatingService(ratingRepository, appointmentRepository);
            ratingController = new RatingController(ratingService);

            AverageDoctorRating = ratingController.GetAverageRatingForDoctor(doctorJmbg);
            doctorRatings = ratingController.GetHistogramOfRatingsForDoctor(doctorJmbg);
            Ones = doctorRatings[0];
            Twos = doctorRatings[1];
            Threes = doctorRatings[2];
            Fours = doctorRatings[3];
            Fives = doctorRatings[4];
            this.DataContext = this;
        }


        public void GoBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public void GoBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
    }
}
