using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void DeleteRating(int ratingId)
        {
            RatingService.Delete(ratingId);
        }

        public Rating GetOneById(int ratingId)
        {
            return RatingService.GetOneById(ratingId);
        }

        public List<Rating> GetRatingsByDoctorJmbg(String doctorJmbg)
        {
            return RatingService.GetRatingsByDoctorJmbg(doctorJmbg);
        }

        public void Create(int appointmentId, int hospitalRating, int doctorRating, String desc)
        {
            RatingService.Create(appointmentId, hospitalRating, doctorRating, desc);
        }

    }
}
