using Model;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
    public class PatientService
    {
        private readonly PatientRepository patientRepository;

        public PatientService(PatientRepository patientRepository)
        {
            this.patientRepository = patientRepository;
        }

        public List<Patient> GetAllPatients()
        {
            return patientRepository.FindAll();
        }

        public void CreatePatient(Patient pacientToMake)
        {
            patientRepository.SavePatient(pacientToMake);
        }

        public void DeletePatient(string jmbg)
        {
            patientRepository.RemovePatient(jmbg);
        }

        public void ModifyPatient(Patient pacientToModify)
        {
            patientRepository.SavePatient(pacientToModify);
        }

        public Model.Patient? GetOnePatient(string jmbg)
        {
            return patientRepository.FindOneByJmbg(jmbg);
        }

        public void CreateGuestAccount(String firstName, String lastName, String jmbg)
        {
            Patient guestPatient = new Patient(null, BloodType.NONE, firstName, lastName, firstName, "password", jmbg,
                null, Gender.NONE, null, null, null);
            patientRepository.SavePatient(guestPatient);
        }

    }
}