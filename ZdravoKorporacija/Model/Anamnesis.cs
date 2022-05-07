using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Model
{
    public class Anamnesis
    {
        public int Id;
        public String Diagnosis;
        public String Report;
        public DateTime DateTime; //datum i vreme izvestaja
        public String DoctorJmbg;

        //null validacija, doctorJmbg

        public Anamnesis()
        {
        }

        public Anamnesis(int id, string diagnosis, string report, DateTime dateTime, string doctorJmbg)
        {
            Id = id;
            Diagnosis = diagnosis;
            Report = report;
            DateTime = dateTime;
            DoctorJmbg = doctorJmbg;
        }

        public Boolean validateAnamnesis()
        {
            Regex onlyNumberRegex = new Regex("^[0-9]+$");
            if (Id == null || !onlyNumberRegex.IsMatch(Id.ToString()))
                return false;
            else if (Diagnosis == null)
                return false;
            else if (Report == null)
                return false;
            else if (DateTime == null)
                return false;
            else if (DoctorJmbg == null || DoctorJmbg.Length != 13 || !onlyNumberRegex.IsMatch(DoctorJmbg))
                return false;
            else
                return true;

        }

        public void ToString()
        {
            Console.WriteLine("ID: " + Id);
            Console.WriteLine("Diagnosis: " + Diagnosis);
            Console.WriteLine("Report: " + Report);
            Console.WriteLine("DateTime: " + DateTime);
            Console.WriteLine("DoctorJmbg: " + DoctorJmbg);
            Console.WriteLine();
        }
    }
}
