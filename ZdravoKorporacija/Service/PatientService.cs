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

        public void CreatePatient(Patient patientToMake)
        {
            PatientRepository.SavePatient(patientToMake);
        }

        public void DeletePatient(string jmbg)
        {
            PatientRepository.RemovePatient(jmbg);
        }

        public void ModifyPatient(Patient patientToModify)
        {
            PatientRepository.UpdatePatient(patientToModify);
        }

        public Model.Patient? GetOnePatient(string jmbg)
        {
            return PatientRepository.FindOneByJmbg(jmbg);
        }

        public void CreateGuestAccount(String firstName, String lastName, String jmbg)
        {
            Patient guestPatient = new Patient(true, null, BloodType.NONE, firstName, lastName, firstName, "password", jmbg,
                null, Gender.NONE, null, null, null);
            PatientRepository.SavePatient(guestPatient);
        }

    }
}