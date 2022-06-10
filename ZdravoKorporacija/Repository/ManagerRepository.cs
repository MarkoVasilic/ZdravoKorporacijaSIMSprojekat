using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using ZdravoKorporacija.Interfaces;

namespace ZdravoKorporacija.Repository
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly String _managerFilePath = @"..\..\..\Resources\manager.json";

        public ManagerRepository()
        {
        }

        public List<Manager> FindAll()
        {
            var values = GetValues();
            return values;
        }

        public void SaveManager(Manager ManagerToMake)
        {
            var values = GetValues();
            values.Add(ManagerToMake);
            Save(values);
        }

        public void UpdateManager(Manager ManagerToModify)
        {
            var oneManager = FindOneByJmbg(ManagerToModify.Jmbg);
            if (oneManager != null)
            {
                var values = GetValues();
                values.RemoveAll(value => value.Jmbg.Equals(oneManager.Jmbg));
                values.Add(ManagerToModify);
                Save(values);
            }
        }

        public void RemoveManager(string jmbg)
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

        public Manager? FindOneByJmbg(String jmbg)
        {
            List<Manager> managers = GetValues();
            foreach (Manager manager in managers)
                if (manager.Jmbg.Equals(jmbg))
                    return manager;

            return null;
        }

        public Manager? FindOneByUsername(string username)
        {
            List<Manager> managers = GetValues();
            foreach (Manager manager in managers)
                if (manager.Username.Equals(username))
                    return manager;

            return null;
        }

        private void Save(List<Manager> values)
        {
            File.WriteAllText(_managerFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }

        private List<Manager> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<Manager>>(File.ReadAllText(_managerFilePath));

            if (values == null)
            {
                values = new List<Manager>();
            }

            return values;
        }
    }
}
