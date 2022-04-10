using System;

namespace Model
{
    public abstract class User
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String Jmbg { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public String Email { get; set; }
        public String PhoneNumber { get; set; }
        public String Address { get; set; }

        protected User(string firstName, string lastName, string username, string password,
            string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? phoneNumber, string? address)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Username = username;
            this.Password = password;
            this.Jmbg = jmbg;
            this.DateOfBirth = dateOfBirth;
            this.Gender = gender;
            this.Email = email ?? "";
            this.PhoneNumber = phoneNumber ?? "";
            this.Address = address ?? "";
        }

        protected User()
        {

        }
    }
}