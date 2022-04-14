using Service;
using System;
using Model;
using System.Collections.Generic;

namespace Controller
{
    public class AppointmentController
    {
        private readonly AppointmentService AppointmentService;

        public AppointmentController(AppointmentService appointmentService)
        {
            this.AppointmentService = appointmentService;
        }

        public List<Model.Appointment> GetAllAppointments()
        {
            return AppointmentService.GetAllAppointments();
        }


        public String CreateAppointmentByDoctor(DateTime startTime, int duration, String patientJmbg)
        {
            return AppointmentService.CreateAppointmentByDoctor(startTime, duration, patientJmbg);
        }

        public String CreateAppointmentByPatient(DateTime startTime, String doctorJmbg)
        {
            return AppointmentService.CreateAppointmentByPatient(startTime, doctorJmbg);
        }

        public String DeleteAppointment(int appointmentId)
        {
            return AppointmentService.DeleteAppointment(appointmentId);
        }
        public String ModifyAppointment(int appointmentId, DateTime newDate)
        {
            return AppointmentService.ModifyAppointment(newDate, appointmentId);
        }

        public Appointment GetOneById(int appointmentId)
        {
            return AppointmentService.GetOneById(appointmentId);
        }

        public List<Model.Appointment> GetAppointmentsByDoctorJmbg(String doctorJmbg)
        {
            return AppointmentService.GetAppointmentsByDoctorJmbg(doctorJmbg);
        }
        public List<Model.Appointment> GetAppointmentsByPatientJmbg(String patientId)
        {
            return AppointmentService.GetAppointmentsByPatientJmbg(patientId);
        }

    }
}