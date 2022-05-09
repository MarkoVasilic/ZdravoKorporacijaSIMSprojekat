using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
namespace Repository
{
    public class PrescriptionRepository
    {
        private readonly String PrescriptionFilePath = @"..\..\..\Resources\prescriptions.json";

        public List<Prescription> FindAll()
        {
            var values = GetValues();

            return values;
        }

        public Prescription? FindOneById(int prescriptionId)
        {
            var values = GetValues();
            foreach (var val in values)
            {
                if (val.Id == prescriptionId)
                {
                    return val;
                }
            }

            return null;
        }

        public void UpdatePrescription(Prescription prescriptionToModify)
        {
            var onePrescription = FindOneById(prescriptionToModify.Id);
            if (onePrescription != null)
            {
                var values = GetValues();
                values.RemoveAll(value => value.Id.Equals(prescriptionToModify.Id));
                values.Add(prescriptionToModify);
                Save(values);
            }
        }

        public List<Prescription> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<Prescription>>(File.ReadAllText(PrescriptionFilePath));
            if (values == null)
            {
                values = new List<Prescription>();
            }

            return values;
        }

        public void SavePrescription(Prescription prescriptionToSave)
        {
            var values = GetValues();
            values.Add(prescriptionToSave);
            Save(values);
        }

        public void Save(List<Prescription> values)
        {
            File.WriteAllText(PrescriptionFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }
    }
}
