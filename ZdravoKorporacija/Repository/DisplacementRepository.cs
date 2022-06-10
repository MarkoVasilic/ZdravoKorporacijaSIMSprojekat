using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using ZdravoKorporacija.Interfaces;
using ZdravoKorporacija.Model;

namespace ZdravoKorporacija.Repository
{
    public class DisplacementRepository : IDisplacementRepository
    {

        private readonly String _displacementsFilePath = @"..\..\..\Resources\displacements.json";

        public void SaveDisplacement(Displacement displacementToMake)
        {
            var values = GetValues();
            values.Add(displacementToMake);
            Save(values);
        }


        public void Save(List<Displacement> values)
        {
            File.WriteAllText(_displacementsFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }

        public List<Displacement> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<Displacement>>(File.ReadAllText(_displacementsFilePath));

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

        public void RemoveDisplacementByStartRoomId(int roomId)
        {
            var values = GetValues();
            values.RemoveAll(val => val.StartRoom == roomId);
            Save(values);
        }
        public void RemoveDisplacementByEndRoomId(int roomId)
        {
            var values = GetValues();
            values.RemoveAll(val => val.EndRoom == roomId);
            Save(values);
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
