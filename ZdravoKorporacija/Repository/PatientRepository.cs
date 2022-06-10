using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using ZdravoKorporacija.Interfaces;

namespace Repository
{
    public class PatientRepository : IPatientRepository

    {
        private readonly String _patientFilePath = @"..\..\..\Resources\patients.json";

        public PatientRepository()
        {
        }

        public List<Patient> FindAll()
        {
            var values = GetValues();
            return values;
        }


        public void SavePatient(Patient patientToMake)
        {
            var values = GetValues();
            values.Add(patientToMake);
            Save(values);
        }

        public void UpdatePatient(Patient patientToModify)
        {
            var onePatient = FindOneByJmbg(patientToModify.Jmbg);
            if (onePatient != null)
            {
                var values = GetValues();
                values.RemoveAll(value => value.Jmbg.Equals(onePatient.Jmbg));
                values.Add(patientToModify);
                Save(values);
            }
        }

        public void RemovePatient(string jmbg)
        {
            var values = GetValues();
            values.RemoveAll(value => value.Jmbg.Equals(jmbg));
            Save(values);
        }

        public void RemoveAll()
        {
            var values = GetValues();
            values.Clear();
            Save(values);
        }

        public Patient? FindOneByJmbg(String jmbg)
        {
            List<Patient> patients = GetValues();
            foreach (Patient patient in patients)
                if (patient.Jmbg.Equals(jmbg))
                    return patient;

            return null;
        }

        public Patient? FindOneByUsername(string username)
        {
            List<Patient> patients = GetValues();
            foreach (Patient patient in patients)
                if (patient.Username.Equals(username))
                    return patient;

            return null;
        }

        private void Save(List<Patient> values)
        {
            File.WriteAllText(_patientFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }

        private List<Patient> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<Patient>>(File.ReadAllText(_patientFilePath));

            if (values == null)
            {
                values = new List<Patient>();
            }

            return values;
        }

    }
}