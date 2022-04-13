using System;

namespace Model
{
    public class Doctor : User
    {
        public Boolean Specialty;
        public String SpecialtyType;
        public int RoomId;


        public Doctor(Boolean specialty, String specialtyType, int roomId,
              string firstName, string lastName, string username, string password,
              string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? phoneNumber,
              string? adress) : base(firstName, lastName, username, password, jmbg, dateOfBirth, gender, email, phoneNumber, adress)
        {
            this.Specialty = specialty;
            this.SpecialtyType = specialtyType;
            this.RoomId = roomId;
        }

        public Doctor() : base() { }

    }
}