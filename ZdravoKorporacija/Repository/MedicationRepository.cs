using System;
using Model;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Model;

namespace Repository
{
    public class MedicationRepository
    {

        private readonly String MedicationFilePath = @"..\..\..\Resources\medications.json";


        public List<Medication> FindAll()
        {
            var values = GetValues();

            return values;
        }

        public List<Medication> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<Medication>>(File.ReadAllText(MedicationFilePath));
            if (values == null)
            {
                values = new List<Medication>();
            }

            return values;
        }

        public Medication? FindOneById(int medicationId)
        {
            var values = GetValues();
            foreach (var val in values)
            {
                if (val.Id == medicationId)
                {
                    return val;
                }
            }

            return null;
        }

        public Medication? FindOneByName(String medicationName)
        {
            var values = GetValues();
            foreach (var val in values)
            {
                if (val.Name.Equals(medicationName))
                {
                    return val;
                }
            }

            return null;
        }

        public void UpdateMedication(Medication medicationToModify)
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

        public void SaveMedication(Medication medicationToSave)
        {
            var values = GetValues();
            values.Add(medicationToSave);
            Save(values);
        }

        public void Save(List<Medication> values)
        {
            File.WriteAllText(MedicationFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }

    }
}
