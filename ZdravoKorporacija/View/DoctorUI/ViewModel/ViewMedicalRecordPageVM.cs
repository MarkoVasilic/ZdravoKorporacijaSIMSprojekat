using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                OnProperyChanged("FirstName");
            }
        }
        private String lastName;
        public String LastName
        {
            get { return lastName; }
            set
            {
                firstName = value;
                OnProperyChanged("LastName");
            }
        }

        private String jmbg;

        public String Jmbg
        {
            get { return jmbg; }
            set
            {
                jmbg = value;
                OnProperyChanged("Jmbg");
            }
        }

        private DateTime dateOfBirth;

        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set
            {
                dateOfBirth = value;
                OnProperyChanged("DateOfBirth");
            }
        }

        private Gender gender;

        public Gender Gender
        {
            get { return gender; }
            set
            {
                gender = value;
                OnProperyChanged("Gender");
            }
        }
        private BloodType bloodType;

        public BloodType BloodType
        {
            get { return bloodType; }
            set
            {
                bloodType = value;
                OnProperyChanged("BloodType");
            }
        }

        private int phoneNumber;

        public int PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = value;
                OnProperyChanged("PhoneNumber");
            }
        }

        private String email;

        public String Email
        {
            get { return email; }
            set
            {
                email = value;
                OnProperyChanged("Email");
            }
        }

        private String adress;

        public String Adress
        {
            get { return adress; }
            set
            {
                adress = value;
                OnProperyChanged("Adress");
            }
        }




    }
}
