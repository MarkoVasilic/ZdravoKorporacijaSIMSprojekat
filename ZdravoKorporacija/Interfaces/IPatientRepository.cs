using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKorporacija.Interfaces
{
    public interface IPatientRepository
    {
        public List<Patient> FindAll();

        public void SavePatient(Patient patientToMake);

        public void UpdatePatient(Patient patientToModify);

        public void RemovePatient(string jmbg);

        public void RemoveAll();

        public Patient? FindOneByJmbg(String jmbg);

        public Patient? FindOneByUsername(string username);

    }
}
