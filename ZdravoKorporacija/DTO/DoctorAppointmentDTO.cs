using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKorporacija.DTO
{
    public class DoctorAppointmentDTO
    {
        public int Id { get; set; }
        public String PatientJmbg { get; set; }
        public String PatientFirstName { get; set; }
        public String PatientLastName { get; set; }
        public String Description { get; set; }
        public DateTime Time { get; set; }
        public String RoomName { get; set; }

        public DoctorAppointmentDTO(int id, string patientJmbg, string patientFirstName, string patientLastName, String description, DateTime time, string roomName)
        {
            Id = id;
            PatientJmbg = patientJmbg;
            PatientFirstName = patientFirstName;
            PatientLastName = patientLastName;
            Description = description;
            Time = time;
            RoomName = roomName;
        }

        public DoctorAppointmentDTO() {}

        public void ToString()
        {
            Console.WriteLine("Appointment : ");
            Console.WriteLine("ID = " + Id);
            Console.WriteLine("Patient JMBG = " + PatientJmbg);
            Console.WriteLine("FirstName = " + PatientFirstName);
            Console.WriteLine("LastName = " + PatientLastName);
            Console.WriteLine("Description = " + Description);
            Console.WriteLine("Time = " + Time);
            Console.WriteLine("Room = " + RoomName);
            Console.WriteLine();
        }


    }
}
