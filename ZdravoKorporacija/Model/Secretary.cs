using System;

namespace Model
{
    public class Secretary : User
    {
        public Secretary(string firstName, string lastName, string username, string password,
            string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? telephone,
            string? address) : base(firstName, lastName, username, password, jmbg, dateOfBirth, gender, email, telephone, address)
        {
        }
    }
}