using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
    public class PatientRepository
    {
        private readonly String PatientFilePath = @"..\..\..\Resources\patients.json";

        public PatientRepository()
        {
        }

        public List<Patient> FindAll()
        {
            var values = GetValues();
            return values;
        }

        public void SavePatient(Patient PatientToMake)
        {
            var values = GetValues();
            values.Add(PatientToMake);
            Save(values);
        }

        public void UpdatePatient(Patient PatientToModify)
        {
            var onePatient = FindOneByJmbg(PatientToModify.Jmbg);
            if (onePatient != null)
            {
                var values = GetValues();
                values.RemoveAll(value => value.Jmbg.Equals(onePatient.Jmbg));
                values.Add(PatientToModify);
                Save(values);
            }
        }

        public void RemovePatient(string Jmbg)
        {
            var values = GetValues();
            values.RemoveAll(value => value.Jmbg.Equals(Jmbg));
            Save(values);
        }

        public void RemoveAll()
        {
            var values = GetValues();
            values.Clear();
            Save(values);
        }

        public Patient? FindOneByJmbg(string Jmbg)
        {
            List<Patient> patients = GetValues();
            foreach (Patient patient in patients)
                if (patient.Jmbg.Equals(Jmbg))
                    return patient;

            return null;
        }

        private void Save(List<Patient> values)
        {
            File.WriteAllText(PatientFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }

        private List<Patient> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText(PatientFilePath));

            if (values == null)
            {
                values = new List<Patient>();
            }

            return values;
        }

    }
}