using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKorporacija.Model;

namespace ZdravoKorporacija.Repository
{
    public class AnamnesisRepository
    {
        private readonly String AnamnesisFilePath = @"..\..\..\Resources\anamnesis.json";

        public List<Anamnesis> FindAll()
        {
            var values = GetValues();

            return values;
        }

        public Anamnesis? FindOneById(int anamnesisId)
        {
            var values = GetValues();
            foreach (var val in values)
            {
                if (val.Id == anamnesisId)
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
            var values = JsonConvert.DeserializeObject<List<Anamnesis>>(File.ReadAllText(AnamnesisFilePath));
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
            File.WriteAllText(AnamnesisFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }

    }
}
