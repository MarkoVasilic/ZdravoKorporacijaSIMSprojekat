using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKorporacija.Interfaces
{
    public interface ISecretaryRepository
    {
        public List<Secretary> FindAll();

        public void SaveSecretary(Secretary SecretaryToMake);

        public void UpdateSecretary(Secretary SecretaryToModify);

        public void RemoveSecretary(string jmbg);

        public void RemoveAll();

        public Secretary? FindOneByJmbg(String jmbg);

        public Secretary? FindOneByUsername(string username);





    }
}
