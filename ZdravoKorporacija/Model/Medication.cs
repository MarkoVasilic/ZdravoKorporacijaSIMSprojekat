using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Model
{
    public class Medication
    {
        public int Id;
        public String Name;
        public List<String> Ingerdients;

        public Medication()
        {
        }

        public Medication(int id, string name, List<string> ingerdients)
        {
            Id = id;
            Name = name;
            Ingerdients = ingerdients;
        }

        public Boolean validateMedication()
        {
            Regex onlyNumberRegex = new Regex("^[0-9]+$");
            if (Id == null || !onlyNumberRegex.IsMatch(Id.ToString()))
                return false;
            else if (Name == null)
                return false;
            else if (Ingerdients == null)
                return false;
            else
                return true;

        }

    }
}
