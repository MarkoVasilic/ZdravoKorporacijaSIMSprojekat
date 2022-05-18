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

        public List<Rating> GetAllRatings()
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

        public void DeleteRating(int ratingId)
        {
            if (RatingRepository.FindOneById(ratingId) == null)
            {
                throw new Exception("Rating with that id doesn't exist!");
            }
            else
            {
                RatingRepository.RemoveRating(ratingId);
            }
        }

        /* public void ModifyRating(int ratingId, DateTime newDate)
         {

             var oneAppointment = AppointmentRepository.FindOneById(appointmentId);
             if (oneAppointment == null)
             {
                 throw new Exception("Appointment with that id doesn't exist!");
             }
             else
             {
                 Appointment newAppointment = new Appointment(newDate, oneAppointment.Duration, oneAppointment.Id, oneAppointment.PatientJmbg,
                     oneAppointment.DoctorJmbg, oneAppointment.RoomId);
                 if (!newAppointment.validateAppointment())
                 {
                     throw new Exception("Something went wrong, new appointment isn't modified!");
                 }
                 AppointmentRepository.UpdateAppointment(newAppointment);
             }

         } */


        public Rating? GetOneById(int ratingId)
        {
            return RatingRepository.FindOneById(ratingId);
        }


        public List<Rating> GetRatingsByDoctorJmbg(String doctorJmbg)
        {
            return RatingRepository.FindAllByDoctorJmbg(doctorJmbg);
        }

        public void CreateRatingByPatient(int appointmentId,int hospitalRating,int doctorRating,String desc)
        {
            int id = GenerateNewId();
            Appointment appointment = AppointmentRepository.FindOneById(appointmentId);

            Rating rating = new Rating(id, appointment.Id, hospitalRating, doctorRating, desc, System.DateTime.Now, App.loggedUser.Jmbg, appointment.DoctorJmbg);
            RatingRepository.SaveRating(rating);
            
            //izmjeniti VALIDACIJU
          /*  if (!appointment.validateAppointment())
            {
                throw new Exception("Something went wrong, new appointment isn't created!");
            }
          */
        }

    }
}
