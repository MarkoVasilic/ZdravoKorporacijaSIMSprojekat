using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ZdravoKorporacija.Repository
{
    public class AnamnesisRepository
    {
        private readonly String _anamnesisFilePath = @"..\..\..\Resources\anamnesis.json";

        public List<Anamnesis> FindAll()
        {
            var values = GetValues();

            return values;
        }

        public Anamnesis? FindOneById(int id)
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
        public List<Anamnesis>? FindAllByDoctor(String doctorJmbg)
        {
            var values = GetValues();
            List<Anamnesis> result = new List<Anamnesis>();
            foreach (Anamnesis anamnesis in values)
                if (anamnesis.DoctorJmbg.Equals(doctorJmbg))
                    result.Add(anamnesis);
            return result;
        }
        public List<Anamnesis> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<Anamnesis>>(File.ReadAllText(_anamnesisFilePath));
            if (values == null)
            {
                values = new List<Anamnesis>();
            }

            return values;
        }

        public void UpdateAnamnesis(Anamnesis anamnesisToModify)
        {
            var oneAnamnesis = FindOneById(anamnesisToModify.Id);
            if (oneAnamnesis != null)
            {
                var values = GetValues();
                values.RemoveAll(value => value.Id.Equals(anamnesisToModify.Id));
                values.Add(anamnesisToModify);
                Save(values);
            }
        }

        public void SaveAnamnesis(Anamnesis anamnesisToSave)
        {
            var values = GetValues();
            values.Add(anamnesisToSave);
            Save(values);
        }

        public void Save(List<Anamnesis> values)
        {
            File.WriteAllText(_anamnesisFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }

    }
}
