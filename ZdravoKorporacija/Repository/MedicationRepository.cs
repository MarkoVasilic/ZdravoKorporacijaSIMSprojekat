using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
    public class MedicationRepository
    {

        private readonly String _medicationFilePath = @"..\..\..\Resources\medications.json";


        public List<Medication> FindAll()
        {
            var values = GetValues();

            return values;
        }

        public List<Medication> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<Medication>>(File.ReadAllText(_medicationFilePath));
            if (values == null)
            {
                values = new List<Medication>();
            }

            return values;
        }

        public Medication? FindOneById(int id)
        {
            var values = GetValues();
            foreach (var val in values)
            {
                if (val.Id == id)
                {
                    return val;
                }
            }

            return null;
        }

        public Medication? FindOneByName(String name)
        {
            var values = GetValues();
            foreach (var val in values)
            {
                if (val.Name.Equals(name))
                {
                    return val;
                }
            }

            return null;
        }



        public void Update(Medication medicationToModify)
        {
            var oneMedication = FindOneById(medicationToModify.Id);
            if (oneMedication != null)
            {
                var values = GetValues();
                values.RemoveAll(value => value.Id.Equals(medicationToModify.Id));
                values.Add(medicationToModify);
                Save(values);
            }
        }

        public List<Medication>? FindAllUnverified()
        {
            var values = GetValues();
            List<Medication> result = new List<Medication>();
            foreach (Medication medication in values)
                if (medication.Status == MedicationStatus.UNVERIFIED)
                    result.Add(medication);
            return result;
        }

        public List<Medication>? FindAllRejected()
        {
            var values = GetValues();
            List<Medication> result = new List<Medication>();
            foreach (Medication medication in values)
                if (medication.Status == MedicationStatus.REJECTED)
                    result.Add(medication);
            return result;
        }

        public List<Medication>? FindAllVerified()
        {
            var values = GetValues();
            List<Medication> result = new List<Medication>();
            foreach (Medication medication in values)
                if (medication.Status == MedicationStatus.VERIFIED)
                    result.Add(medication);
            return result;
        }

        public void SaveMedication(Medication medicationToSave)
        {
            var values = GetValues();
            values.Add(medicationToSave);
            Save(values);
        }

        public void Save(List<Medication> values)
        {
            File.WriteAllText(_medicationFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }

    }
}
