using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKorporacija.View.PatientUI.DTO
{
    public class AnamnesisDTO
    {
        public int Id;
        public String Diagnosis { get; set; }
        public String Report { get; set; }
        public DateTime DateTime { get; set; } //datum i vreme izvestaja
        public String DoctorJmbg { get; set; }

        public String DoctorFullName {get; set; }
        public AnamnesisDTO()
        {
        }

        public AnamnesisDTO(int id, string diagnosis, string report, DateTime dateTime, string doctorJmbg, String DoctorFullName)
        {
            Id = id;
            Diagnosis = diagnosis;
            Report = report;
            DateTime = dateTime;
            DoctorJmbg = doctorJmbg;
            this.DoctorFullName = DoctorFullName;
        }
    }
}
