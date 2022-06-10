using Model;
using Repository;
using System;
using System.Collections.Generic;
using ZdravoKorporacija.Interfaces;

namespace Service
{
    public class PatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository PatientRepository)
        {
            this._patientRepository = PatientRepository;
        }

        public List<Patient> GetAllPatients()
        {
            return _patientRepository.FindAll();
        }

        public void CreatePatient(Boolean isGuest, List<String>? allergens, BloodType bloodType,
            string firstName, string lastName, string username, string password,
            string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? telephone,
            string? address)
        {
            if (_patientRepository.FindOneByJmbg(jmbg) != null)
                throw new Exception("Patient with that jmbg already exists!");
            else if (_patientRepository.FindOneByUsername(username) != null)
                throw new Exception("Patient with that username already exists!");
            else
            {
                if (dateOfBirth == null)
                    throw new Exception("You must enter date of birth!");
                Patient newPatient = new Patient(isGuest, allergens, bloodType, firstName, lastName, username, password,
                    jmbg, dateOfBirth, gender, email, telephone, address);
                if (!newPatient.validatePatient())
                    throw new Exception("Something went wrong, patient account isn't saved!");
                _patientRepository.SavePatient(newPatient);
            }
        }

        public void DeletePatient(string jmbg)
        {
            if (_patientRepository.FindOneByJmbg(jmbg) == null)
            {
                throw new Exception("Patient with that jmbg doesn't exist!");
            }
            else
            {
                _patientRepository.RemovePatient(jmbg);
            }
        }

        public void DeleteAllPatients()
        {
            _patientRepository.RemoveAll();
        }

        public void ModifyPatient(Boolean isGuest, List<String>? allergens, BloodType bloodType,
            string firstName, string lastName, string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? telephone,
            string? address)
        {
            Patient oldPatient = _patientRepository.FindOneByJmbg(jmbg);
            if (oldPatient == null)
                throw new Exception("Patient with that jmbg doesn't exist!");
            if (isGuest)
            {
                oldPatient.Allergens = allergens;
                _patientRepository.UpdatePatient(oldPatient);
            }
            else
            {
                if (dateOfBirth == null)
                    throw new Exception("You must enter date of birth!");
                Patient newPatient = new Patient(isGuest, allergens, bloodType, firstName, lastName, oldPatient.Username, oldPatient.Password,
            jmbg, dateOfBirth, gender, email, telephone, address);
                if (!newPatient.validatePatient())
                    throw new Exception("Something went wrong, patient account isn't changed!");
                _patientRepository.UpdatePatient(newPatient);
            }
        }

        public Patient? GetOneByJmbg(string jmbg)
        {
            return _patientRepository.FindOneByJmbg(jmbg);
        }

        public Patient? GetOneByUsername(String username)
        {
            return _patientRepository.FindOneByUsername(username);

        }

        public int getTrollCounterByPatient(string jmbg)
        {
            Patient patient = _patientRepository.FindOneByJmbg(jmbg);
            return patient.trollCounter;
        }

        public void incrementTrollCounterByPatient(string jmbg)
        {
            Patient patient = _patientRepository.FindOneByJmbg(jmbg);
            patient.trollCounter++;
            _patientRepository.UpdatePatient(patient);

        }
        public void resetTrollCounterByPatient(string jmbg)
        {
            Patient patient = _patientRepository.FindOneByJmbg(jmbg);
            patient.trollCounter = 0;
            _patientRepository.UpdatePatient(patient);

        }




        public void CreateGuestAccount(String firstName, String lastName, String jmbg)
        {
            Patient guestPatient = new Patient(true, null, BloodType.NONE, firstName, lastName, firstName, "sifra123", jmbg,
                null, Gender.NONE, null, null, null);
            string retVal = guestPatient.validateGuest();
            if (_patientRepository.FindOneByJmbg(jmbg) != null)
                throw new Exception("Patient with that jmbg doesn't exist!");
            else if (retVal.Length != 0)
                throw new Exception(retVal);
            else
            {
                _patientRepository.SavePatient(guestPatient);
            }
        }

    }
}