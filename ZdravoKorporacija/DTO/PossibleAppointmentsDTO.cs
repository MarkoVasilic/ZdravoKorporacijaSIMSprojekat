﻿using System;

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

        public PossibleAppointmentsDTO() { }

        public void toStringManager()
        {
            Console.WriteLine("RoomID = " + RoomId);
            Console.WriteLine("RoomName = " + RoomName);
            Console.WriteLine("StartTime = " + StartTime);
            Console.WriteLine("Duration = " + Duration);
            Console.WriteLine();
        }
        public void ToStringPossible()
        {
            Console.WriteLine("Doctor = " + DoctorFullName);
            Console.WriteLine("Doctor Specialty = " + DoctorSpeciality);
            Console.WriteLine("StartTime = " + StartTime);
            Console.WriteLine("Duration = " + Duration);
            Console.WriteLine();
        }
        public void ToStringChoosen()
        {
            Console.WriteLine("Patient = " + PatientFullName);
            Console.WriteLine("Doctor Jmbg = " + DoctorJmbg);
            Console.WriteLine("Doctor = " + DoctorFullName);
            Console.WriteLine("Doctor Specialty = " + DoctorSpeciality);
            Console.WriteLine("RoomID = " + RoomId);
            Console.WriteLine("Room Name = " + RoomName);
            Console.WriteLine("StartTime = " + StartTime);
            Console.WriteLine("Duration = " + Duration);
            Console.WriteLine();
        }
    }
}
