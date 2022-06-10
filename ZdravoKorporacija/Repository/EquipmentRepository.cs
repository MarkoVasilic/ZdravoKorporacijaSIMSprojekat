using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using ZdravoKorporacija.Interfaces;
using ZdravoKorporacija.Model;

namespace ZdravoKorporacija.Repository
{
    public class EquipmentRepository : IEquipmentRepository
    {


        private readonly String _equipmentFilePath = @"..\..\..\Resources\equipment.json";

        public List<Equipment> FindAll()
        {
            var values = GetValues();
            return values;
        }

        public List<Equipment> FindAllByRoomId(int roomId)
        {
            List<Equipment> values = new List<Equipment>();
            List<Equipment> equipments = GetValues();
            foreach (Equipment eq in equipments)
            {
                if (eq.RoomId == roomId)
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

        public void UpdateEquipment(Equipment equipmentToModify)
        {
            var oneEquipment = FindOneById(equipmentToModify.Id);
            if (oneEquipment != null)
            {
                var values = GetValues();
                values.RemoveAll(value => value.Id.Equals(oneEquipment.Id));
                values.Add(equipmentToModify);
                Save(values);
            }
        }


        public void RemoveEquipment(int equipmentId)
        {
            var values = GetValues();
            values.RemoveAll(val => val.Id == equipmentId);
            Save(values);
        }

        public void RemoveEquipmentByRoom(int roomId)
        {
            var values = GetValues();
            values.RemoveAll(val => val.RoomId == roomId);
            Save(values);
        }

        public void Save(List<Equipment> values)
        {
            File.WriteAllText(_equipmentFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }

        public List<Equipment> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<Equipment>>(File.ReadAllText(_equipmentFilePath));

            if (values == null)
            {
                values = new List<Equipment>();
            }

            return values;
        }

    }
}
