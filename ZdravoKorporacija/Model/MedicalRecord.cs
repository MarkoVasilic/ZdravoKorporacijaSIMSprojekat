using System;
using System.Collections.Generic;


namespace ZdravoKorporacija.Model
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
    }
}
