using Service;
using System;
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

        public Model.Appointment CreateAppointmentByPatient(DateTime newDate, string doctorJMBG)
        {
            return AppointmentService.CreateAppointmentPatient(newDate, doctorJMBG);
        }

        //void
        public Model.Appointment CreateAppointmentByDoctor(DateTime StartTime, int Duration, String PatientJmbg)
        {
            return AppointmentService.CreateAppointmentByDoctor(StartTime, Duration, PatientJmbg);
        }

        //void
        public void DeleteAppointment(int AppointmentId)
        {
            AppointmentService.DeleteAppointment(AppointmentId);
        }
        // void
        // ISPRAVITI NA DIJAGRAMU PROMJENU PARAMETARA 
        public void ModifyAppointment(int appointmentId, DateTime newDate)
        {
            AppointmentService.ModifyAppointment(newDate, appointmentId);
        }

        //msm da ne treba
        public Model.Appointment GetOneById(int AppointmentId)
        {
            return AppointmentService.GetOneById(AppointmentId);
        }

        public List<Model.Appointment> GetAppointmentsByDoctorJmbg(String DoctorJmbg)
        {
            return AppointmentService.GetAppointmentsByDoctorJmbg(DoctorJmbg);
        }
        public List<Model.Appointment> GetAppointmentsByPatientJmbg(String patientId)
        {
            return AppointmentService.GetAppointmentsByPatientJmbg(patientId);
        }
        public List<Model.Appointment> FindAllByPatientId(String patientId)
        {
            return AppointmentService.FindAllByPatientId(patientId);
        }

    }
    }