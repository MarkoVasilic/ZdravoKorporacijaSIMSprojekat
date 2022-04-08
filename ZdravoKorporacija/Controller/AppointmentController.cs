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

        public Model.Appointment CreateAppointment(Model.Appointment PacientToMake)
        {
            throw new NotImplementedException();
        }

        public Boolean DeleteAppointment(int AppointmentId)
        {
            throw new NotImplementedException();
        }

        public Boolean ModifyAppointment(Model.Appointment AppointmentToModify)
        {
            throw new NotImplementedException();
        }

        public Model.Appointment GetAppointmentById(int AppointmentId)
        {
            throw new NotImplementedException();
        }

        public List<Model.Appointment> GetAppointmentsByDoctor(Model.Doctor DoctorForAppointment)
        {
            throw new NotImplementedException();
        }

    }
}