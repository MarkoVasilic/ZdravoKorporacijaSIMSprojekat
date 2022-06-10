using Model;
using Repository;
using System;
using System.Collections.Generic;
using ZdravoKorporacija.Interfaces;

namespace Service
{
    public class DoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            this._doctorRepository = doctorRepository;
        }

        public Doctor? GetOneByJmbg(String jmbg)
        {
            return _doctorRepository.FindOneByJmbg(jmbg);

        }

        public List<Doctor>? GetAllBySpeciality(String speciality)
        {
            return _doctorRepository.FindAllBySpeciality(speciality);
        }

        public List<String> GetAllSpecialities()
        {
            List<Doctor> doctors = _doctorRepository.FindAll();
            List<String> specialities = new List<String>();
            foreach (var doctor in doctors)
            {
                if (!specialities.Contains(doctor.SpecialtyType))
                    specialities.Add(doctor.SpecialtyType);
            }
            return specialities;
        }

        public List<Doctor> GetAllDoctors()
        {
            return _doctorRepository.FindAll();
        }

        public Doctor? GetOneByUsername(String username)
        {
            return _doctorRepository.FindOneByUsername(username);

        }

        public void CreateDoctor(bool speciality, String specialityType, string firstName, int roomId, string lastName, string username, string password,
            string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? phoneNumber,
            string? address)
        {
            if (_doctorRepository.FindOneByJmbg(jmbg) != null)
                throw new Exception("Doctor with that jmbg already exists!");
            else if (_doctorRepository.FindOneByUsername(username) != null)
                throw new Exception("Doctor with that username already exists!");
            else
            {
                Doctor newDoctor = new Doctor(speciality, specialityType, roomId, firstName, lastName, username, password,
                    jmbg, dateOfBirth, gender, email, phoneNumber, address);
                _doctorRepository.SaveDoctor(newDoctor);
            }
        }

        public void DeleteDoctor(string jmbg)
        {
            if (_doctorRepository.FindOneByJmbg(jmbg) == null)
            {
                throw new Exception("Doctor with that jmbg doesn't exist!");
            }
            else
            {
                _doctorRepository.RemoveDoctor(jmbg);
            }
        }
    }
}