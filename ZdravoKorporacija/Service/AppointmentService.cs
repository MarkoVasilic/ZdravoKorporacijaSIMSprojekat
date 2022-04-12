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

        //void
        public void CreateAppointment(Model.Appointment appointmentToMake)
        {
            int id = GenerateNewId();
            appointmentToMake.Id = id;
            AppointmentRepository.SaveAppointment(appointmentToMake);

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
        public void ModifyAppointment(int appointmentId, DateTime newDate)
        {
            Appointment newAppointment = AppointmentRepository.FindOneById(appointmentId);
            newAppointment.StartTime = newDate;
            AppointmentRepository.SaveAppointment(newAppointment);
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

    }
}