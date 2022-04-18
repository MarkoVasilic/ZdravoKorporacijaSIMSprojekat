using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class PatientController
    {
        private readonly PatientService PatientService;

        public PatientController(PatientService patientService)
        {
            this.PatientService = patientService;
        }

        public List<Patient> GetAllPatients()
        {
            return PatientService.GetAllPatients();
        }

        public void CreatePatient(Boolean isGuest, List<String>? allergens, BloodType bloodType,
            string firstName, string lastName, string username, string password,
            string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? phoneNumber,
            string? address)
        {
            PatientService.CreatePatient(isGuest, allergens, bloodType, firstName, lastName, username, password,
            jmbg, dateOfBirth, gender, email, phoneNumber, address);
        }

        public void CreateGuestAccount(String firstName, String lastName, String jmbg)
        {
            PatientService.CreateGuestAccount(firstName, lastName, jmbg);
        }

        public void DeletePatient(string jmbg)
        {
            PatientService.DeletePatient(jmbg);
        }

        public void DeleteAllPatients()
        {
            PatientService.DeleteAllPatients();
        }

        public void ModifyPatient(Boolean isGuest, List<String>? allergens, BloodType bloodType,
            string firstName, string lastName, string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? phoneNumber,
            string? address)
        {
            PatientService.ModifyPatient(isGuest, allergens, bloodType, firstName, lastName,
                jmbg, dateOfBirth, gender, email, phoneNumber, address);
        }

        public Patient? GetOnePatient(string jmbg)
        {
            return PatientService.GetOneByJmbg(jmbg);
        }

    }
}