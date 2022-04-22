using Model;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
    public class DoctorService
    {
        private readonly DoctorRepository DoctorRepository;

        public DoctorService(DoctorRepository doctorRepository)
        {
            this.DoctorRepository = doctorRepository;
        }

        public Doctor? GetOneByJmbg(String jmbg)
        {
            return DoctorRepository.FindOneByJmbg(jmbg);

        }

        public List<Doctor>? GetAllBySpeciality(String speciality)
        {
            return DoctorRepository.FindAllBySpeciality(speciality);
        }

        public List<String> GetAllSpecialities()
        {
            List<Doctor> doctors = DoctorRepository.FindAll();
            List<String> specialities = new List<String>();
            foreach (var doctor in doctors)
            {
                if (!specialities.Contains(doctor.SpecialtyType))
                    specialities.Add(doctor.SpecialtyType);
            }
            return specialities;
        }

        public List<Doctor> GetAll()
        {
            return DoctorRepository.FindAll();
        }
 
    }
}