﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using ZdravoKorporacija.Model;

namespace ZdravoKorporacija.Repository
{
    public class AdvancedRenovationJoiningRepository
    {

        private readonly String JoiningFilePath = @"..\..\..\Resources\advancedRenovationJoining.json";


        public void Save(List<AdvancedRenovationJoining> values)
        {
            File.WriteAllText(JoiningFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }

        public List<AdvancedRenovationJoining> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<AdvancedRenovationJoining>>(File.ReadAllText(JoiningFilePath));

            if (values == null)
            {
                values = new List<AdvancedRenovationJoining>();
            }

            return values;
        }


        public List<AdvancedRenovationJoining> FindAll()
        {
            var values = GetValues();
            return values;
        }

        public void SaveJoining(AdvancedRenovationJoining renovationToMake)
        {
            var values = GetValues();
            values.Add(renovationToMake);
            Save(values);
        }

        public void RemoveJoining(int renovationId)
        {
            var values = GetValues();
            values.RemoveAll(val => val.Id == renovationId);
            Save(values);
        }

        public Model.AdvancedRenovationJoining? FindOneById(int? id)
        {
            List<AdvancedRenovationJoining> advancedRenovationJoining = GetValues();
            foreach (AdvancedRenovationJoining arj in advancedRenovationJoining)
            {
                if (arj.Id == id)
                    return arj;
            }
            return null;
        }

        public List<AdvancedRenovationJoining> FindAllByRoomId(int id)
        {
            List<AdvancedRenovationJoining> advancedRenovationJoining = GetValues();
            List<AdvancedRenovationJoining> renovationJoinings = new List<AdvancedRenovationJoining>();
            foreach (var arj in advancedRenovationJoining)
            {
                if (arj.FirstStartRoom == id || arj.SecondStartRoom == id)
                    renovationJoinings.Add(arj);
            }

            return renovationJoinings;
        }
    }
}
