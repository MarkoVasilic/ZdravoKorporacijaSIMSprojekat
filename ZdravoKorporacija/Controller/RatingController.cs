using System;
using System.Collections.Generic;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.Controller
{
    public class RatingController
    {
        RatingService RatingService = new RatingService();

        public RatingController(RatingService ratingService)
        {
            this.RatingService = ratingService;
        }

        public List<Rating> GetAll()
        {
            return RatingService.GetAll();
        }

        public void Delete(int id)
        {
            RatingService.Delete(id);
        }

        public Rating GetOneById(int id)
        {
            return RatingService.GetOneById(id);
        }

        public List<Rating> GetAllByDoctorJmbg(String jmbg)
        {
            return RatingService.GetAllByDoctorJmbg(jmbg);
        }

        public void Create(int appointmentId, int hospitalRating, int doctorRating, String comment)
        {
            RatingService.Create(appointmentId, hospitalRating, doctorRating, comment);
        }

        public double GetAverageRatingForDoctor(String doctorJmbg)
        {
            return RatingService.GetAverageRatingForDoctor(doctorJmbg);
        }

        public double GetAverageRatingForHospital()
        {
            return RatingService.GetAverageRatingForHospital();
        }

        public List<int> GetHistogramOfRatingsForHospital()
        {
            return RatingService.GetHistogramOfRatingsForHospital();
        }

        public List<int> GetHistogramOfRatingsForDoctor(String doctorJmbg)
        {
            return RatingService.GetHistogramOfRatingsForDoctor(doctorJmbg);
        }
    }
}
