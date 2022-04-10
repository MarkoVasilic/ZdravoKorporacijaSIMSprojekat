using System;
using Model;
using Service;
using System.Collections.Generic;
using System.Diagnostics;

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

        public void CreatePatient(Patient patientToMake)
        {
            if (PatientService.GetOnePatient(patientToMake.Jmbg) == null)
                PatientService.CreatePatient(patientToMake);
            else
            {
                //throw new Exception("Patient with that jmbg already exists!");
            }
                
        }

        public void CreateGuestAccount(String firstName, String lastName, String jmbg)
        {
            if (PatientService.GetOnePatient(jmbg) == null)
                PatientService.CreateGuestAccount(firstName, lastName, jmbg);
            else
            {
                throw new Exception("Patient with that jmbg already exists!");
            }
        }

        public void DeletePatient(string jmbg)
        {
            if (PatientService.GetOnePatient(jmbg) != null)
                PatientService.DeletePatient(jmbg);
            else
            {
                throw new Exception("Patient with that jmbg doesn't exists!");
            }
        }

        public void ModifyPatient(Patient patientToModify)
        {
            if (PatientService.GetOnePatient(patientToModify.Jmbg) != null)
                PatientService.ModifyPatient(patientToModify);
            else
            {
                throw new Exception("Patient with that jmbg doesn't exists!");
            }  
        }

        public Patient? GetOnePatient(string jmbg)
        {
            return PatientService.GetOnePatient(jmbg);
        }

    }
}