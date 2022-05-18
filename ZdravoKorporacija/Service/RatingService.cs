using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;

namespace ZdravoKorporacija.Service
{
    public class RatingService
    {
        readonly RatingRepository RatingRepository = new RatingRepository();
        readonly AppointmentRepository AppointmentRepository = new AppointmentRepository();

        public RatingService(RatingRepository ratingRepository, AppointmentRepository AppointmentRepository)
        {
            this.RatingRepository = ratingRepository;
            this.AppointmentRepository = AppointmentRepository;
        }

        public RatingService() { }

        public List<Rating> GetAll()
        {
            return RatingRepository.FindAll();
        }


        private int GenerateNewId()
        {
            try
            {
                List<Rating> ratings = RatingRepository.FindAll();
                int currentMax = ratings.Max(obj => obj.Id);
                return currentMax + 1;
            }
            catch
            {
                return 1;
            }
        }

        public void Delete(int ratingId)
        {
            if (RatingRepository.FindOneById(ratingId) == null)
            {
                throw new Exception("Rating with that id doesn't exist!");
            }
            else
            {
                RatingRepository.Remove(ratingId);
            }
        }


        public Rating? GetOneById(int ratingId)
        {
            return RatingRepository.FindOneById(ratingId);
        }


        public List<Rating> GetRatingsByDoctorJmbg(String doctorJmbg)
        {
            return RatingRepository.FindAllRatingsByDoctorJmbg(doctorJmbg);
        }

        public void Create(int appointmentId,int hospitalRating,int doctorRating,String desc)
        {
            int id = GenerateNewId();
            Appointment appointment = AppointmentRepository.FindOneById(appointmentId);
            
            Rating rating = new Rating(id, appointment.Id, hospitalRating, doctorRating, desc, System.DateTime.Now, "7778889994445", appointment.DoctorJmbg);
            RatingRepository.SaveRating(rating);
            
        }

    }
}
