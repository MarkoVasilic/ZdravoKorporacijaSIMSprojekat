using Service;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class AppointmentController
    {
        AppointmentService appointmentService = new AppointmentService();
        public List<Model.Appointment> GetAllAppointments()
        {
            return appointmentService.GetAllAppointments();
        }

        //void
        public void CreateAppointmentPatient(DateTime date, String doctorJMBG)
        {
            appointmentService.CreateAppointmentPatient(date, doctorJMBG);  
        }

        //void
        public void DeleteAppointment(int AppointmentId)
        {
            appointmentService.DeleteAppointment(AppointmentId);
        }
        // ISPRAVITI NA DIJAGRAMU PROMJENU PARAMETARA 
        public void ModifyAppointment(int appointmentId, DateTime newDate)
        {
            appointmentService.ModifyAppointment(newDate, appointmentId);
        }

        public Model.Appointment GetAppointmentById(int AppointmentId)
        {
            throw new NotImplementedException();
        }

        public List<Model.Appointment> GetAppointmentsByDoctorJmbg(String DoctorJmbg)
        {
            throw new NotImplementedException();
        }
        public List<Model.Appointment> FindAllByPatientId(String patientId)
        {
            return appointmentService.FindAllByPatientId(patientId);
        }

    }
    }