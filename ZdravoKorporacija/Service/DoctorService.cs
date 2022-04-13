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

        public Doctor? GetOneByJmbg(String Jmbg)
        {
            return DoctorRepository.FindOneByJmbg(Jmbg);

        }

        public List<Model.Doctor>? GetDoctorsBySpeciality(String Speciality)
        {
            return DoctorRepository.FindAllBySpeciality(Speciality);
        }

    }
}