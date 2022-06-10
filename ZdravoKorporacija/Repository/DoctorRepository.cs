using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using ZdravoKorporacija.Interfaces;

namespace Repository
{
    public class DoctorRepository:IDoctorRepository
    {
        private readonly String _doctorFilePath = @"..\..\..\Resources\doctors.json";

        public DoctorRepository()
        {
        }

        public Model.Doctor? FindOneByJmbg(String jmbg)
        {
            List<Model.Doctor> doctors = GetValues();
            foreach (Doctor doctor in doctors)
                if (doctor.Jmbg.Equals(jmbg))
                    return doctor;
            return null;
        }

        public Doctor? FindOneByUsername(string username)
        {
            List<Doctor> doctors = GetValues();
            foreach (Doctor doctor in doctors)
                if (doctor.Username.Equals(username))
                    return doctor;

            return null;
        }

        public void SaveDoctor(Doctor DoctorToMake)
        {
            var values = GetValues();
            values.Add(DoctorToMake);
            Save(values);
        }

        public void RemoveDoctor(string jmbg)
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

        public List<Model.Doctor>? FindAllBySpeciality(String speciality)
        {
            List<Doctor> doctors = GetValues();
            return doctors.FindAll(doctor => doctor.SpecialtyType.Equals(speciality));
        }

        private void Save(List<Doctor> values)
        {
            File.WriteAllText(_doctorFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }

        public List<Doctor> FindAll()
        {
            var values = GetValues();
            return values;
        }

        private List<Doctor> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<Doctor>>(File.ReadAllText(_doctorFilePath));

            if (values == null)
            {
                values = new List<Doctor>();
            }

            return values;
        }
    }
}