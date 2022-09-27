using System;
using System.Text.RegularExpressions;

namespace Model
{
    public class Anamnesis
    {
        public int Id;
        public String Diagnosis { get; set; }
        public String Report { get; set; }
        public DateTime DateTime { get; set; } //datum i vreme izvestaja
        public String DoctorJmbg { get; set; }
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
            else if (Diagnosis.Length < 2)
                return false;
            else if (Report.Length < 2)
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
            Console.WriteLine("Doctor: " + DoctorJmbg);
            Console.WriteLine();
        }

        public String toPDF()
        {
            String txt = "";
            txt += "Diagnosis:" + Diagnosis + "\n";
            txt += "Report:" + Report + "\n";
            txt += "DateTime:" + DateTime + "\n";
            txt += "Doctor:" + DoctorJmbg + "\n";
            return txt;
        }
    }
}
