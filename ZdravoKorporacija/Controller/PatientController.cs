using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class PatientController
    {
        private readonly PatientService patientService;

        public PatientController(PatientService patientService)
        {
            this.patientService = patientService;
        }

        public List<Patient> GetAllPatients()
        {
            return patientService.GetAllPatients();
        }

        public void CreatePatient(Patient patientToMake)
        {
            if (patientService.GetOnePatient(patientToMake.jmbg) == null)
                patientService.CreatePatient(patientToMake);
            else
            {
                throw new Exception("Patient with that jmbg already exists!");
            }

        }

        public void CreateGuestAccount(String firstName, String lastName, String jmbg)
        {
            if (patientService.GetOnePatient(jmbg) == null)
                patientService.CreateGuestAccount(firstName, lastName, jmbg);
            else
            {
                throw new Exception("Patient with that jmbg already exists!");
            }
        }

        public void DeletePatient(string jmbg)
        {
            if (patientService.GetOnePatient(jmbg) != null)
                patientService.DeletePatient(jmbg);
            else
            {
                throw new Exception("Patient with that jmbg doesn't exists!");
            }
        }

        public void ModifyPatient(Patient patientToModify)
        {
            if (patientService.GetOnePatient(patientToModify.jmbg) != null)
                patientService.ModifyPatient(patientToModify);
            else
            {
                throw new Exception("Patient with that jmbg doesn't exists!");
            }
        }

        public Patient? GetOnePatient(string jmbg)
        {
            return patientService.GetOnePatient(jmbg);
        }

    }
}