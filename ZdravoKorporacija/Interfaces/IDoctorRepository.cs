using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKorporacija.Interfaces
{
    public interface IDoctorRepository
    {
        public Doctor? FindOneByJmbg(String jmbg);

        public Doctor? FindOneByUsername(string username);

        public void SaveDoctor(Doctor DoctorToMake);

        public void RemoveDoctor(string jmbg);

        public void RemoveAll();

        public List<Doctor>? FindAllBySpeciality(String speciality);

        public List<Doctor> FindAll();
    }
}
