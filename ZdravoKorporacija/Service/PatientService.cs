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

        public String CreatePatient(Boolean isGuest, List<String>? allergens, BloodType bloodType,
            string firstName, string lastName, string username, string password,
            string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? telephone,
            string? address)
        {
            if (PatientRepository.FindOneByJmbg(jmbg) != null)
                return "Patient with that jmbg already exists!";
            else if (PatientRepository.FindOneByUsername(username) != null)
                return "Patient with that username already exists!";
            else
            {
                Patient newPatient = new Patient(isGuest, allergens, bloodType, firstName, lastName, username, password,
            jmbg, dateOfBirth, gender, email, telephone, address);
                if (!newPatient.validatePatient())
                    return "Something went wrong, patient account isn't saved!";
                PatientRepository.SavePatient(newPatient);
                return "";
            }
        }

        public String DeletePatient(string jmbg)
        {
            if (PatientRepository.FindOneByJmbg(jmbg) == null)
            {
                return "Patient with that jmbg doesn't exist!";
            }
            else
            {
                PatientRepository.RemovePatient(jmbg);
                return "";
            }
        }

        public void DeleteAllPatients()
        {
            PatientRepository.RemoveAll();
        }

        public String ModifyPatient(Boolean isGuest, List<String>? allergens, BloodType bloodType,
            string firstName, string lastName, string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? telephone,
            string? address)
        {
            Patient oldPatient = PatientRepository.FindOneByJmbg(jmbg);
            if (oldPatient == null)
                return "Patient with that jmbg doesn't exists!";
            else
            {
                Patient newPatient = new Patient(isGuest, allergens, bloodType, firstName, lastName, oldPatient.Username, oldPatient.Password,
            jmbg, dateOfBirth, gender, email, telephone, address);
                if (!newPatient.validatePatient())
                    return "Something went wrong, patient account isn't changed!";
                PatientRepository.UpdatePatient(newPatient);
                return "";
            }
        }

        public Patient? GetOneByJmbg(string jmbg)
        {
            return PatientRepository.FindOneByJmbg(jmbg);
        }

        public String CreateGuestAccount(String firstName, String lastName, String jmbg)
        {
            Patient guestPatient = new Patient(true, null, BloodType.NONE, firstName, lastName, firstName, "sifra123", jmbg,
                null, Gender.NONE, null, null, null);
            if (PatientRepository.FindOneByJmbg(jmbg) != null)
                return "Patient with that jmbg already exists!";
            else if (!guestPatient.validateGuest())
                return "Something went wrong, patient account isn't saved!";
            else
            {
                PatientRepository.SavePatient(guestPatient);
                return "";
            }
        }

    }
}