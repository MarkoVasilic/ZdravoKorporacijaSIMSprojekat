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
        public void CreateAppointmentPatient(DateTime date, String doctorJMBG)
        {
            
            Appointment appointment = new Appointment();    
            int id = GenerateNewId();
            appointment.Id = id;
            AppointmentRepository.SaveAppointment(appointment);

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

        public Model.Appointment CreateAppointmentByDoctor(DateTime StartTime, int Duration, String PatientJmbg)
        {
            int id = GenerateNewId();
            Appointment appointment = new Appointment(StartTime, Duration, id, PatientJmbg, "456", 11);
            AppointmentRepository.SaveAppointment(appointment);
            return appointment;

        }
        public List<Appointment> FindAllByPatientId(String patientId)
        {
            return AppointmentRepository.FindAllByPatientId(patientId);
        }
    }
}