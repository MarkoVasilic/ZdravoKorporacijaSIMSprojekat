using System;

namespace ZdravoKorporacija.DTO
{
    public class PossibleAppointmentsDTO
    {
        public String PatientJmbg { get; set; }
        public String PatientFullName { get; set; }
        public String DoctorJmbg { get; set; }
        public String DoctorFullName { get; set; }
        public String DoctorSpeciality { get; set; }
        public int RoomId { get; set; }

        public String RoomName { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }

        public int AppointmentId { get; set; }

        public PossibleAppointmentsDTO(string patientJmbg, string patientFullName, string doctorJmbg, string doctorFullName,
            string doctorSpeciality, int roomId, string roomName, DateTime startTime, int duration, int appointmentId)
        {
            PatientJmbg = patientJmbg;
            PatientFullName = patientFullName;
            DoctorJmbg = doctorJmbg;
            DoctorFullName = doctorFullName;
            DoctorSpeciality = doctorSpeciality;
            RoomId = roomId;
            RoomName = roomName;
            StartTime = startTime;
            Duration = duration;
            AppointmentId = appointmentId;
        }
    }
}
