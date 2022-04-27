using Model;
using Service;
using System;
using System.Collections.Generic;
using ZdravoKorporacija.DTO;

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

        public List<PossibleAppointmentsDTO> GetAllAppointmentsBySecretary()
        {
            return AppointmentService.GetAllAppointmentsBySecretary();
        }

        public List<PossibleAppointmentsDTO> GetAllFutureAppointmentsByPatient()
        {
            return AppointmentService.GetAllFutureAppointmentsByPatient();
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

        public List<PossibleAppointmentsDTO> GetPossibleAppointmentsBySecretary(String patientJmbg, String doctorJmbg, int roomId,
            DateTime dateFrom, DateTime dateUntil, int duration, String priority)
        {
            return AppointmentService.GetPossibleAppointmentsBySecretary(patientJmbg, doctorJmbg, roomId,
                dateFrom, dateUntil, duration, priority);
        }

        public void CreateAppointmentBySecretary(String patientJmbg, String doctorJmbg, int roomId,
            DateTime startTime, int duration)
        {
            AppointmentService.CreateAppointmentBySecretary(patientJmbg, doctorJmbg, roomId,
                startTime, duration);
        }

    }
}