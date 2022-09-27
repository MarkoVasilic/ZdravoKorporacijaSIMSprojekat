using Model;
using System;

namespace ZdravoKorporacija.View.DoctorUI.ViewModel
{
    public class ViewMedicalRecordPageVM : ViewModelBase
    {
        private String firstName;
        public String FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        private String lastName;
        public String LastName
        {
            get { return lastName; }
            set
            {
                firstName = value;
                OnPropertyChanged("LastName");
            }
        }

        private String jmbg;

        public String Jmbg
        {
            get { return jmbg; }
            set
            {
                jmbg = value;
                OnPropertyChanged("Jmbg");
            }
        }

        private DateTime dateOfBirth;

        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set
            {
                dateOfBirth = value;
                OnPropertyChanged("DateOfBirth");
            }
        }

        private Gender gender;

        public Gender Gender
        {
            get { return gender; }
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }
        private BloodType bloodType;

        public BloodType BloodType
        {
            get { return bloodType; }
            set
            {
                bloodType = value;
                OnPropertyChanged("BloodType");
            }
        }

        private int phoneNumber;

        public int PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }

        private String email;

        public String Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }

        private String adress;

        public String Adress
        {
            get { return adress; }
            set
            {
                adress = value;
                OnPropertyChanged("Adress");
            }
        }




    }
}
