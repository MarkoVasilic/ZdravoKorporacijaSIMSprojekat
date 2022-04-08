using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
    public class PatientRepository
    {
        private String patientFilePath = @"..\..\..\Resources\patients.json";

        public PatientRepository()
        {
        }

        public List<Patient> FindAll()
        {
            var values = GetValues();
            return values;
        }

        public void SavePatient(Patient PacientToMake)
        {
            var values = GetValues();
            values.Add(PacientToMake);
            Save(values);
        }

        public void RemovePatient(string Jmbg)
        {
            var values = GetValues();
            values.RemoveAll(value => value.jmbg.Equals(Jmbg));
            Save(values);
        }

        public Patient? FindOneByJmbg(string Jmbg)
        {
            List<Patient> patients = GetValues();
            foreach (Patient patient in patients)
                if (patient.jmbg.Equals(Jmbg))
                    return patient;

            return null;
        }

        private void Save(List<Patient> values)
        {
            File.WriteAllText(patientFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }

        private List<Patient> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText(patientFilePath));

            if (values == null)
            {
                values = new List<Patient>();
            }

            return values;
        }

    }
}