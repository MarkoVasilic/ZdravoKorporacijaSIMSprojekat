using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class DoctorController
    {
        private readonly DoctorService DoctorService;

        public DoctorController(DoctorService doctorService)
        {
            this.DoctorService = doctorService;
        }

        public Doctor? GetOneDoctor(String jmbg)
        {
            return DoctorService.GetOneByJmbg(jmbg);
        }

        public List<Model.Doctor>? getAllBySpeciality(String speciality)
        {
            return DoctorService.GetAllBySpeciality(speciality);
        }

        public List<String> GetAllSpecialities()
        {
            return DoctorService.GetAllSpecialities();
        }

        public List<Doctor> GetAll()
        {
            return DoctorService.GetAll();
        }

    }
}