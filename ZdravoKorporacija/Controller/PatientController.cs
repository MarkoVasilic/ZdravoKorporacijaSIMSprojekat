using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class PatientController
    {
        private readonly PatientService _patientService;
        private readonly AppointmentService _appointmentService;

        public PatientController(PatientService patientService, AppointmentService appointmentService)
        {
            this._patientService = patientService;
            this._appointmentService = appointmentService;
        }

        public Patient? getPatientByUsername(String username)
        {
            return _patientService.GetOneByUsername(username);
        }

        public List<Patient> GetAllPatients()
        {
            return _patientService.GetAllPatients();
        }

        public int getTrollCounterByPatient(string jmbg)
        {
            return _patientService.getTrollCounterByPatient(jmbg);
        }

        public void incrementTrollCounterByPatient(string jmbg)
        {
            _patientService.incrementTrollCounterByPatient(jmbg);
        }

        public void resetTrollCounterByPatient(string jmbg)
        {
            _patientService.resetTrollCounterByPatient(jmbg);
        }

        public void CreatePatient(Boolean isGuest, List<String>? allergens, BloodType bloodType,
            string firstName, string lastName, string username, string password,
            string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? phoneNumber,
            string? address)
        {
            _patientService.CreatePatient(isGuest, allergens, bloodType, firstName, lastName, username, password,
            jmbg, dateOfBirth, gender, email, phoneNumber, address);
        }

        public void CreateGuestAccount(String firstName, String lastName, String jmbg)
        {
            _patientService.CreateGuestAccount(firstName, lastName, jmbg);
        }

        public void DeletePatient(string jmbg)
        {
            _patientService.DeletePatient(jmbg);
            _appointmentService.DeleteAppointmentsForOnePatient(jmbg);
        }

        public void DeleteAllPatients()
        {
            _patientService.DeleteAllPatients();
        }

        public void ModifyPatient(Boolean isGuest, List<String>? allergens, BloodType bloodType,
            string firstName, string lastName, string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? phoneNumber,
            string? address)
        {
            _patientService.ModifyPatient(isGuest, allergens, bloodType, firstName, lastName,
                jmbg, dateOfBirth, gender, email, phoneNumber, address);
        }

        public Patient? GetOnePatient(string jmbg)
        {
            return _patientService.GetOneByJmbg(jmbg);
        }

    }
}