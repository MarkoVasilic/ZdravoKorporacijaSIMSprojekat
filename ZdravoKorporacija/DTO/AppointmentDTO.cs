using System;

namespace ZdravoKorporacija.DTO
{
    public class AppointmentDTO
    {
        public int Id { get; set; }
        public String PatientJmbg { get; set; }
        public String PatientFirstName { get; set; }
        public String PatientLastName { get; set; }
        public DateTime StartTime { get; set; }
        public String RoomName { get; set; }

        public AppointmentDTO(int id, string patientJmbg, string patientFirstName, string patientLastName, DateTime startTime, string roomName)
        {
            Id = id;
            PatientJmbg = patientJmbg;
            PatientFirstName = patientFirstName;
            PatientLastName = patientLastName;
            StartTime = startTime;
            RoomName = roomName;
        }

        public AppointmentDTO()
        {
        }

        public void ToString()
        {
            Console.WriteLine("Appointment : ");
            Console.WriteLine("ID = " + Id);
            Console.WriteLine("Patient JMBG = " + PatientJmbg);
            Console.WriteLine("FirstName = " + PatientFirstName);
            Console.WriteLine("LastName = " + PatientLastName);
            Console.WriteLine("Time = " + StartTime);
            Console.WriteLine("Room = " + RoomName);
            Console.WriteLine();
        }
    }
}
