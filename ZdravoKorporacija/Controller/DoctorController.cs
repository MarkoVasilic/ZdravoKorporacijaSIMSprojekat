using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class DoctorController
    {
        private readonly DoctorService _doctorService;

        public DoctorController(DoctorService doctorService)
        {
            this._doctorService = doctorService;
        }

        public Doctor? GetOneDoctor(String jmbg)
        {
            return _doctorService.GetOneByJmbg(jmbg);
        }

        public List<Model.Doctor>? getAllBySpeciality(String speciality)
        {
            return _doctorService.GetAllBySpeciality(speciality);
        }

        public List<String> GetAllSpecialities()
        {
            return _doctorService.GetAllSpecialities();
        }

        public List<Doctor> GetAllDoctors()
        {
            return _doctorService.GetAllDoctors();
        }

        public Doctor? getDoctorByUsername(String username)
        {
            return _doctorService.GetOneByUsername(username);
        }

        public void CreateDoctor(bool speciality, String specialityType, string firstName, int roomId, string lastName, string username, string password,
            string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? phoneNumber,
            string? address)
        {
            _doctorService.CreateDoctor(speciality, specialityType, firstName, roomId, lastName, username, password,
            jmbg, dateOfBirth, gender, email, phoneNumber, address);
        }

        public void DeleteDoctor(string jmbg)
        {
            _doctorService.DeleteDoctor(jmbg);
        }

    }
}