using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class PatientController
    {
        private readonly PatientService PatientService;
        private readonly AppointmentService AppointmentService;

        public PatientController(PatientService patientService, AppointmentService appointmentService)
        {
            this.PatientService = patientService;
            this.AppointmentService = appointmentService;
        }

        public Patient? getPatientByUsername(String username)
        {
            return PatientService.GetOneByUsername(username);
        }

        public List<Patient> GetAllPatients()
        {
            return PatientService.GetAllPatients();
        }

        public int getTrollCounterByPatient(string jmbg)
        {
            return PatientService.getTrollCounterByPatient(jmbg);
        }

        public void incrementTrollCounterByPatient(string jmbg)
        {
            PatientService.incrementTrollCounterByPatient(jmbg);
        }

        public void resetTrollCounterByPatient(string jmbg)
        {
            PatientService.resetTrollCounterByPatient(jmbg);
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
            AppointmentService.DeleteAppointmentsForOnePatient(jmbg);
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

        public void ModifyInfo(
string firstName, string lastName,  DateTime? dateOfBirth, string? email, string? telephone,
string? address, string Username, string Password, string jmbg)
        {
            PatientService.ModifyInfo(firstName, lastName, dateOfBirth, email, telephone, address, Username, Password, jmbg);
        }

    }
}