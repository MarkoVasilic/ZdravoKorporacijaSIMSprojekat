using System;
using System.Collections.Generic;

namespace Model
{
    public class Patient : User
    {
        public List<String> allergens { get; set; }
        public BloodType? bloodType { get; set; }

        public Patient(List<String>? allergens, BloodType bloodType,
            string firstName, string lastName, string username, string password,
            string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? telephone,
            string? adress) : base(firstName, lastName, username, password, jmbg, dateOfBirth, gender, email, telephone, adress)
        {
            this.allergens = allergens ?? new List<String>();
            this.bloodType = bloodType;
        }
    }
}