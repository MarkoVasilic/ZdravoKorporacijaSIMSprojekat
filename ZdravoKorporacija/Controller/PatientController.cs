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

        public String CreatePatient(Patient patientToMake)
        {
            if (PatientService.GetOnePatient(patientToMake.Jmbg) != null)
            {
                return "Patient with that jmbg already exists!";
            }
            else if(patientToMake.IsGuest == false && (patientToMake.FirstName == null || patientToMake.LastName == null
                || patientToMake.Jmbg == null || patientToMake.Gender == Gender.NONE || patientToMake.Email == null))
            {
                return "You must enter first name, last name, jmbg, gender and email!";
            }
            else
            {
                PatientService.CreatePatient(patientToMake);
                return "";
            }

        }

        public String CreateGuestAccount(String firstName, String lastName, String jmbg)
        {
            if (PatientService.GetOnePatient(jmbg) != null)
            {
                return "Patient with that jmbg already exists!";
            }
            else if (firstName == null || lastName == null || jmbg == null)
            {
                return "You must enter first name, last name and jmbg!";
            }
            else
            {
                PatientService.CreateGuestAccount(firstName, lastName, jmbg);
                return "";
            }
        }

        public String DeletePatient(string jmbg)
        {
            if (PatientService.GetOnePatient(jmbg) != null)
            {
                return "Patient with that jmbg doesn't exist!";
            }
            else
            {
                PatientService.DeletePatient(jmbg);
                return "";
            }
        }

        public void DeleteAllPatients()
        {
            PatientService.DeleteAllPatients();
        }

        public String ModifyPatient(Patient patientToModify)
        {
            if (PatientService.GetOnePatient(patientToModify.Jmbg) == null)
            {
                return "Patient with that jmbg doesn't exist!";
            }
            else if (patientToModify.IsGuest == false && (patientToModify.FirstName == null || patientToModify.LastName == null
                || patientToModify.Jmbg == null || patientToModify.Gender == Gender.NONE || patientToModify.Email == null))
            {
                return "You must enter first name, last name, jmbg, gender and email!";
            }
            else
            {
                PatientService.ModifyPatient(patientToModify);
                return "";
            }
        }

        public Patient? GetOnePatient(string jmbg)
        {
            return PatientService.GetOnePatient(jmbg);
        }

    }
}