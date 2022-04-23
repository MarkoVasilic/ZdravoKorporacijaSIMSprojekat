using Model;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
    public class PatientService
    {
        private readonly PatientRepository PatientRepository;

        public PatientService(PatientRepository patientRepository)
        {
            this.PatientRepository = patientRepository;
        }

        public List<Patient> GetAllPatients()
        {
            return PatientRepository.FindAll();
        }

        public void CreatePatient(Boolean isGuest, List<String>? allergens, BloodType bloodType,
            string firstName, string lastName, string username, string password,
            string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? telephone,
            string? address)
        {
            if (PatientRepository.FindOneByJmbg(jmbg) != null)
                throw new Exception("Patient with that jmbg already exists!");
            else if (PatientRepository.FindOneByUsername(username) != null)
                throw new Exception("Patient with that username already exists!");
            else
            {
                Patient newPatient = new Patient(isGuest, allergens, bloodType, firstName, lastName, username, password,
                    jmbg, dateOfBirth, gender, email, telephone, address);
                if (!newPatient.validatePatient())
                    throw new Exception("Something went wrong, patient account isn't saved!");
                PatientRepository.SavePatient(newPatient);
            }
        }

        public void DeletePatient(string jmbg)
        {
            if (PatientRepository.FindOneByJmbg(jmbg) == null)
            {
                throw new Exception("Patient with that jmbg doesn't exist!");
            }
            else
            {
                PatientRepository.RemovePatient(jmbg);
            }
        }

        public void DeleteAllPatients()
        {
            PatientRepository.RemoveAll();
        }

        public void ModifyPatient(Boolean isGuest, List<String>? allergens, BloodType bloodType,
            string firstName, string lastName, string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? telephone,
            string? address)
        {
            Patient oldPatient = PatientRepository.FindOneByJmbg(jmbg);
            if (oldPatient == null)
                throw new Exception("Patient with that jmbg doesn't exist!");
            else if (isGuest == true) {
                oldPatient.Allergens = allergens;
                PatientRepository.UpdatePatient(oldPatient);
            }
            else
            {
                Patient newPatient = new Patient(isGuest, allergens, bloodType, firstName, lastName, oldPatient.Username, oldPatient.Password,
            jmbg, dateOfBirth, gender, email, telephone, address);
                if (!newPatient.validatePatient())
                    throw new Exception("Something went wrong, patient account isn't changed!");
                PatientRepository.UpdatePatient(newPatient);
            }
        }

        public Patient? GetOneByJmbg(string jmbg)
        {
            return PatientRepository.FindOneByJmbg(jmbg);
        }

        public void CreateGuestAccount(String firstName, String lastName, String jmbg)
        {
            Patient guestPatient = new Patient(true, null, BloodType.NONE, firstName, lastName, firstName, "sifra123", jmbg,
                null, Gender.NONE, null, null, null);
            if (PatientRepository.FindOneByJmbg(jmbg) != null)
                throw new Exception("Patient with that jmbg doesn't exist!");
            else if (!guestPatient.validateGuest())
                throw new Exception("Something went wrong, patient account isn't saved!");
            else
            {
                PatientRepository.SavePatient(guestPatient);
            }
        }

    }
}