using System;
using System.Collections.Generic;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.Controller
{
    public class RatingController
    {
        RatingService _ratingService = new RatingService();

        public RatingController(RatingService ratingService)
        {
            this._ratingService = ratingService;
        }

        public List<Rating> GetAll()
        {
            return _ratingService.GetAll();
        }

        public void Delete(int id)
        {
            _ratingService.Delete(id);
        }

        public Rating GetOneById(int id)
        {
            return _ratingService.GetOneById(id);
        }

        public List<Rating> GetAllByDoctorJmbg(String jmbg)
        {
            return _ratingService.GetAllByDoctorJmbg(jmbg);
        }

        public void Create(int appointmentId, int hospitalRating, int doctorRating, String comment)
        {
            _ratingService.Create(appointmentId, hospitalRating, doctorRating, comment);
        }

        public double GetAverageRatingForDoctor(String doctorJmbg)
        {
            return _ratingService.GetAverageRatingForDoctor(doctorJmbg);
        }

        public double GetAverageRatingForHospital()
        {
            return _ratingService.GetAverageRatingForHospital();
        }

        public List<int> GetHistogramOfRatingsForHospital()
        {
            return _ratingService.GetHistogramOfRatingsForHospital();
        }

        public List<int> GetHistogramOfRatingsForDoctor(String doctorJmbg)
        {
            return _ratingService.GetHistogramOfRatingsForDoctor(doctorJmbg);
        }
    }
}
