using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class MedicationService
    {

        private readonly MedicationRepository MedicationRepository;

        public MedicationService(MedicationRepository medicationRepository)
        {
            this.MedicationRepository = medicationRepository;
        }

        public MedicationService() { }


        public List<Medication> GetAll()
        {
            return MedicationRepository.FindAll();
        }

        public Medication GetOneByName(String medicationName)
        {
            return MedicationRepository.FindOneByName(medicationName);
        }

        public Medication GetOneById(int medicationId)
        {
            return MedicationRepository.FindOneById(medicationId);
        }

        public int GenerateNewId()
        {
            try
            {
                List<Medication> medications = MedicationRepository.FindAll();
                int currentMax = medications.Max(obj => obj.Id);
                return currentMax + 1;
            }
            catch
            {
                return 1;
            }
        }


        public void Create(String name, List<String> ingredients, String alternative)
        {
            int id = GenerateNewId();
            CheckBeforeCreating(id, name);
            Medication newMedication = new Medication(id, name, ingredients, MedicationStatus.UNVERIFIED, alternative);
            Validate(newMedication);
            MedicationRepository.SaveMedication(newMedication);
        }

        public void CheckBeforeCreating(int id, String name)
        {
            
            if(MedicationRepository.FindOneById(id) != null)
            {
                throw new Exception("Medication with that ID already exists!");
            }
            else if (MedicationRepository.FindOneByName(name) != null)
            {
                throw new Exception("Medication with that name already exists!");
            }

        }

        public List<Medication> GetAllUnverifiedMedications()
        {
            return MedicationRepository.FindAllUnverifiedMedications();
        }

        public void VerifyMedication(int medicationId)
        {
            Medication medication = GetOneById(medicationId);
            medication.Status = MedicationStatus.VERIFIED;
            MedicationRepository.UpdateMedication(medication);
        }

        public void RejectMedication(int medicationId, String reason)
        {
            Medication medication = GetOneById(medicationId);
            medication.Status = MedicationStatus.REJECTED;
            MedicationRepository.UpdateMedication(medication);
        }

        public void Modify(int id, String name, List<String> ingredients, String alternative)
        {

            CheckBeforeModification(id);
            Medication oldMedication = MedicationRepository.FindOneById(id);
            Medication newMedication = new Medication(oldMedication.Id, name, ingredients, MedicationStatus.UNVERIFIED, alternative);
            Validate(newMedication);
            MedicationRepository.UpdateMedication(newMedication);
            
        }


        public void CheckBeforeModification(int id)
        {
            if (MedicationRepository.FindOneById(id) == null)
                throw new Exception("Medication with that identification number doesn't exist");
            
        }

        public void Validate(Medication medication)
        {
            if (!medication.Validate())
            {
                throw new Exception("Something went wrong");
            }
        }

        public List<Medication> GetRejected()
        {
            List<Medication> medications = new List<Medication>(GetAll());
            List<Medication> foundMedications = new List<Medication>();

            foreach(Medication medication in medications)
            {
                if(medication.Status == MedicationStatus.REJECTED)
                {
                    foundMedications.Add(medication);
                }
            }

            return foundMedications;
        }


    }
}
