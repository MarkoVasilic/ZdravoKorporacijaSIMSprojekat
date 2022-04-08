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

        public void RemovePatient(long Jmbg)
        {
            var values = GetValues();
            values.RemoveAll(value => value.jmbg.Equals(Jmbg));
            Save(values);
        }

        public Model.Patient? FindOneByJmbg(long Jmbg)
        {
            List<Patient> patients = GetValues();
            foreach (Patient patient in patients)
                if (patient.jmbg.Equals(Jmbg))
                    return patient;

            return null;
        }

        public void Save(List<Patient> values)
        {
            File.WriteAllText(patientFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }

        public List<Patient> GetValues()
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