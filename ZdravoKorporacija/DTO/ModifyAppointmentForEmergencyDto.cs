using System;

namespace ZdravoKorporacija.DTO
{
    public class ModifyAppointmentForEmergencyDto
    {
        public String PatientJmbg { get; set; }
        public String PatientFullName { get; set; }
        public String DoctorJmbg { get; set; }
        public String DoctorFullName { get; set; }
        public String DoctorSpeciality { get; set; }
        public int RoomId { get; set; }
        public String RoomName { get; set; }
        public DateTime OldStartTime { get; set; }
        public DateTime NewStartTime { get; set; }
        public int Duration { get; set; }

        public int AppointmentId { get; set; }

        public ModifyAppointmentForEmergencyDto(string patientJmbg, string patientFullName, string doctorJmbg, string doctorFullName,
            string doctorSpeciality, int roomId, string roomName, DateTime oldStartTime, DateTime newStartTime, int duration, int appointmentId)
        {
            PatientJmbg = patientJmbg;
            PatientFullName = patientFullName;
            DoctorJmbg = doctorJmbg;
            DoctorFullName = doctorFullName;
            DoctorSpeciality = doctorSpeciality;
            RoomId = roomId;
            RoomName = roomName;
            OldStartTime = oldStartTime;
            NewStartTime = newStartTime;
            Duration = duration;
            AppointmentId = appointmentId;
        }
    }
}
