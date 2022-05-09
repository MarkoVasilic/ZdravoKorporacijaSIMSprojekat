using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class PrescriptionService
    {
        private readonly PrescriptionRepository PrescriptionRepository;
        private readonly MedicalRecordRepository MedicalRecordRepository;
        private readonly PatientRepository PatientRepository;
        private readonly MedicationRepository MedicationRepository;

        public PrescriptionService(PrescriptionRepository prescriptionRepository, MedicalRecordRepository medicalRecordRepository, PatientRepository patientRepository, MedicationRepository medicationRepository)
        {
            PrescriptionRepository = prescriptionRepository;
            MedicalRecordRepository = medicalRecordRepository;
            PatientRepository = patientRepository;
            MedicationRepository = medicationRepository;
        }

        public PrescriptionService()
        {
        }

        public List<Prescription> GetAll()
        {
            return PrescriptionRepository.FindAll();
        }

        public Prescription? GetOneById(int prescriptionId)
        {
            return PrescriptionRepository.FindOneById(prescriptionId);
        }

        public List<Prescription>? GetAllByPatient(String patientJmbg)
        {
            List<int> prescriptionIds = MedicalRecordRepository.FindOneByPatientJmbg(patientJmbg).PrescriptionIds;
            List<Prescription> result = new List<Prescription>();
            foreach (int id in prescriptionIds)
            {
                result.Add(PrescriptionRepository.FindOneById(id));
            }
            return result;

        }
        private int GenerateNewId()
        {
            try
            {
                List<Prescription> prescriptions = PrescriptionRepository.FindAll();
                int currentMax = prescriptions.Max(obj => obj.Id);
                return currentMax + 1;
            }
            catch
            {
                return 1;
            }
        }

        public void CreatePrescription(String patientJmbg, String medication, String amount, int frequency, DateTime from, DateTime to)
        {
            int id = GenerateNewId();
            Prescription prescription = new Prescription(id, medication, amount, frequency, from, to);
            List<String> allergens = PatientRepository.FindOneByJmbg(patientJmbg).Allergens;
            List<String> ingredients = MedicationRepository.FindOneByName(medication).Ingerdients;
            if (ingredients == null)
            {
                throw new Exception("Prescribed medication is not available!");
            }
            foreach (String ingredient in ingredients)
            {
                foreach (String allergen in allergens)
                {
                    if (ingredient.Equals(allergen))
                    {
                        throw new Exception("Patient is allergic to that medication!");
                    }
                }
            }
            if (!prescription.validatePrescription())
            {
                throw new Exception("Something went wrong, prescription isn't created!");
            }
            PrescriptionRepository.SavePrescription(prescription);

            List<int> newPrescriptions = MedicalRecordRepository.FindOneByPatientJmbg(patientJmbg).PrescriptionIds;
            newPrescriptions.Add(prescription.Id);
            MedicalRecord oneMedicalRecord = new MedicalRecord(patientJmbg, newPrescriptions,
                                     MedicalRecordRepository.FindOneByPatientJmbg(patientJmbg).AnamnesisIds);

            if (!oneMedicalRecord.validateMedicalRecord())
            {
                throw new Exception("Something went wrong, medical record isn't updated!");
            }
            MedicalRecordRepository.UpdateMedicalRecord(oneMedicalRecord);

        }

        public void ModifyPrescription(int prescriptonId, String newMedication, String newAmount, int newFrequency, DateTime newFrom, DateTime newTo)
        {

            var onePrescription = PrescriptionRepository.FindOneById(prescriptonId);
            Prescription newPrescription = new Prescription(onePrescription.Id, newMedication, newAmount,
                                                            newFrequency, newFrom, newTo);

            List<String> allergens = new List<String>();
            List<String> ingredients = MedicationRepository.FindOneByName(newMedication).Ingerdients;
            if (ingredients == null)
            {
                throw new Exception("Prescribed medication is not available!");
            }
            List<MedicalRecord> medicalRecords = MedicalRecordRepository.FindAll();
            foreach (MedicalRecord mr in medicalRecords)
            {
                List<int> pr = mr.PrescriptionIds;
                foreach (int id in pr)
                {
                    if (id == prescriptonId)
                    {
                        allergens = PatientRepository.FindOneByJmbg(mr.PatientJmbg).Allergens;
                    }
                }
            }

            foreach (String ingredient in ingredients)
            {
                foreach (String allergen in allergens)
                {
                    if (ingredient.Equals(allergen))
                    {
                        throw new Exception("Patient is allergic to that medication!");
                    }
                }
            }

            if (!onePrescription.validatePrescription())
            {
                throw new Exception("Something went wrong, prescription isn't updated!");
            }
            PrescriptionRepository.UpdatePrescription(newPrescription);

        }


    }
}
