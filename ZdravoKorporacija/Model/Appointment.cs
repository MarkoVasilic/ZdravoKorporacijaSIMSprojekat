using System;

namespace Model
{
    public class Appointment
    {
        public DateTime startTime { get; set; }
        public int duration { get; set; }
        public int id { get; set; }

        public Patient patient {get; set;}
        public Doctor doctor; 


        public Doctor Doctor
        {
            get
            {
                return doctor;
            }
            set
            {
                if (this.doctor == null || !this.doctor.Equals(value))
                {
                    if (this.doctor != null)
                    {
                        Doctor oldDoctor = this.doctor;
                        this.doctor = null;
                        oldDoctor.RemoveAppointment(this);
                    }
                    if (value != null)
                    {
                        this.doctor = value;
                        this.doctor.AddAppointment(this);
                    }
                }
            }
        }

    }
}