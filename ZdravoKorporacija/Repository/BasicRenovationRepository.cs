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
    public class BasicRenovationRepository
    {

        private readonly String BasicRenovationsFilePath = @"..\..\..\Resources\basicRenovations.json";



        public void Save(List<BasicRenovation> values)
        {
            File.WriteAllText(BasicRenovationsFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }

        public List<BasicRenovation> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<BasicRenovation>>(File.ReadAllText(BasicRenovationsFilePath));

            if (values == null)
            {
                values = new List<BasicRenovation>();
            }

            return values;
        }

        public List<BasicRenovation> FindAll()
        {
            var values = GetValues();
            return values;
        }

        public void SaveBasicRenovation(BasicRenovation renovationToMake)
        {
            var values = GetValues();
            values.Add(renovationToMake);
            Save(values);
        }

        public void RemoveBasicRenovation(int renovationId)
        {
            var values = GetValues();
            values.RemoveAll(val => val.Id == renovationId);
            Save(values);
        }

        public Model.BasicRenovation? FindOneById(int? id)
        {
            List<BasicRenovation> basicRenovations = GetValues();
            foreach (BasicRenovation bs in basicRenovations)
            {
                if (bs.Id == id)
                    return bs;
            }
            return null;
        }

    }
}
