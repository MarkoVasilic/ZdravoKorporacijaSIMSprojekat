using System;

namespace ZdravoKorporacija.Model
{
    public class Rating
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public int HospitalRating { get; set; }
        public int DoctorRating { get; set; }
        public String? Comment { get; set; }
        public DateTime RatingDate { get; set; }

        public String DoctorId { get; set; }
        public String PatientId { get; set; }

        public Rating(int id, int appointmentId, int hospitalRating, int doctorRating, string? comment, DateTime dateTime, String patientId, String doctorId)
        {
            this.Id = id;
            this.AppointmentId = appointmentId;
            this.HospitalRating = hospitalRating;
            this.DoctorRating = doctorRating;
            this.Comment = comment;
            this.RatingDate = dateTime;
            this.PatientId = patientId;
            this.DoctorId = doctorId;
        }

        public Rating() { }
    }
}
