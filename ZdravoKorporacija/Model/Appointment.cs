using System;

namespace Model
{
   public class Appointment
   {
      public DateTime startTime;
      public int duration;
      public int id;
      
      public Patient patient;
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
      public Room room;
      
      
      public Room Room
      {
         get
         {
            return room;
         }
         set
         {
            this.room = value;
         }
      }
   
   }
}