using System;
using System.Collections.Generic;

namespace Model
{
    public class Patient : User
    {
        public Boolean IsGuest { get; set; }
        public List<String> Allergens { get; set; }
        public BloodType? BloodType { get; set; }

        public Patient(Boolean isGuest, List<String>? allergens, BloodType bloodType,
            string firstName, string lastName, string username, string password,
            string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? telephone,
            string? address) : base(firstName, lastName, username, password, jmbg, dateOfBirth, gender, email, telephone, address)
        {
            this.IsGuest = isGuest;
            this.Allergens = allergens ?? new List<String> ();
            this.BloodType = bloodType;
        }

        public Patient() : base()
        {
            this.Allergens = new List<String>();
        }
    }
}