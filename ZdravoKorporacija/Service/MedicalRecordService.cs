using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;

namespace ZdravoKorporacija.Service
{
    public class MedicalRecordService
    {
        private readonly MedicalRecordRepository MedicalRecordRepository;

        public MedicalRecordService(MedicalRecordRepository medicalRecordRepository)
        {
            this.MedicalRecordRepository = medicalRecordRepository;
        }
        public MedicalRecordService()
        {
        }

        public List<MedicalRecord> GetAll()
        {
            return MedicalRecordRepository.FindAll();
        }

        public MedicalRecord? GetOneByPatientJmbg(String patientJmbg)
        {
            return MedicalRecordRepository.FindOneByPatientJmbg(patientJmbg);
        }
        public void ModifyMedicalRecord(String patientJmbg, List<int> prescriptionIds, List<int> anamnesisIds)
        {

            var oneMedicalRecord = MedicalRecordRepository.FindOneByPatientJmbg(patientJmbg);
            MedicalRecord newMedicalRecord = new MedicalRecord(oneMedicalRecord.PatientJmbg, prescriptionIds, anamnesisIds);

            MedicalRecordRepository.UpdateMedicalRecord(newMedicalRecord);
        }
    }
}
