using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class AppointmentService
    {
        AppointmentRepository AppointmentRepository = new AppointmentRepository();
        PatientRepository patientRepository = new PatientRepository();

        public AppointmentService(AppointmentRepository appointmentRepository)
        {
            this.AppointmentRepository = appointmentRepository;
        }

        public List<Appointment> GetAllAppointments()
        {
            return AppointmentRepository.FindAll();
        }

        //neophodno odraditi neki try Catch ako se unese ID doktora koji ne postoji.
        // odnosno potrebna je validacija, takodje razmatram opciju da vraca Appointment umjesto void
        public Model.Appointment CreateAppointmentByPatient(DateTime date, String doctorJMBG)
        {
            int id = GenerateNewId();
            Appointment appointment = new Appointment(date, 15, id, "1111111111111", doctorJMBG, 11);    
            AppointmentRepository.SaveAppointment(appointment);
            return appointment;

        }

        private int GenerateNewId()
        {
            try
            {
                List<Appointment> appointments = AppointmentRepository.FindAll();
                int currentMax = appointments.Max(obj => obj.Id);
                return currentMax + 1;
            }
            catch
            {
                return 1;
            }
        }
        //void
        public void DeleteAppointment(int AppointmentId)
        {
            AppointmentRepository.RemoveAppointment(AppointmentId);
        }

        //vraca void
        //PROMIJENIO SAM PARAMETRE FUNKCIJE, ISPRAVITI NA DIJAGRAMU
        public void ModifyAppointment(DateTime newDate, int appointmentId)
        {
            var oneAppointment = AppointmentRepository.FindOneById(appointmentId);
            if (oneAppointment != null)
            {
                var values = AppointmentRepository.GetValues();
                values.RemoveAll(value => value.Id.Equals(oneAppointment.Id));
                oneAppointment.StartTime = newDate;
                values.Add(oneAppointment);
                AppointmentRepository.Save(values);
            }
        }

        public Model.Appointment GetOneById(int AppointmentId)
        {
            return AppointmentRepository.FindOneById(AppointmentId);
        }


        public List<Appointment> GetAppointmentsByDoctorJmbg(String DoctorJmbg)
        {
            return AppointmentRepository.FindAllByDoctorJmbg(DoctorJmbg);
        }

        public List<Appointment> GetAppointmentsByPatientJmbg(String PatientId)
        {
            return AppointmentRepository.FindAllByPatientJmbg(PatientId);
        }

        public String CreateAppointmentByDoctor(DateTime StartTime, int Duration, String PatientJmbg)
        {
            if (patientRepository.FindOneByJmbg(PatientJmbg) == null)
            {
                return "Patient with that JMBG doesn't exist!";
            }
            int id = GenerateNewId();
            Appointment appointment = new Appointment(StartTime, Duration, id, PatientJmbg, "456", 11);
             if(!appointment.validateAppointment())
            {
                return "Domething went wrong, new appointment isn't created!";
            }
            else
            {
                AppointmentRepository.SaveAppointment(appointment);
                return "";
            }

        }
    }
}