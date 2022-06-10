using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class MedicationService
    {

        private readonly MedicationRepository _medicationRepository;

        public MedicationService(MedicationRepository medicationRepository)
        {
            this._medicationRepository = medicationRepository;
        }

        public MedicationService() { }


        public List<Medication> GetAll()
        {
            return _medicationRepository.FindAll();
        }

        public Medication GetOneByName(String name)
        {
            return _medicationRepository.FindOneByName(name);
        }

        public Medication GetOneById(int id)
        {
            return _medicationRepository.FindOneById(id);
        }

        public int GenerateNewId()
        {
            try
            {
                List<Medication> medications = _medicationRepository.FindAll();
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
            _medicationRepository.SaveMedication(newMedication);
        }

        public void CheckBeforeCreating(int id, String name)
        {

            if (_medicationRepository.FindOneById(id) != null)
            {
                throw new Exception("Medication with that ID already exists!");
            }
            else if (_medicationRepository.FindOneByName(name) != null)
            {
                throw new Exception("Medication with that name already exists!");
            }

        }

        public List<Medication> GetAllUnverified()
        {
            return _medicationRepository.FindAllUnverified();
        }

        public List<Medication> GetAllVerified()
        {
            return _medicationRepository.FindAllVerified();
        }

        public void Verify(int id)
        {
            Medication medication = GetOneById(id);
            medication.Status = MedicationStatus.VERIFIED;
            _medicationRepository.Update(medication);
        }

        public void Reject(int id)
        {
            Medication medication = GetOneById(id);
            medication.Status = MedicationStatus.REJECTED;
            _medicationRepository.Update(medication);
        }

        public void Modify(int id, String name, List<String> ingredients, String alternative)
        {

            CheckBeforeModification(id);
            Medication oldMedication = _medicationRepository.FindOneById(id);
            Medication newMedication = new Medication(oldMedication.Id, name, ingredients, MedicationStatus.UNVERIFIED, alternative);
            Validate(newMedication);
            _medicationRepository.Update(newMedication);

        }


        public void CheckBeforeModification(int id)
        {
            if (_medicationRepository.FindOneById(id) == null)
                throw new Exception("Medication with that identification number doesn't exist");

        }

        public void Validate(Medication medication)
        {
            if (!medication.Validate())
            {
                throw new Exception("Something went wrong");
            }
        }

        public List<Medication> GetAllRejected()
        {
            return _medicationRepository.FindAllRejected();
        }


    }
}
