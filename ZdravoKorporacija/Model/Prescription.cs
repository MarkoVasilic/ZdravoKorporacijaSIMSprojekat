using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKorporacija.Model
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
    }




}
