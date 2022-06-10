using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using ZdravoKorporacija.Repository;

namespace ZdravoKorporacija.Service
{
    public class PrescriptionService
    {
        private readonly PrescriptionRepository _prescriptionRepository;
        private readonly MedicalRecordRepository _medicalRecordRepository;
        private readonly PatientRepository _patientRepository;
        private readonly MedicationRepository _medicationRepository;

        public PrescriptionService(PrescriptionRepository prescriptionRepository, MedicalRecordRepository medicalRecordRepository, PatientRepository patientRepository, MedicationRepository medicationRepository)
        {
            _prescriptionRepository = prescriptionRepository;
            _medicalRecordRepository = medicalRecordRepository;
            _patientRepository = patientRepository;
            _medicationRepository = medicationRepository;
        }

        public PrescriptionService()
        {
        }

        public List<Prescription> GetAll()
        {
            return _prescriptionRepository.FindAll();
        }

        public Prescription? GetOneById(int id)
        {
            return _prescriptionRepository.FindOneById(id);
        }

        public List<Prescription>? GetAllByPatient(String patientJmbg)
        {
            List<int> prescriptionIds = _medicalRecordRepository.FindOneByPatientJmbg(patientJmbg).PrescriptionIds;
            List<Prescription> result = new List<Prescription>();
            foreach (int id in prescriptionIds)
            {
                result.Add(_prescriptionRepository.FindOneById(id));
            }
            return result;

        }

        public void CreatePrescription(String patientJmbg, String medication, String amount, int frequency, DateTime from, DateTime to)
        {
            Prescription prescription = new Prescription(-1, medication, amount, frequency, from, to);

            ValidatePrescriptionBeforeCreation(patientJmbg, medication, prescription);
            _prescriptionRepository.SavePrescription(prescription);
            AddPrescriptionToMedicalRecord(patientJmbg, prescription);
        }

        private void AddPrescriptionToMedicalRecord(string patientJmbg, Prescription prescription)
        {
            List<int> newPrescriptions = _medicalRecordRepository.FindOneByPatientJmbg(patientJmbg).PrescriptionIds;
            newPrescriptions.Add(prescription.Id);
            MedicalRecord oneMedicalRecord = new MedicalRecord(patientJmbg, newPrescriptions,
                _medicalRecordRepository.FindOneByPatientJmbg(patientJmbg).AnamnesisIds);

            if (!oneMedicalRecord.validateMedicalRecord())
                throw new Exception("Something went wrong, medical record isn't updated!");

            _medicalRecordRepository.UpdateMedicalRecord(oneMedicalRecord);
        }

        private void ValidatePrescriptionBeforeCreation(string patientJmbg, string medication, Prescription prescription)
        {
            List<String> ingredients = _medicationRepository.FindOneByName(medication).Ingredients;

            if (ingredients == null)
                throw new Exception("Prescribed medication is not available!");
            List<String>? allergens = _patientRepository.FindOneByJmbg(patientJmbg).Allergens;
            if (isAllergic(allergens, ingredients))
                throw new Exception("Patient is allergic to that medication!");
            if (!prescription.validatePrescription())
                throw new Exception("Something went wrong, prescription isn't created!");
        }

        private Boolean isAllergic(List<string> allergens, List<string> ingredients)
        {
            if (allergens == null)
                return false;

            foreach (String ingredient in ingredients)
            {
                foreach (String allergen in allergens)
                {
                    if (ingredient.Equals(allergen))
                        return true;
                }
            }
            return false;
        }

        public void ModifyPrescription(int prescriptonId, String newMedication, String newAmount, int newFrequency, DateTime newFrom, DateTime newTo)
        {
            List<String> ingredients = _medicationRepository.FindOneByName(newMedication).Ingredients;
            List<String> allergens = GetPatientAllergens(prescriptonId);

            if (isAllergic(allergens, ingredients))
                throw new Exception("Patient is allergic to that medication!");

            Prescription newPrescription = new Prescription(prescriptonId, newMedication, newAmount, newFrequency, newFrom, newTo);
            if (!newPrescription.validatePrescription())
                throw new Exception("Something went wrong, prescription isn't updated!");

            _prescriptionRepository.UpdatePrescription(newPrescription);

        }

        private List<String> GetPatientAllergens(int prescriptonId)
        {
            List<String> allergens = new List<String>();
            List<MedicalRecord> medicalRecords = _medicalRecordRepository.FindAll();
            foreach (MedicalRecord mr in medicalRecords)
            {
                List<int> pr = mr.PrescriptionIds;
                foreach (int id in pr)
                {
                    if (id == prescriptonId)
                    {
                        allergens = _patientRepository.FindOneByJmbg(mr.PatientJmbg).Allergens;
                    }
                }
            }

            return allergens;
        }
    }
}
