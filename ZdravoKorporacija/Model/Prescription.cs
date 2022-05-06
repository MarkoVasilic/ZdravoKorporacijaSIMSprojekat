using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Model
{
    public class Prescription
    {
        public int Id;
        public String Medication;
        public String Amount;
        public int Frequency;   //na koliko sati
        public DateTime From;
        public DateTime To;

        public Prescription()
        {
        }

        public Prescription(int id, string medication, string amount, int frequency, DateTime from, DateTime to)
        {
            Id = id;
            Medication = medication;
            Amount = amount;
            Frequency = frequency;
            From = from;
            To = to;
        }
        public Boolean validatePrescription()
        {
            Regex onlyNumberRegex = new Regex("^[0-9]+$");
            if (Id == null || !onlyNumberRegex.IsMatch(Id.ToString()))
                return false;
            else if (Medication == null)
                return false;
            else if (Frequency == null)
                return false;
            else if (From == null || From < DateTime.Now)
                return false;
            if (To == null || To < DateTime.Now)
                return false;
            else
                return true;

        }
    }




}
