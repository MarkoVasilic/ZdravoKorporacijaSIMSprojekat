using Model;
using Service;
using System;
using System.Collections.Generic;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.Controller
{
    public class MedicalRecordController
    {
        private readonly MedicalRecordService _medicalRecordService;
        private readonly AnamnesisService _anamnesisService;
        private readonly PrescriptionService _prescriptionService;

        public MedicalRecordController(MedicalRecordService medicalRecordService, AnamnesisService anamnesisService, PrescriptionService prescriptionService)
        {
            this._medicalRecordService = medicalRecordService;
            this._anamnesisService = anamnesisService;
            this._prescriptionService = prescriptionService;
        }

        public List<MedicalRecordDTO> GetAllMedicalRecords()
        {
            return _medicalRecordService.GetAll();
        }
        public List<Anamnesis> GetAllAnamnesis()
        {
            return _anamnesisService.GetAll();
        }
        public MedicalRecordDTO? GetOneMedicalRecorByPatientJmbg(String patientJmbg)
        {
            return _medicalRecordService.GetOneByPatientJmbg(patientJmbg);
        }

        public MedicalRecordDTO? GetOneMedicalRecorByAppointmentId(int appointmentId)
        {
            return _medicalRecordService.GetOneByAppointmentId(appointmentId);
        }

        public Anamnesis? GetOneAnamnesisById(int anamnesisId)
        {
            return _anamnesisService.GetOneById(anamnesisId);
        }
        public List<Anamnesis>? GetAllAnamnesisByDoctor(String doctorJmbg)
        {
            return _anamnesisService.GetAllByDoctor(doctorJmbg);
        }
        public List<Anamnesis>? GetAllAnamnesisByPatient(String patientJmbg)
        {
            return _anamnesisService.GetAllByPatient(patientJmbg);
        }
        public List<Prescription> GetAllPrescriptions()
        {
            return _prescriptionService.GetAll();
        }
        public Prescription? GetOnePrescriptionById(int prescriptionId)
        {
            return _prescriptionService.GetOneById(prescriptionId);
        }
        public void ModifyMedicalRecord(String patientJmbg, List<int> prescriptionIds, List<int> anamnesisIds)
        {

            _medicalRecordService.ModifyMedicalRecord(patientJmbg, prescriptionIds, anamnesisIds);
        }
        public void ModifyAnamnesis(int anamnesisId, String diagnosis, String report)
        {
            _anamnesisService.ModifyAnamnesis(anamnesisId, diagnosis, report);
        }
        public void ModifyPrescription(int prescriptonId, String newMedication, String newAmount, int newFrequency, DateTime newFrom, DateTime newTo)
        {

            _prescriptionService.ModifyPrescription(prescriptonId, newMedication, newAmount, newFrequency, newFrom, newTo);

        }
        public void CreateAnamnesis(String patientJmbg, String diagnosis, String report)
        {
            _anamnesisService.CreateAnamnesis(patientJmbg, diagnosis, report);
        }
        public void CreatePrescription(String patientJmbg, String medication, String amount, int frequency, DateTime from, DateTime to)
        {
            _prescriptionService.CreatePrescription(patientJmbg, medication, amount, frequency, from, to);
        }
        public void CreateMedicalRecord(String patientJmbg)
        {
            _medicalRecordService.CreateMedicalRecord(patientJmbg);
        }


    }
}
