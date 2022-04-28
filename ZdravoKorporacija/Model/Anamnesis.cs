using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKorporacija.Model
{
    public class Anamnesis
    {
        public int Id;
        public String Diagnosis;
        public String Report;
        public DateTime DateTime; //datum i vreme izvestaja
        public String DoctorJmbg;

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
    }
}
