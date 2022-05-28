using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using ZdravoKorporacija.Model;

namespace ZdravoKorporacija.Repository
{
    public class AdvancedRenovationSeparationRepository
    {

        private readonly String SeparationFilePath = @"..\..\..\Resources\advancedRenovationSeparation.json";

        public void Save(List<AdvancedRenovationSeparation> values)
        {
            File.WriteAllText(SeparationFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }

        public List<AdvancedRenovationSeparation> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<AdvancedRenovationSeparation>>(File.ReadAllText(SeparationFilePath));

            if (values == null)
            {
                values = new List<AdvancedRenovationSeparation>();
            }

            return values;
        }


        public List<AdvancedRenovationSeparation> FindAll()
        {
            var values = GetValues();
            return values;
        }

        public void SaveSeparation(AdvancedRenovationSeparation renovationToMake)
        {
            var values = GetValues();
            values.Add(renovationToMake);
            Save(values);
        }

        public void RemoveSeparation(int renovationId)
        {
            var values = GetValues();
            values.RemoveAll(val => val.Id == renovationId);
            Save(values);
        }

        public Model.AdvancedRenovationSeparation? FindOneById(int? id)
        {
            List<AdvancedRenovationSeparation> advancedRenovationSeparations = GetValues();
            foreach (AdvancedRenovationSeparation ars in advancedRenovationSeparations)
            {
                if (ars.Id == id)
                    return ars;
            }
            return null;
        }

        public List<AdvancedRenovationSeparation> FindAllByRoomId(int id)
        {
            List<AdvancedRenovationSeparation> advancedRenovationSeparations = GetValues();
            List<AdvancedRenovationSeparation> renovationSeparations = new List<AdvancedRenovationSeparation>();
            foreach (var ars in advancedRenovationSeparations)
            {
                if (ars.StartRoomId == id)
                    renovationSeparations.Add(ars);
            }

            return renovationSeparations;
        }
    }
}
