using Model;
using System;
using System.Collections.Generic;
using Service;

namespace Controller
{
    public class DoctorController
    {
        private readonly DoctorService DoctorService;

        public DoctorController(DoctorService doctorService)
        {
            DoctorService = doctorService;
        }

        public Doctor GetDoctor(String Jmbg)
        {
            return DoctorService.GetOneByJmbg(Jmbg);
        }

        public List<Model.Doctor> getDoctorsBySpeciality(String Speciality)
        {
            return DoctorService.GetDoctorsBySpeciality(Speciality);
        }

    }
}