using System;
using System.Collections.Generic;

namespace Model
{
    public class Doctor : User
    {
        public Boolean specialization;
        public String specializationType;


        public Doctor(Boolean specialization, String specializationType,
              string firstName, string lastName, string username, string password,
              string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? telephone,
              string? adress) : base(firstName, lastName, username, password, jmbg, dateOfBirth, gender, email, telephone, adress)
        {
            this.specialization = specialization;
            this.specializationType = specializationType;
        }

    }
}