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

        public Doctor? GetDoctor(String Jmbg)
        {
            return DoctorService.GetOneByJmbg(Jmbg);
        }

        public List<Model.Doctor>? getDoctorsBySpeciality(String Speciality)
        {
            return DoctorService.GetDoctorsBySpeciality(Speciality);
        }

    }
}