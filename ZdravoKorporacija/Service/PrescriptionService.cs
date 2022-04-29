using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;

namespace ZdravoKorporacija.Service
{
    public class PrescriptionService
    {
        private readonly PrescriptionRepository PrescriptionRepository;
        private readonly MedicalRecordRepository MedicalRecordRepository;

        public PrescriptionService(PrescriptionRepository prescriptionRepository, MedicalRecordRepository medicalRecordRepository)
        {
            this.PrescriptionRepository = prescriptionRepository;
            this.MedicalRecordRepository = medicalRecordRepository;
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
            PrescriptionRepository.SavePrescription(prescription);

            List<int> newPrescriptions = MedicalRecordRepository.FindOneByPatientJmbg(patientJmbg).PrescriptionIds;
            newPrescriptions.Add(prescription.Id);
            MedicalRecord oneMedicalRecord = new MedicalRecord(patientJmbg, newPrescriptions,
                                     MedicalRecordRepository.FindOneByPatientJmbg(patientJmbg).AnamnesisIds);

            MedicalRecordRepository.UpdateMedicalRecord(oneMedicalRecord);


        }

        public void ModifyPrescription(int prescriptonId, String newMedication, String newAmount, int newFrequency, DateTime newFrom, DateTime newTo)
        {

            var onePrescription = PrescriptionRepository.FindOneById(prescriptonId);
            Prescription newPrescription = new Prescription(onePrescription.Id, newMedication, newAmount,
                                                            newFrequency, newFrom, newTo);

            PrescriptionRepository.UpdatePrescription(newPrescription);

        }


    }
}
