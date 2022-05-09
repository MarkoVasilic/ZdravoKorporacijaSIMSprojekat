using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using ZdravoKorporacija.Model;

namespace ZdravoKorporacija.Repository
{
    public class DisplacementRepository
    {

        private readonly String DisplacementsFilePath = @"..\..\..\Resources\displacements.json";

        public void SaveDisplacement(Displacement displacementToMake)
        {
            var values = GetValues();
            values.Add(displacementToMake);
            Save(values);
        }


        public void Save(List<Displacement> values)
        {
            File.WriteAllText(DisplacementsFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }

        public List<Displacement> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<Displacement>>(File.ReadAllText(DisplacementsFilePath));

            if (values == null)
            {
                values = new List<Displacement>();
            }

            return values;
        }

        public List<Displacement> FindAll()
        {
            var values = GetValues();
            return values;
        }

        public Model.Displacement? FindOneById(int? id)
        {
            List<Displacement> displacements = GetValues();
            foreach (Displacement dis in displacements)
            {
                if (dis.Id == id)
                    return dis;
            }
            return null;
        }
    }
}
