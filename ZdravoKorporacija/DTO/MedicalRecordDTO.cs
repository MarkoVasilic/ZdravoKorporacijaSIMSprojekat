using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKorporacija.Model;

namespace ZdravoKorporacija.DTO
{
    public class MedicalRecordDTO
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Jmbg { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public List<String>? Allergens { get; set; }
        public BloodType BloodTypeEnum { get; set; }
        public List<Anamnesis>? Anamnesis { get; set; }
        public List<Prescription>? Prescriptions { get; set; }

        public MedicalRecordDTO(string firstName, string lastName, string jmbg, DateTime? dateOfBirth, Gender gender, List<string>? allergens, BloodType bloodTypeEnum, List<Anamnesis>? anamnesis, List<Prescription>? prescriptions)
        {
            FirstName = firstName;
            LastName = lastName;
            Jmbg = jmbg;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Allergens = allergens;
            BloodTypeEnum = bloodTypeEnum;
            Anamnesis = anamnesis;
            Prescriptions = prescriptions;
        }

        public MedicalRecordDTO()
        {
        }

        public void ToString()
        {
            Console.WriteLine("First Name: " + FirstName);
            Console.WriteLine("Last Name: " + LastName);
            Console.WriteLine("Jmbg: " + Jmbg);
            Console.WriteLine("Date of Birth: " + DateOfBirth);
            Console.WriteLine("Gender: " + Gender.MALE);
            Console.Write("Allergens: ");
            if (Allergens != null)
            {
                foreach (String allergen in Allergens)
                {
                    Console.Write(allergen + ", ");
                }
            }
            Console.WriteLine("\nBlood Type: " + BloodTypeEnum);
            Console.WriteLine("\n");
            Console.WriteLine("Anamnesis: ");
            if (Anamnesis != null)
            {
                foreach (Anamnesis anamnesis in Anamnesis)
                {
                    anamnesis.ToString();
                    Console.Write("\n");
                }
            }
            else
            {
                Console.Write("No anamnesis \n");
            }
            Console.WriteLine("\n");
            Console.WriteLine("Prescriptions: ");
            if (Prescriptions != null)
            {
                foreach(Prescription prescription in Prescriptions)
                {
                    prescription.ToString();
                    Console.Write("\n");
                }
            }
            else
            {
                Console.Write("No prescriptions \n");
            }
            Console.WriteLine("\n");


        }
    }
}
