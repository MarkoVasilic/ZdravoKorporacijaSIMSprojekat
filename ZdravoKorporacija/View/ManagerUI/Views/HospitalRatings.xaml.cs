using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.View.ManagerUI.Views
{
    /// <summary>
    /// Interaction logic for HospitalRatings.xaml
    /// </summary>
    public partial class HospitalRatings : Window
    {

        private double averageHospitalRating;
        private int fives;
        private int fours;
        private int threes;
        private int twos;
        private int ones;
        public List<int> hospitalRatings { get; set; }
        private RatingController ratingController;


        public double AverageHospitalRating
        {
            get { return averageHospitalRating; }
            set { averageHospitalRating = value; }
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
            


        public HospitalRatings()
        {
            InitializeComponent();
            RatingRepository ratingRepository = new RatingRepository();
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            RatingService ratingService = new RatingService(ratingRepository, appointmentRepository);
            ratingController = new RatingController(ratingService);

            AverageHospitalRating = ratingController.GetAverageRatingForHospital();
            hospitalRatings = ratingController.GetHistogramOfRatingsForHospital();
            Ones = hospitalRatings[0];
            Twos = hospitalRatings[1];
            Threes = hospitalRatings[2];
            Fours = hospitalRatings[3];
            Fives = hospitalRatings[4];
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
