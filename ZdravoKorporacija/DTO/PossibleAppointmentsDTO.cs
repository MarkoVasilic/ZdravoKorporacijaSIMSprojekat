using System;

namespace ZdravoKorporacija.DTO
{
    public class PossibleAppointmentsDTO
    {
        public String patientJmbg;
        public String doctorJmbg;
        public String doctorSpeciality;
        public int roomId;
        public DateTime startTime;
        public int duration;

        public PossibleAppointmentsDTO(string patientJmbg, string doctorJmbg, string doctorSpeciality, int roomId, DateTime startTime, int duration)
        {
            this.patientJmbg = patientJmbg;
            this.doctorJmbg = doctorJmbg;
            this.doctorSpeciality = doctorSpeciality;
            this.roomId = roomId;
            this.startTime = startTime;
            this.duration = duration;
        }
    }
}
