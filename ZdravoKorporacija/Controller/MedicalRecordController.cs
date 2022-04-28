using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.Controller
{
    public class MedicalRecordController
    {
        private readonly MedicalRecordService MedicalRecordService;

        public MedicalRecordController(MedicalRecordService medicalRecordService)
        {
            MedicalRecordService = medicalRecordService;
        }

        public List<MedicalRecord> GetAll()
        {
            return MedicalRecordService.GetAll();
        }

        public MedicalRecord? GetOneByPatientJmbg(String patientJmbg)
        {
            return MedicalRecordService.GetOneByPatientJmbg(patientJmbg);
        }

        public void ModifyMedicalRecord(String patientJmbg, List<int> prescriptionIds, List<int> anamnesisIds)
        {

            MedicalRecordService.ModifyMedicalRecord(patientJmbg, prescriptionIds, anamnesisIds);
        }

    }
}
