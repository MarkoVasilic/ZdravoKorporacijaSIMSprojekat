using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Model
{
    public class MedicalRecord
    {
        public String PatientJmbg;
        public List<int> PrescriptionIds;
        public List<int> AnamnesisIds;

        public MedicalRecord()
        {
        }
        public MedicalRecord(string patientJmbg, List<int> prescriptionIds, List<int> anamnesisIds)
        {
            PatientJmbg = patientJmbg;
            PrescriptionIds = prescriptionIds;
            AnamnesisIds = anamnesisIds;
        }
        public Boolean validateMedicalRecord()
        {
            Regex onlyNumberRegex = new Regex("^[0-9]+$");
            if (PatientJmbg == null || PatientJmbg.Length != 13 || !onlyNumberRegex.IsMatch(PatientJmbg))
                return false;
            else
                return true;

        }



    }
}
