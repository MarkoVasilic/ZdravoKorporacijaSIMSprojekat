using System;
using System.Collections.Generic;

namespace Model
{
    public class Doctor : User
    {
        public Boolean Specialization;
        public String SpecializationType;
        public Room Room;


        public Doctor(Boolean specialization, String specializationType, Room room,
              string firstName, string lastName, string username, string password,
              string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? phoneNumber,
              string? adress) : base(firstName, lastName, username, password, jmbg, dateOfBirth, gender, email, phoneNumber, adress)
        {
            this.Specialization = specialization;
            this.SpecializationType = specializationType;
            this.Room = room;
        }

        public Doctor() : base() { }

    }
}