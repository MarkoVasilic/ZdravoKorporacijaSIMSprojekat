using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Model
{
    public class Patient : User
    {
        public Boolean IsGuest { get; set; }
        public List<String> Allergens { get; set; }
        public BloodType BloodTypeEnum { get; set; }

        public int trollCounter { set; get; }

        public Patient(Boolean isGuest, List<String>? allergens, BloodType bloodType,
            string firstName, string lastName, string username, string password,
            string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? phoneNumber,
            string? address) : base(firstName, lastName, username, password, jmbg, dateOfBirth, gender, email, phoneNumber, address)
        {
            this.IsGuest = isGuest;
            this.Allergens = allergens ?? new List<String>();
            this.BloodTypeEnum = bloodType;
            this.trollCounter = 0;
        }

        public Patient() : base()
        {
            this.Allergens = new List<String>();
        }

        public Boolean validatePatient()
        {
            Regex usernameRegex = new Regex("^$|[a-zA-Z]+[a-zA-Z0-9_\\.\\s]*$");
            Regex onlyNumberRegex = new Regex("^[0-9]+$");
            Regex emailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            if (validateGuest().Length != 0)
                return false;
            else if (BloodTypeEnum == null || BloodTypeEnum == BloodType.NONE)
                return false;
            else if (Username == null || Username.Length < 3 || !usernameRegex.IsMatch(Username))
                return false;
            else if (Password == null || Password.Length < 8)
                return false;
            else if (DateOfBirth == null || DateOfBirth > DateTime.Now)
                return false;
            else if (Email == null || Email.Length == 0 || !emailRegex.IsMatch(Email))
                return false;
            else if (PhoneNumber == null || PhoneNumber.Length > 0 && !onlyNumberRegex.IsMatch(PhoneNumber))
                return false;
            else if (Address == null || Address.Length == 0)
                return false;
            else
                return true;

        }

        public void incrementTrollCounter()
        {
            this.trollCounter++;
            Console.WriteLine("THIS:" + this);
        }

        public String validateGuest()
        {
            Regex nameRegex = new Regex("^[a-zA-Z-\\s]+$");
            Regex onlyNumberRegex = new Regex("^[0-9]+$");
            if (FirstName == null || FirstName.Length < 3 || !nameRegex.IsMatch(FirstName))
                return "First name length must be greater than 3!";
            else if (LastName == null || LastName.Length < 3 || !nameRegex.IsMatch(LastName))
                return "Last name length must be greater than 3!";
            else if (Jmbg == null || Jmbg.Length != 13 || !onlyNumberRegex.IsMatch(Jmbg))
                return "Jmbg can only be 13 digits and is necessary to input!";
            else
                return "";
        }
    }
}