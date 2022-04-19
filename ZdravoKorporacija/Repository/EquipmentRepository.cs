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
    public class EquipmentRepository
    {


        private readonly String EquipmentFilePath = @"..\..\..\Resources\equipment.json";


        public void Save(List<Equipment> values)
        {
            File.WriteAllText(EquipmentFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }

        public List<Equipment> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<Equipment>>(File.ReadAllText(EquipmentFilePath));

            if (values == null)
            {
                values = new List<Equipment>();
            }

            return values;
        }

    }
}
