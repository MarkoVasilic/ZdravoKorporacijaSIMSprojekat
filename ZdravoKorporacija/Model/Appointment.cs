using System;

namespace Model
{
    public class Appointment
    {
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public int Id { get; set; }
        public String PatientJmbg { get; set; }
        public String DoctorJmbg { get; set; }
        public int? RoomId { get; set; }

        public Appointment(DateTime startTime, int duration, int id, String patientJmbg, String doctorJmbg, int roomId)
        {
            this.StartTime = startTime;
            this.Duration = duration;
            this.Id = id;
            this.PatientJmbg = patientJmbg;
            this.DoctorJmbg = doctorJmbg;
            this.RoomId = roomId;
        }

        public Appointment() { }
    }
}