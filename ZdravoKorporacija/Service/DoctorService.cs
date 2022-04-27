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

        public List<Doctor> GetAllDoctors()
        {
            return DoctorRepository.FindAll();
        }

        public Doctor? GetOneByUsername(String username)
        {
            return DoctorRepository.FindOneByUsername(username);

        }

        public void CreateDoctor(bool speciality, String specialityType, string firstName, int roomId, string lastName, string username, string password,
            string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? telephone,
            string? address)
        {
            if (DoctorRepository.FindOneByJmbg(jmbg) != null)
                throw new Exception("Doctor with that jmbg already exists!");
            else if (DoctorRepository.FindOneByUsername(username) != null)
                throw new Exception("Doctor with that username already exists!");
            else
            {
                Doctor newDoctor = new Doctor(speciality, specialityType, roomId, firstName, lastName, username, password,
                    jmbg, dateOfBirth, gender, email, telephone, address);
                DoctorRepository.SaveDoctor(newDoctor);
            }
        }

        public void DeleteDoctor(string jmbg)
        {
            if (DoctorRepository.FindOneByJmbg(jmbg) == null)
            {
                throw new Exception("Doctor with that jmbg doesn't exist!");
            }
            else
            {
                DoctorRepository.RemoveDoctor(jmbg);
            }
        }
    }
}