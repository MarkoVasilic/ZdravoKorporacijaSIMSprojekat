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

        public List<Equipment> FindAll()
        {
            var values = GetValues();
            return values;
        }

        public List<Equipment> FindAllByRoomId(int roomId)
        {
            List<Equipment> values = new List<Equipment>();
            List <Equipment> equipments = GetValues();
            foreach(Equipment eq in equipments)
            {
                if(eq.RoomId == roomId)
                    values.Add(eq);
            }
            return values;
            
        }

        public void SaveEquipment(Equipment equipmentToMake)
        {
            var values = GetValues();
            values.Add(equipmentToMake);
            Save(values);
        }

        public Model.Equipment? FindOneById(int? id)
        {
            List<Equipment> equipment = GetValues();
            foreach (Equipment eq in equipment)
            {
                if (eq.Id == id)
                    return eq;
            }
            return null;
        }

        public Model.Equipment? FindOneByRoomId(int? roomId)
        {
            List<Equipment> equipment = GetValues();
            foreach (Equipment eq in equipment)
            {
                if (eq.RoomId == roomId)
                    return eq;
            }
            return null;
        }

        public Model.Equipment? FindOneByName(String name)
        {
            List<Equipment> equipment = GetValues();
            foreach (Equipment eq in equipment)
            {
                if (eq.Name == name)
                    return eq;
            }
            return null;
        }


        public void RemoveEquipment(int equipmentId)
        {
            var values = GetValues();
            values.RemoveAll(val => val.Id == equipmentId);
            Save(values);
        }

        public void RemoveEquipmentByRoom(int RoomId)
        {
            var values = GetValues();
            values.RemoveAll(val => val.RoomId == RoomId);
            Save(values);
        }

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
