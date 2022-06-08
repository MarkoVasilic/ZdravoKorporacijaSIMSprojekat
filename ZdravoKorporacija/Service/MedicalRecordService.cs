using Model;
using Repository;
using System;
using System.Collections.Generic;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Interfaces;
using ZdravoKorporacija.Repository;

namespace ZdravoKorporacija.Service
{
    public class MedicalRecordService
    {
        private readonly MedicalRecordRepository medicalRecordRepository;
        private readonly AnamnesisRepository anamnesisRepository;
        private readonly PrescriptionRepository prescriptionRepository;
        private readonly IPatientRepository patientRepository;
        private readonly AppointmentRepository appointmentRepository;

        public MedicalRecordService(MedicalRecordRepository medicalRecordRepository, AnamnesisRepository anamnesisRepository, PrescriptionRepository prescriptionRepository, IPatientRepository patientRepository, AppointmentRepository appointmentRepository)
        {
            this.medicalRecordRepository = medicalRecordRepository;
            this.anamnesisRepository = anamnesisRepository;
            this.prescriptionRepository = prescriptionRepository;
            this.patientRepository = patientRepository;
            this.appointmentRepository = appointmentRepository;
        }

        public MedicalRecordService()
        {
        }

        public List<MedicalRecordDTO> GetAll()
        {
            List<MedicalRecordDTO> medicalRecordDTOs = new List<MedicalRecordDTO>();
            List<MedicalRecord> medicalRecords = medicalRecordRepository.FindAll();
            foreach (MedicalRecord medicalRecord in medicalRecords)
            {
                MedicalRecordDTO medicalRecordDTO = GetOneByPatientJmbg(medicalRecord.PatientJmbg);
                medicalRecordDTOs.Add(medicalRecordDTO);
            }

            return medicalRecordDTOs;
        }

        public MedicalRecordDTO? GetOneByPatientJmbg(String patientJmbg)
        {
            
            MedicalRecord medicalRecord = medicalRecordRepository.FindOneByPatientJmbg(patientJmbg);
            List<Anamnesis> anamnesis = new List<Anamnesis>();

            foreach (int id in medicalRecord.AnamnesisIds)
                anamnesis.Add(anamnesisRepository.FindOneById(id));

            List<Prescription> prescriptions = new List<Prescription>();
            foreach (int id in medicalRecord.PrescriptionIds)
                prescriptions.Add(prescriptionRepository.FindOneById(id));

            Patient patient = patientRepository.FindOneByJmbg(patientJmbg);
            MedicalRecordDTO medicalRecordDTO = new MedicalRecordDTO(patient.FirstName, patient.LastName, patientJmbg, patient.DateOfBirth, patient.Gender, patient.Allergens,
                                                            patient.BloodTypeEnum, patient.PhoneNumber, patient.Email, patient.Address, anamnesis, prescriptions);

            return medicalRecordDTO;
        }

        public MedicalRecordDTO? GetOneByAppointmentId(int appointmentId)
        {
            String patientJmbg = appointmentRepository.FindOneById(appointmentId).PatientJmbg;
            MedicalRecordDTO medicalRecordDTO = GetOneByPatientJmbg(patientJmbg);

            return medicalRecordDTO;
        }
        public void ModifyMedicalRecord(String patientJmbg, List<int> prescriptionIds, List<int> anamnesisIds)
        {

            var oneMedicalRecord = medicalRecordRepository.FindOneByPatientJmbg(patientJmbg);
            MedicalRecord newMedicalRecord = new MedicalRecord(oneMedicalRecord.PatientJmbg, prescriptionIds, anamnesisIds);

            if (!newMedicalRecord.validateMedicalRecord())
                throw new Exception("Something went wrong, medical record isn't updated!");

            medicalRecordRepository.UpdateMedicalRecord(newMedicalRecord);
        }

        public void CreateMedicalRecord(String patientJmbg)
        {
            MedicalRecord medicalRecord = new MedicalRecord(patientJmbg, new List<int>(), new List<int>());

            if (!medicalRecord.validateMedicalRecord())
                throw new Exception("Something went wrong, medical record isn't created!");

            medicalRecordRepository.SaveMedicalRecord(medicalRecord);
        }
    }
}
