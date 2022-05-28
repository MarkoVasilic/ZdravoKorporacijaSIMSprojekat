using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Model
{
    public class Medication
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public List<String> Ingredients { get; set; }
        public MedicationStatus Status { get; set; }

        public String Alternative { get; set; }


        public Medication()
        {
        }

        public Medication(int id, String name, List<String> ingredients, MedicationStatus status, String alternative)
        {
            Id = id;
            Name = name;
            Ingredients = ingredients;
            Status = status;
            Alternative = alternative;

        }

        public Boolean Validate()
        {
            Regex onlyNumberRegex = new Regex("^[0-9]+$");
            if (Id == null || !onlyNumberRegex.IsMatch(Id.ToString()))
                return false;
            else if (Name == null || Name.Length < 2)
                return false;
            else if (Ingredients == null)
                return false;
            else if (Status == null)
                return false;
            else if (Alternative == null || Alternative.Length < 2)
                return false;
            else
                return true;

        }

        public void toString()
        {
            Console.WriteLine("ID = " + Id);
            Console.WriteLine("Name = " + Name);
            Console.WriteLine("Ingredients = ");
            if (Ingredients != null)
            {
                foreach (string ingredient in Ingredients)
                {
                    Console.WriteLine(ingredient);
                }
            }
            else
            {
                Console.WriteLine("No ingredients");
            }
            Console.WriteLine("Status = " + Status);
            Console.WriteLine("Zamena = " + Alternative);
            Console.WriteLine();
        }

    }
}
