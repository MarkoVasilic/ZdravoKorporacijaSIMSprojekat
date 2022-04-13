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

        public String CreatePatient(Boolean isGuest, List<String>? allergens, BloodType bloodType,
            string firstName, string lastName, string username, string password,
            string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? telephone,
            string? address)
        {
            return PatientService.CreatePatient(isGuest, allergens, bloodType, firstName, lastName, username, password,
            jmbg, dateOfBirth, gender, email, telephone, address);
        }

        public String CreateGuestAccount(String firstName, String lastName, String jmbg)
        {
            return PatientService.CreateGuestAccount(firstName, lastName, jmbg);
        }

        public String DeletePatient(string jmbg)
        {
            return PatientService.DeletePatient(jmbg);
        }

        public void DeleteAllPatients()
        {
            PatientService.DeleteAllPatients();
        }

        public String ModifyPatient(Boolean isGuest, List<String>? allergens, BloodType bloodType,
            string firstName, string lastName, string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? telephone,
            string? address)
        {
            return PatientService.ModifyPatient(isGuest, allergens, bloodType, firstName, lastName,
            jmbg, dateOfBirth, gender, email, telephone, address);
        }

        public Patient? GetOnePatient(string jmbg)
        {
            return PatientService.GetOnePatient(jmbg);
        }

    }
}