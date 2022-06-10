using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using ZdravoKorporacija.Interfaces;
using ZdravoKorporacija.Model;

namespace ZdravoKorporacija.Repository
{
    public class BasicRenovationRepository : IBasicRenovationRepository
    {

        private readonly String _basicRenovationsFilePath = @"..\..\..\Resources\basicRenovations.json";



        private void Save(List<BasicRenovation> values)
        {
            File.WriteAllText(_basicRenovationsFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }

        private List<BasicRenovation> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<BasicRenovation>>(File.ReadAllText(_basicRenovationsFilePath));

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

        public List<BasicRenovation> FindAllByRoomId(int id)
        {
            var values = GetValues();
            List<BasicRenovation> result = new List<BasicRenovation>();
            foreach (BasicRenovation basicRenovation in values)
                if (basicRenovation.RoomId.Equals(id))
                    result.Add(basicRenovation);
            return result;
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

        public void RemoveBasicRenovationByRoomId(int roomId)
        {
            var values = GetValues();
            values.RemoveAll(val => val.RoomId == roomId);
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
