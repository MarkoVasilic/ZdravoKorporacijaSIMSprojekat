using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKorporacija.DTO;
using Model;
using Service;

namespace Controller
{
    public class MedicalRecordController
    {
        private readonly MedicalRecordService MedicalRecordService;
        private readonly AnamnesisService AnamnesisService;
        private readonly PrescriptionService PrescriptionService;

        public MedicalRecordController(MedicalRecordService medicalRecordService, AnamnesisService anamnesisService, PrescriptionService prescriptionService)
        {
            MedicalRecordService = medicalRecordService;
            AnamnesisService = anamnesisService;
            PrescriptionService = prescriptionService;
        }

        public List<MedicalRecordDTO> GetAllMedicalRecords()
        {
            return MedicalRecordService.GetAll();
        }
        public List<Anamnesis> GetAllAnamnesis()
        {
            return AnamnesisService.GetAll();
        }
        public MedicalRecordDTO? GetOneMedicalRecorByPatientJmbg(String patientJmbg)
        {
            return MedicalRecordService.GetOneByPatientJmbg(patientJmbg);
        }

        public MedicalRecordDTO? GetOneMedicalRecorByAppointmentId(int appointmentId)
        {
            return MedicalRecordService.GetOneByAppointmentId(appointmentId);
        }

        public Anamnesis? GetOneAnamnesisById(int anamnesisId)
        {
            return AnamnesisService.GetOneById(anamnesisId);
        }
        public List<Anamnesis>? GetAllAnamnesisByDoctor(String doctorJmbg)
        {
            return AnamnesisService.GetAllByDoctor(doctorJmbg);
        }
        public List<Anamnesis>? GetAllAnamnesisByPatient(String patientJmbg)
        {
            return AnamnesisService.GetAllByPatient(patientJmbg);
        }
        public List<Prescription> GetAllPrescriptions()
        {
            return PrescriptionService.GetAll();
        }
        public Prescription? GetOnePrescriptionById(int prescriptionId)
        {
            return PrescriptionService.GetOneById(prescriptionId);
        }
        public void ModifyMedicalRecord(String patientJmbg, List<int> prescriptionIds, List<int> anamnesisIds)
        {

            MedicalRecordService.ModifyMedicalRecord(patientJmbg, prescriptionIds, anamnesisIds);
        }
        public void ModifyAnamnesis(int anamnesisId, String diagnosis, String report)
        {
            AnamnesisService.ModifyAnamnesis(anamnesisId, diagnosis, report);
        }
        public void ModifyPrescription(int prescriptonId, String newMedication, String newAmount, int newFrequency, DateTime newFrom, DateTime newTo)
        {

            PrescriptionService.ModifyPrescription(prescriptonId, newMedication, newAmount, newFrequency, newFrom, newTo);

        }
        public void CreateAnamnesis(String patientJmbg, String diagnosis, String report)
        {
            AnamnesisService.CreateAnamnesis(patientJmbg, diagnosis, report);
        }
        public void CreatePrescription(String patientJmbg, String medication, String amount, int frequency, DateTime from, DateTime to)
        {
            PrescriptionService.CreatePrescription(patientJmbg, medication, amount, frequency, from, to);
        }
        public void CreateMedicalRecord(String patientJmbg)
        {
            MedicalRecordService.CreateMedicalRecord(patientJmbg);
        }


    }
}
