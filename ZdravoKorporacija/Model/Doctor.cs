using System;
using System.Collections.Generic;

namespace Model
{
    public class Doctor : User
    {
        public Boolean specialization;
        public String specializationType;
        public int roomId { get; set; }//dodao jovan, ne znam da li treba int ili string  

        public Doctor(Boolean specialization, String specializationType, int roomId,
              string firstName, string lastName, string username, string password,
              string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? telephone,
              string? adress) : base(firstName, lastName, username, password, jmbg, dateOfBirth, gender, email, telephone, adress)
        {
            this.specialization = specialization;
            this.specializationType = specializationType;
            this.roomId = roomId;
        }

        public Doctor(bool specialization, string specializationType)
        {
            this.specialization = specialization;
            this.specializationType = specializationType;
        }
        public Doctor() { }
    }
}