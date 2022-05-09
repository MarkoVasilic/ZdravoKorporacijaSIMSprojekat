using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class AnamnesisService
    {
        private readonly AnamnesisRepository AnamnesisRepository;
        private readonly MedicalRecordRepository MedicalRecordRepository;

        public AnamnesisService(AnamnesisRepository anamnesisRepository, MedicalRecordRepository medicalRecordRepository)
        {
            this.AnamnesisRepository = anamnesisRepository;
            this.MedicalRecordRepository = medicalRecordRepository;
        }

        public AnamnesisService()
        {
        }

        public List<Anamnesis> GetAll()
        {
            return AnamnesisRepository.FindAll();
        }

        public Anamnesis? GetOneById(int anamnesisId)
        {
            return AnamnesisRepository.FindOneById(anamnesisId);
        }
        public List<Anamnesis>? GetAllByDoctor(String doctorJmbg)
        {
            return AnamnesisRepository.FindAllByDoctor(doctorJmbg);
        }

        public List<Anamnesis>? GetAllByPatient(String patientJmbg)
        {
            List<Anamnesis> result = new List<Anamnesis>();
            List<int> anamnesisIds = MedicalRecordRepository.FindOneByPatientJmbg(patientJmbg).AnamnesisIds;
            foreach (int id in anamnesisIds)
            {
                if (AnamnesisRepository.FindOneById(id) != null)
                {
                    result.Add(AnamnesisRepository.FindOneById(id));
                }
            }
            return result;
        }
        private int GenerateNewId()
        {
            try
            {
                List<Anamnesis> anamnesis = AnamnesisRepository.FindAll();
                int currentMax = anamnesis.Max(obj => obj.Id);
                return currentMax + 1;
            }
            catch
            {
                return 1;
            }
        }

        public void CreateAnamnesis(String patientJmbg, String diagnosis, String report)
        {
            int id = GenerateNewId();
            Anamnesis anamnesis = new Anamnesis(id, diagnosis, report, DateTime.Now, "4444444444444");   //doctorJmbg
            if (!anamnesis.validateAnamnesis())
            {
                throw new Exception("Something went wrong, anamnesis isn't created!");
            }
            AnamnesisRepository.SaveAnamnesis(anamnesis);

            if (MedicalRecordRepository.FindOneByPatientJmbg(patientJmbg) == null)
            {
                throw new Exception("Medical Record for patient with that jmbg doesn't exists");
            }
            else
            {
                List<int> newAnamnesis = MedicalRecordRepository.FindOneByPatientJmbg(patientJmbg).AnamnesisIds;
                newAnamnesis.Add(anamnesis.Id);
                MedicalRecord oneMedicalRecord = new MedicalRecord(patientJmbg,
                MedicalRecordRepository.FindOneByPatientJmbg(patientJmbg).PrescriptionIds, newAnamnesis);

                if (!oneMedicalRecord.validateMedicalRecord())
                {
                    throw new Exception("Something went wrong, medical record isn't updated!");
                }
                MedicalRecordRepository.UpdateMedicalRecord(oneMedicalRecord);
            }

        }

        public void ModifyAnamnesis(int anamnesisId, String diagnosis, String report)
        {

            var oneAnamnesis = AnamnesisRepository.FindOneById(anamnesisId);
            Anamnesis newAnamnesis = new Anamnesis(oneAnamnesis.Id, diagnosis, report, DateTime.Now, "4444444444444"); //vreme postaje vreme izmene

            if (!oneAnamnesis.validateAnamnesis())
            {
                throw new Exception("Something went wrong, anamnesis isn't updated!");
            }
            AnamnesisRepository.UpdateAnamnesis(newAnamnesis);

        }
    }
}
