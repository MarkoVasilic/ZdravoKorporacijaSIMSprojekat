using System;

namespace Model
{
    public class Appointment
    {
        public DateTime startTime;
        public int duration;
        public int id;

        public Patient? patient;
        public Doctor? doctor;

    }
}