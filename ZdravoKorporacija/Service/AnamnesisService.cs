using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using ZdravoKorporacija.Repository;

namespace ZdravoKorporacija.Service
{
    public class AnamnesisService
    {
        private readonly AnamnesisRepository anamnesisRepository;
        private readonly MedicalRecordRepository medicalRecordRepository;
        private readonly DoctorRepository doctorRepository;

        public AnamnesisService(AnamnesisRepository anamnesisRepository, MedicalRecordRepository medicalRecordRepository, DoctorRepository doctorRepository)
        {
            this.anamnesisRepository = anamnesisRepository;
            this.medicalRecordRepository = medicalRecordRepository;
            this.doctorRepository = doctorRepository;
        }

        public AnamnesisService()
        {
        }

        public List<Anamnesis> GetAll()
        {
            return anamnesisRepository.FindAll();
        }

        public Anamnesis? GetOneById(int id)
        {
            return anamnesisRepository.FindOneById(id);
        }
        public List<Anamnesis>? GetAllByDoctor(String doctorJmbg)
        {
            return anamnesisRepository.FindAllByDoctor(doctorJmbg);
        }

        public List<Anamnesis>? GetAllByPatient(String patientJmbg)
        {
            List<Anamnesis> result = new List<Anamnesis>();
            List<int> anamnesisIds = medicalRecordRepository.FindOneByPatientJmbg(patientJmbg).AnamnesisIds;
            foreach (int id in anamnesisIds)
            {
                if (anamnesisRepository.FindOneById(id) != null)
                {
                    result.Add(anamnesisRepository.FindOneById(id));
                }
            }
            return result;
        }
        private int GenerateNewId()
        {
            try
            {
                List<Anamnesis> anamnesis = anamnesisRepository.FindAll();
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
            Anamnesis anamnesis = new Anamnesis(id, diagnosis, report, DateTime.Now, "1231231231231");
            if (!anamnesis.validateAnamnesis())
                throw new Exception("Something went wrong, anamnesis isn't created!");
            anamnesisRepository.SaveAnamnesis(anamnesis);

            if (medicalRecordRepository.FindOneByPatientJmbg(patientJmbg) == null)
            {
                throw new Exception("Medical Record for patient with that jmbg doesn't exists");
            }
            else
            {
                List<int> newAnamnesis = medicalRecordRepository.FindOneByPatientJmbg(patientJmbg).AnamnesisIds;
                newAnamnesis.Add(anamnesis.Id);
                MedicalRecord oneMedicalRecord = new MedicalRecord(patientJmbg,
                medicalRecordRepository.FindOneByPatientJmbg(patientJmbg).PrescriptionIds, newAnamnesis);

                if (!oneMedicalRecord.validateMedicalRecord())
                {
                    throw new Exception("Something went wrong, medical record isn't updated!");
                }
                medicalRecordRepository.UpdateMedicalRecord(oneMedicalRecord);
            }

        }

        public void ModifyAnamnesis(int id, String diagnosis, String report)
        {

            var oneAnamnesis = anamnesisRepository.FindOneById(id);
            Anamnesis newAnamnesis = new Anamnesis(oneAnamnesis.Id, diagnosis, report, DateTime.Now, "4444444444444"); //vreme postaje vreme izmene

            if (!newAnamnesis.validateAnamnesis())
            {
                throw new Exception("Something went wrong, anamnesis isn't updated!");
            }
            anamnesisRepository.UpdateAnamnesis(newAnamnesis);

        }
    }
}
