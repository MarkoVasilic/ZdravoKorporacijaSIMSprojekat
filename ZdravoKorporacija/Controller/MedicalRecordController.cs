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
        private readonly MedicalRecordService medicalRecordService;
        private readonly AnamnesisService anamnesisService;
        private readonly PrescriptionService prescriptionService;

        public MedicalRecordController(MedicalRecordService medicalRecordService, AnamnesisService anamnesisService, PrescriptionService prescriptionService)
        {
            this.medicalRecordService = medicalRecordService;
            this.anamnesisService = anamnesisService;
            this.prescriptionService = prescriptionService;
        }

        public List<MedicalRecordDTO> GetAllMedicalRecords()
        {
            return medicalRecordService.GetAll();
        }
        public List<Anamnesis> GetAllAnamnesis()
        {
            return anamnesisService.GetAll();
        }
        public MedicalRecordDTO? GetOneMedicalRecorByPatientJmbg(String patientJmbg)
        {
            return medicalRecordService.GetOneByPatientJmbg(patientJmbg);
        }

        public MedicalRecordDTO? GetOneMedicalRecorByAppointmentId(int appointmentId)
        {
            return medicalRecordService.GetOneByAppointmentId(appointmentId);
        }

        public Anamnesis? GetOneAnamnesisById(int anamnesisId)
        {
            return anamnesisService.GetOneById(anamnesisId);
        }
        public List<Anamnesis>? GetAllAnamnesisByDoctor(String doctorJmbg)
        {
            return anamnesisService.GetAllByDoctor(doctorJmbg);
        }
        public List<Anamnesis>? GetAllAnamnesisByPatient(String patientJmbg)
        {
            return anamnesisService.GetAllByPatient(patientJmbg);
        }
        public List<Prescription> GetAllPrescriptions()
        {
            return prescriptionService.GetAll();
        }
        public Prescription? GetOnePrescriptionById(int prescriptionId)
        {
            return prescriptionService.GetOneById(prescriptionId);
        }
        public void ModifyMedicalRecord(String patientJmbg, List<int> prescriptionIds, List<int> anamnesisIds)
        {

            medicalRecordService.ModifyMedicalRecord(patientJmbg, prescriptionIds, anamnesisIds);
        }
        public void ModifyAnamnesis(int anamnesisId, String diagnosis, String report)
        {
            anamnesisService.ModifyAnamnesis(anamnesisId, diagnosis, report);
        }
        public void ModifyPrescription(int prescriptonId, String newMedication, String newAmount, int newFrequency, DateTime newFrom, DateTime newTo)
        {

            prescriptionService.ModifyPrescription(prescriptonId, newMedication, newAmount, newFrequency, newFrom, newTo);

        }
        public void CreateAnamnesis(String patientJmbg, String diagnosis, String report)
        {
            anamnesisService.CreateAnamnesis(patientJmbg, diagnosis, report);
        }
        public void CreatePrescription(String patientJmbg, String medication, String amount, int frequency, DateTime from, DateTime to)
        {
            prescriptionService.CreatePrescription(patientJmbg, medication, amount, frequency, from, to);
        }
        public void CreateMedicalRecord(String patientJmbg)
        {
            medicalRecordService.CreateMedicalRecord(patientJmbg);
        }


    }
}
