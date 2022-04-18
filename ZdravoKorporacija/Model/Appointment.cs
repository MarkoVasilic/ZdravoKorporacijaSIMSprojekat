using System;
using System.Text.RegularExpressions;

namespace Model
{
    public class Appointment
    {
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public int Id { get; set; }
        public String PatientJmbg { get; set; }
        public String DoctorJmbg { get; set; }
        public int RoomId { get; set; }

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

        public Boolean validateAppointment()
        {
            Regex onlyNumberRegex = new Regex("^[0-9]+$");
            if (StartTime == null || StartTime < DateTime.Now)
                return false;
            else if (Duration < 1 || !onlyNumberRegex.IsMatch(Duration.ToString()))
                return false;
            else if (Id == null || !onlyNumberRegex.IsMatch(Id.ToString()))
                return false;
            else if (PatientJmbg == null || PatientJmbg.Length != 13 || !onlyNumberRegex.IsMatch(PatientJmbg))
                return false;
            else if (DoctorJmbg == null || DoctorJmbg.Length != 13 || !onlyNumberRegex.IsMatch(DoctorJmbg))
                return false;
            else if (RoomId == null || !onlyNumberRegex.IsMatch(RoomId.ToString()))
                return false;
            else
                return true;

        }

        public void toString()
        {
            Console.WriteLine("ID = " + Id);
            Console.WriteLine("PatientJMBG = " + PatientJmbg);
            Console.WriteLine("DoctorJmbg = " + DoctorJmbg);
            Console.WriteLine("StartTime = " + StartTime);
            Console.WriteLine("RoomId = " + RoomId);
            Console.WriteLine("Duration = " + Duration);
            Console.WriteLine();
        }
    }
}