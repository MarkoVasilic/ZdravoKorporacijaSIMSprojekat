using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKorporacija.Interfaces
{
    public interface IManagerRepository
    {
        public List<Manager> FindAll();

        public void SaveManager(Manager ManagerToMake);

        public void UpdateManager(Manager ManagerToModify);

        public void RemoveManager(string jmbg);

        public void RemoveAll();

        public Manager? FindOneByJmbg(String jmbg);

        public Manager? FindOneByUsername(string username);





    }
}
