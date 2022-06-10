using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;

namespace ZdravoKorporacija.Service
{
    public class RatingService
    {
        readonly RatingRepository _ratingRepository = new RatingRepository();
        readonly AppointmentRepository _appointmentRepository = new AppointmentRepository();

        public RatingService(RatingRepository ratingRepository, AppointmentRepository AppointmentRepository)
        {
            this._ratingRepository = ratingRepository;
            this._appointmentRepository = AppointmentRepository;
        }

        public RatingService() { }

        public List<Rating> GetAll()
        {
            return _ratingRepository.FindAll();
        }


        private int GenerateNewId()
        {
            try
            {
                List<Rating> ratings = _ratingRepository.FindAll();
                int currentMax = ratings.Max(obj => obj.Id);
                return currentMax + 1;
            }
            catch
            {
                return 1;
            }
        }

        public void Delete(int id)
        {
            if (_ratingRepository.FindOneById(id) == null)
            {
                throw new Exception("Rating with that id doesn't exist!");
            }
            else
            {
                _ratingRepository.Remove(id);
            }
        }


        public Rating? GetOneById(int id)
        {
            return _ratingRepository.FindOneById(id);
        }


        public List<Rating> GetAllByDoctorJmbg(String jmbg)
        {
            return _ratingRepository.FindAllByDoctorJmbg(jmbg);
        }

        public bool FindByAppointmentId(int id)
        {
            return _ratingRepository.FindOneByAppointemntId(id);
        }

        public void Create(int appointmentId, int hospitalRating, int doctorRating, String comment)
        {
            int id = GenerateNewId();
            Appointment appointment = _appointmentRepository.FindOneById(appointmentId);

            Rating rating = new Rating(id, appointment.Id, hospitalRating, doctorRating, comment, System.DateTime.Now, App.loggedUser.Jmbg, appointment.DoctorJmbg);
            _ratingRepository.SaveRating(rating);

        }

        public double GetAverageRatingForDoctor(String doctorJmbg)
        {
            List<Rating> ratings = GetAllByDoctorJmbg(doctorJmbg);
            double ratingSum = 0;
            double ratingsCount = 0;

            if (ratings != null)
            {
                ratingSum = GetRatingsSumForDoctor(ratings);
                ratingsCount = ratings.Count();
            }
            else
            {
                throw new Exception("Doctor with that jmbg has no ratings");
            }

            return Math.Round(ratingSum / ratingsCount, 2);
        }


        public double GetRatingsSumForDoctor(List<Rating> ratings)
        {
            double ratingSum = 0;

            foreach (Rating rating in ratings)
            {
                ratingSum += rating.DoctorRating;
            }

            return ratingSum;
        }



        public double GetAverageRatingForHospital()
        {
            List<Rating> ratings = GetAll();
            double ratingSum = 0;
            double ratingsCount = 0;

            if (ratings != null)
            {
                ratingSum = GetRatingsSumForHospital(ratings);
                ratingsCount = ratings.Count();

            }
            else
            {
                throw new Exception("");
            }


            return Math.Round(ratingSum / ratingsCount, 2);
        }


        public double GetRatingsSumForHospital(List<Rating> ratings)
        {
            double ratingSum = 0;

            foreach (Rating rating in ratings)
            {
                ratingSum += rating.HospitalRating;
            }

            return ratingSum;
        }


        public List<int> CreateListOfHospitalRatings()
        {
            List<Rating> ratings = GetAll(); ;
            List<int> hospitalRatings = new List<int>();

            foreach (Rating rating in ratings)
            {
                hospitalRatings.Add(rating.HospitalRating);
            }

            return hospitalRatings;

        }

        public List<int> GetHistogramOfRatingsForHospital()
        {
            List<int> hospitalRatings = CreateListOfHospitalRatings();
            List<int> histogram = new List<int>(){0, 0, 0, 0, 0};
           

            foreach (int rating in hospitalRatings)
            {
                histogram[rating-1]++;
            }

            return histogram;

        }

        public List<int> CreateListOfDoctorRatings(String doctorJmbg)
        {
            List<Rating> ratings = GetAllByDoctorJmbg(doctorJmbg);
            List<int> doctorRatings = new List<int>();

            foreach (Rating rating in ratings)
            {
                doctorRatings.Add(rating.DoctorRating);
            }

            return doctorRatings;

        }


        public List<int> GetHistogramOfRatingsForDoctor(String doctorJmbg)
        {
            List<int> doctorRatings = CreateListOfDoctorRatings(doctorJmbg);
            List<int> histogram = new List<int>(){ 0, 0, 0, 0, 0 };


            foreach (int rating in doctorRatings)
            {
                histogram[rating - 1]++;
            }

            return histogram;

        }






    }
}
