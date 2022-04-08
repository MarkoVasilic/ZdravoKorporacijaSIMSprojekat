using System;

namespace Model
{
    public abstract class User
    {
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String username { get; set; }
        public String password { get; set; }
        public String jmbg { get; set; }
        public DateTime? dateOfBirth { get; set; }
        public Gender gender { get; set; }
        public String email { get; set; }
        public String telephone { get; set; }
        public String adress { get; set; }

        protected User(string firstName, string lastName, string username, string password,
            string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? telephone, string? adress)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.username = username;
            this.password = password;
            this.jmbg = jmbg;
            this.dateOfBirth = dateOfBirth;
            this.gender = gender;
            this.email = email ?? "";
            this.telephone = telephone ?? "";
            this.adress = adress ?? "";
        }
    }
}