using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ZdravoKorporacija.Repository
{
    public class MedicalRecordRepository
    {
        private readonly String _medicalRecordFilePath = @"..\..\..\Resources\medicalRecords.json";

        public List<MedicalRecord> FindAll()
        {
            var values = GetValues();

            return values;
        }

        public MedicalRecord? FindOneByPatientJmbg(String patientJmbg)
        {
            var values = GetValues();
            foreach (var val in values)
            {
                if (val.PatientJmbg.Equals(patientJmbg))
                {
                    return val;
                }
            }

            return null;
        }

        public List<MedicalRecord> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<MedicalRecord>>(File.ReadAllText(_medicalRecordFilePath));
            if (values == null)
            {
                values = new List<MedicalRecord>();
            }

            return values;
        }

        public void UpdateMedicalRecord(MedicalRecord medicalRecordToModify)
        {
            var oneMedicalRecord = FindOneByPatientJmbg(medicalRecordToModify.PatientJmbg);
            if (oneMedicalRecord != null)
            {
                var values = GetValues();
                values.RemoveAll(value => value.PatientJmbg.Equals(medicalRecordToModify.PatientJmbg));
                values.Add(medicalRecordToModify);
                Save(values);
            }
        }

        public void SaveMedicalRecord(MedicalRecord medicalRecordToSave)
        {
            var values = GetValues();
            values.Add(medicalRecordToSave);
            Save(values);
        }

        public void Save(List<MedicalRecord> values)
        {
            File.WriteAllText(_medicalRecordFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }

    }
}
