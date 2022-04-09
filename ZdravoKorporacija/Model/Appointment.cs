using System;

namespace Model
{
    public class Appointment
    {
        public DateTime startTime { get; set; }
        public int duration { get; set; }
        public int id { get; set; }

        public String patientJmbg { get; set; }
        public String doctorJmbg { get; set; }
        public int roomId { get; set; }

    }
}