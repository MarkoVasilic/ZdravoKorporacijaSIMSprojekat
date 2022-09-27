using System;
using System.Text.RegularExpressions;

namespace Model
{
    public class Prescription
    {
        public int Id { get; set; }
        public String Medication { get; set; }
        public String Amount { get; set; }
        public int Frequency { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

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
           // if (Id == null || !onlyNumberRegex.IsMatch(Id.ToString()))
              //  return false;
             if (Medication == null)
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

        public String ToString()
        {
            Console.WriteLine("ID: " + Id);
            Console.WriteLine("Medication: " + Medication);
            Console.WriteLine("Amount: " + Amount);
            Console.WriteLine("Frequency: " + Frequency);
            Console.WriteLine("From: " + From);
            Console.WriteLine("To: " + To);
            // return "Recepti: \n\n\n" + " ID: \n\n\n" + Id + "Lijek: \n" + Medication + "Količina: \n" + Amount + "Frekvencija: \n" + Frequency + " Datum OD: \n" + From + " Datum DO: \n" + To + "\n" + "-------------------------------- \n";
            String txt = "";
            txt += "ID : " + Id + "\n";
            txt += "Medication : " + Medication + "\n";
            txt += "Amount : " + Amount + "\n";
            txt += "Frequency : " + Frequency + "\n";
            txt += "From : " + From + "\n";
            txt += "To : " + To + "\n";
            txt += "---------------------- \n";
            return txt;
        }

        public String toPDF()
        {
            String txt = "";
            txt += "Medication : " + Medication + "\n";
            txt += "Amount : " + Amount + "\n";
            txt += "Frequency : " + Frequency + "\n";
            txt += "From : " + From + "\n";
            txt += "To : " + To + "\n";
            return txt;
        }

    }




}
