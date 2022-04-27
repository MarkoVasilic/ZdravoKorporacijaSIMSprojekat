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

        public List<Doctor> GetAllDoctors()
        {
            return DoctorService.GetAllDoctors();
        }

        public Doctor? getDoctorByUsername(String username)
        {
            return DoctorService.GetOneByUsername(username);
        }

        public void CreateDoctor(bool speciality, String specialityType, string firstName, int roomId, string lastName, string username, string password,
            string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? phoneNumber,
            string? address)
        {
            DoctorService.CreateDoctor(speciality, specialityType, firstName, roomId, lastName, username, password,
            jmbg, dateOfBirth, gender, email, phoneNumber, address);
        }

        public void DeleteDoctor(string jmbg)
        {
            DoctorService.DeleteDoctor(jmbg);
        }

    }
}