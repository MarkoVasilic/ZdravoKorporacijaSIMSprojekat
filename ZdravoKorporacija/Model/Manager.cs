using System;

namespace Model
{
    public class Manager : User
    {
        public Manager(string firstName, string lastName, string username, string password,
            string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? phoneNumber,
            string? adress) : base(firstName, lastName, username, password, jmbg, dateOfBirth, gender, email, phoneNumber, adress)
        {
        }
    }
}