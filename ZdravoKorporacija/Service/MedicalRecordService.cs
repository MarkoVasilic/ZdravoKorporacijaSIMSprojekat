using Model;
using Repository;
using System;
using System.Collections.Generic;
using ZdravoKorporacija.DTO;

namespace Service
{
    public class MedicalRecordService
    {
        private readonly MedicalRecordRepository MedicalRecordRepository;
        private readonly AnamnesisRepository AnamnesisRepository;
        private readonly PrescriptionRepository PrescriptionRepository;
        private readonly PatientRepository PatientRepository;
        private readonly AppointmentRepository AppointmentRepository;

        public MedicalRecordService(MedicalRecordRepository medicalRecordRepository, AnamnesisRepository anamnesisRepository, PrescriptionRepository prescriptionRepository, PatientRepository patientRepository, AppointmentRepository appointmentRepository)
        {
            MedicalRecordRepository = medicalRecordRepository;
            AnamnesisRepository = anamnesisRepository;
            PrescriptionRepository = prescriptionRepository;
            PatientRepository = patientRepository;
            AppointmentRepository = appointmentRepository;
        }

        public MedicalRecordService()
        {
        }

        public List<MedicalRecordDTO> GetAll()
        {
            List<MedicalRecordDTO> medicalRecordDTOs = new List<MedicalRecordDTO>();
            List<MedicalRecord> medicalRecords = MedicalRecordRepository.FindAll();
            foreach (MedicalRecord medicalRecord in medicalRecords)
            {

                MedicalRecordDTO medicalRecordDTO = GetOneByPatientJmbg(medicalRecord.PatientJmbg);

                medicalRecordDTOs.Add(medicalRecordDTO);

            }

            return medicalRecordDTOs;
        }

        public MedicalRecordDTO? GetOneByPatientJmbg(String patientJmbg)
        {
            MedicalRecordDTO medicalRecordDTO = new MedicalRecordDTO();
            MedicalRecord medicalRecord = MedicalRecordRepository.FindOneByPatientJmbg(patientJmbg);
            List<Anamnesis> anamnesis = new List<Anamnesis>();
            List<Prescription> prescriptions = new List<Prescription>();
            foreach (int id in medicalRecord.AnamnesisIds)
            {
                anamnesis.Add(AnamnesisRepository.FindOneById(id));
            }
            foreach (int id in medicalRecord.PrescriptionIds)
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
            medicalRecordDTO.PhoneNumber = PatientRepository.FindOneByJmbg(patientJmbg).PhoneNumber;
            medicalRecordDTO.Email = PatientRepository.FindOneByJmbg(patientJmbg).Email;
            medicalRecordDTO.Address = PatientRepository.FindOneByJmbg(patientJmbg).Address;
            medicalRecordDTO.Anamnesis = anamnesis;
            medicalRecordDTO.Prescriptions = prescriptions;

            return medicalRecordDTO;
        }

        public MedicalRecordDTO? GetOneByAppointmentId(int appointmentId)
        {
            MedicalRecordDTO medicalRecordDTO = new MedicalRecordDTO();
            String patientJmbg = AppointmentRepository.FindOneById(appointmentId).PatientJmbg;
            MedicalRecord medicalRecord = MedicalRecordRepository.FindOneByPatientJmbg(patientJmbg);
            List<Anamnesis> anamnesis = new List<Anamnesis>();
            List<Prescription> prescriptions = new List<Prescription>();
            foreach (int id in medicalRecord.AnamnesisIds)
            {
                anamnesis.Add(AnamnesisRepository.FindOneById(id));
            }
            foreach (int id in medicalRecord.PrescriptionIds)
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

        public void CreateMedicalRecord(String patientJmbg)
        {
            MedicalRecord medicalRecord = new MedicalRecord();
            medicalRecord.PatientJmbg = patientJmbg;
            medicalRecord.AnamnesisIds = new List<int>();
            medicalRecord.PrescriptionIds = new List<int>();

            if (!medicalRecord.validateMedicalRecord())
            {
                throw new Exception("Something went wrong, medical record isn't created!");
            }
            MedicalRecordRepository.SaveMedicalRecord(medicalRecord);
        }
    }
}
