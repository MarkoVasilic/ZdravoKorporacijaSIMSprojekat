using System;

namespace Model
{
   public class Patient : User
   {
      public List<String> allergens;
      
      public Appointment[] appointment;
   
   }
}