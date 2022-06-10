using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using ZdravoKorporacija.Interfaces;

namespace Repository
{
    public class SecretaryRepository : ISecretaryRepository
    {
        private readonly String _secretaryFilePath = @"..\..\..\Resources\secretary.json";

        public SecretaryRepository()
        {
        }

        public List<Secretary> FindAll()
        {
            var values = GetValues();
            return values;
        }

        public void SaveSecretary(Secretary SecretaryToMake)
        {
            var values = GetValues();
            values.Add(SecretaryToMake);
            Save(values);
        }

        public void UpdateSecretary(Secretary SecretaryToModify)
        {
            var oneSecretary = FindOneByJmbg(SecretaryToModify.Jmbg);
            if (oneSecretary != null)
            {
                var values = GetValues();
                values.RemoveAll(value => value.Jmbg.Equals(oneSecretary.Jmbg));
                values.Add(SecretaryToModify);
                Save(values);
            }
        }

        public void RemoveSecretary(string jmbg)
        {
            var values = GetValues();
            values.RemoveAll(value => value.Jmbg.Equals(jmbg));
            Save(values);
        }

        public void RemoveAll()
        {
            var values = GetValues();
            values.Clear();
            Save(values);
        }

        public Secretary? FindOneByJmbg(String jmbg)
        {
            List<Secretary> secretaries = GetValues();
            foreach (Secretary secretary in secretaries)
                if (secretary.Jmbg.Equals(jmbg))
                    return secretary;

            return null;
        }

        public Secretary? FindOneByUsername(string username)
        {
            List<Secretary> secretaries = GetValues();
            foreach (Secretary secretary in secretaries)
                if (secretary.Username.Equals(username))
                    return secretary;

            return null;
        }

        private void Save(List<Secretary> values)
        {
            File.WriteAllText(_secretaryFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }

        private List<Secretary> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<Secretary>>(File.ReadAllText(_secretaryFilePath));

            if (values == null)
            {
                values = new List<Secretary>();
            }

            return values;
        }
    }
}