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
        private readonly MedicalRecordRepository _medicalRecordRepository;
        private readonly AnamnesisRepository _anamnesisRepository;
        private readonly PrescriptionRepository _prescriptionRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly AppointmentRepository _appointmentRepository;

        public MedicalRecordService(MedicalRecordRepository medicalRecordRepository, AnamnesisRepository anamnesisRepository, PrescriptionRepository prescriptionRepository, IPatientRepository patientRepository, AppointmentRepository appointmentRepository)
        {
            this._medicalRecordRepository = medicalRecordRepository;
            this._anamnesisRepository = anamnesisRepository;
            this._prescriptionRepository = prescriptionRepository;
            this._patientRepository = patientRepository;
            this._appointmentRepository = appointmentRepository;
        }

        public MedicalRecordService()
        {
        }

        public List<MedicalRecordDTO> GetAll()
        {
            List<MedicalRecordDTO> medicalRecordDTOs = new List<MedicalRecordDTO>();
            List<MedicalRecord> medicalRecords = _medicalRecordRepository.FindAll();
            foreach (MedicalRecord medicalRecord in medicalRecords)
            {
                MedicalRecordDTO medicalRecordDTO = GetOneByPatientJmbg(medicalRecord.PatientJmbg);
                medicalRecordDTOs.Add(medicalRecordDTO);
            }

            return medicalRecordDTOs;
        }

        public MedicalRecordDTO? GetOneByPatientJmbg(String patientJmbg)
        {
            
            MedicalRecord medicalRecord = _medicalRecordRepository.FindOneByPatientJmbg(patientJmbg);
            List<Anamnesis> anamnesis = new List<Anamnesis>();

            foreach (int id in medicalRecord.AnamnesisIds)
                anamnesis.Add(_anamnesisRepository.FindOneById(id));

            List<Prescription> prescriptions = new List<Prescription>();
            foreach (int id in medicalRecord.PrescriptionIds)
                prescriptions.Add(_prescriptionRepository.FindOneById(id));

            Patient patient = _patientRepository.FindOneByJmbg(patientJmbg);
            MedicalRecordDTO medicalRecordDTO = new MedicalRecordDTO(patient.FirstName, patient.LastName, patientJmbg, patient.DateOfBirth, patient.Gender, patient.Allergens,
                                                            patient.BloodTypeEnum, patient.PhoneNumber, patient.Email, patient.Address, anamnesis, prescriptions);

            return medicalRecordDTO;
        }

        public MedicalRecordDTO? GetOneByAppointmentId(int appointmentId)
        {
            String patientJmbg = _appointmentRepository.FindOneById(appointmentId).PatientJmbg;
            MedicalRecordDTO medicalRecordDTO = GetOneByPatientJmbg(patientJmbg);

            return medicalRecordDTO;
        }
        public void ModifyMedicalRecord(String patientJmbg, List<int> prescriptionIds, List<int> anamnesisIds)
        {

            var oneMedicalRecord = _medicalRecordRepository.FindOneByPatientJmbg(patientJmbg);
            MedicalRecord newMedicalRecord = new MedicalRecord(oneMedicalRecord.PatientJmbg, prescriptionIds, anamnesisIds);

            if (!newMedicalRecord.validateMedicalRecord())
                throw new Exception("Something went wrong, medical record isn't updated!");

            _medicalRecordRepository.UpdateMedicalRecord(newMedicalRecord);
        }

        public void CreateMedicalRecord(String patientJmbg)
        {
            MedicalRecord medicalRecord = new MedicalRecord(patientJmbg, new List<int>(), new List<int>());

            if (!medicalRecord.validateMedicalRecord())
                throw new Exception("Something went wrong, medical record isn't created!");

            _medicalRecordRepository.SaveMedicalRecord(medicalRecord);
        }
    }
}
