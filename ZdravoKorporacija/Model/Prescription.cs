﻿using System;
using System.Text.RegularExpressions;

namespace Model
{
    public class Prescription
    {
        public int Id;
        public String Medication;
        public String Amount;
        public int Frequency;
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

        public void ToString()
        {
            Console.WriteLine("ID: " + Id);
            Console.WriteLine("Medication: " + Medication);
            Console.WriteLine("Amount: " + Amount);
            Console.WriteLine("Frequency: " + Frequency);
            Console.WriteLine("From: " + From);
            Console.WriteLine("To: " + To);
        }
    }




}
