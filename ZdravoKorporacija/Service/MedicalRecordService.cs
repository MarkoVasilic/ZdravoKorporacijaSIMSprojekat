using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKorporacija.DTO;
using Model;
using Repository;

namespace Service
{
    public class MedicalRecordService
    {
        private readonly MedicalRecordRepository MedicalRecordRepository;
        private readonly AnamnesisRepository AnamnesisRepository;
        private readonly PrescriptionRepository PrescriptionRepository;
        private readonly PatientRepository PatientRepository;

        public MedicalRecordService(MedicalRecordRepository medicalRecordRepository, AnamnesisRepository anamnesisRepository, PrescriptionRepository prescriptionRepository, PatientRepository patientRepository)
        {
            MedicalRecordRepository = medicalRecordRepository;
            AnamnesisRepository = anamnesisRepository;
            PrescriptionRepository = prescriptionRepository;
            PatientRepository = patientRepository;
        }

        public MedicalRecordService()
        {
        }

        public List<MedicalRecord> GetAll()
        {
            return MedicalRecordRepository.FindAll();
        }

        public MedicalRecordDTO? GetOneByPatientJmbg(String patientJmbg)
        {
            MedicalRecordDTO medicalRecordDTO = new MedicalRecordDTO();
            MedicalRecord medicalRecord = MedicalRecordRepository.FindOneByPatientJmbg(patientJmbg);
            List<Anamnesis> anamnesis = new List<Anamnesis>();
            List<Prescription> prescriptions = new List<Prescription>();
            foreach(int id in medicalRecord.AnamnesisIds)
            {
                anamnesis.Add(AnamnesisRepository.FindOneById(id));
            }
            foreach(int id in medicalRecord.PrescriptionIds)
            {
                prescriptions.Add(PrescriptionRepository.FindOneById(id));
            }

            medicalRecordDTO.FirstName = PatientRepository.FindOneByJmbg(patientJmbg).FirstName;
            medicalRecordDTO.LastName = PatientRepository.FindOneByJmbg(patientJmbg).LastName;
            medicalRecordDTO.Jmbg = PatientRepository.FindOneByJmbg(patientJmbg).Jmbg;
            medicalRecordDTO.DateOfBirth = PatientRepository.FindOneByJmbg(patientJmbg).DateOfBirth;
            medicalRecordDTO.Gender = PatientRepository.FindOneByJmbg(patientJmbg).Gender;
            medicalRecordDTO.Allergens = PatientRepository.FindOneByJmbg(patientJmbg).Allergens;
            medicalRecordDTO.BloodTypeEnum = PatientRepository.FindOneByJmbg(patientJmbg).BloodTypeEnum;
            medicalRecordDTO.Anamnesis = anamnesis;
            medicalRecordDTO.Prescriptions = prescriptions;

            return medicalRecordDTO;
        }
        public void ModifyMedicalRecord(String patientJmbg, List<int> prescriptionIds, List<int> anamnesisIds)
        {

            var oneMedicalRecord = MedicalRecordRepository.FindOneByPatientJmbg(patientJmbg);
            MedicalRecord newMedicalRecord = new MedicalRecord(oneMedicalRecord.PatientJmbg, prescriptionIds, anamnesisIds);

            if (!newMedicalRecord.validateMedicalRecord())
            {
                throw new Exception("Something went wrong, medical record isn't updated!");
            }
            MedicalRecordRepository.UpdateMedicalRecord(newMedicalRecord);
        }
    }
}
