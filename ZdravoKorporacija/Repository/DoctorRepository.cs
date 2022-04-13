using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
    public class DoctorRepository
    {
        private readonly String doctorFilePath = @"..\..\..\Resources\doctors.json";

        public DoctorRepository()
        {
        }

        public Model.Doctor? FindOneByJmbg(String Jmbg)
        {
            List<Model.Doctor> doctors = GetValues();
            foreach (Doctor doctor in doctors)
                if (doctor.Jmbg.Equals(Jmbg))
                    return doctor;
            return null;
        }

        public List<Model.Doctor>? FindAllBySpeciality(String Speciality)
        {
            List<Doctor> doctors = GetValues();
            return doctors.FindAll(doctor => doctor.SpecialtyType.Equals(Speciality));
        }

        private void Save(List<Doctor> values)
        {
            File.WriteAllText(doctorFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }

        private List<Doctor> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<Doctor>>(File.ReadAllText(doctorFilePath));

            if (values == null)
            {
                values = new List<Doctor>();
            }

            return values;
        }
    }
}