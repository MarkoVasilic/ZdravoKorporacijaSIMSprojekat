using System;
using System.Collections.Generic;
using System.IO;
using Model;
using Newtonsoft.Json;

namespace Repository
{
    public class DoctorRepository
    {
        private String doctorFilePath = @"..\..\..\Resources\doctors.json";

        private List<Doctor> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<Doctor>>(File.ReadAllText(doctorFilePath));

            if (values == null)
            {
                values = new List<Doctor>();
            }

            return values;
        }

        public Model.Doctor FindOneByJmbg(String Jmbg)
        {
            var values = GetValues();
            foreach (var val in values)
            {
                if (val.Jmbg == Jmbg)
                {
                    return val;
                }
            }

            return null;
        }

        public List<Model.Doctor> FindAllBySpeciality(String Speciality)
        {
            throw new NotImplementedException();
        }
    }
}