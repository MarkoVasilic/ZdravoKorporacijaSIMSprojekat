using System;
using System.Collections.Generic;
using Model;
using Repository;

namespace Service
{
    public class AppointmentService
    {
        AppointmentRepository appointmentRepository = new AppointmentRepository();
        public List<Appointment> GetAllAppointments()
        {
            return appointmentRepository.FindAll();
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

        public Model.Appointment GetOneAppointment(int AppointmentId)
        {
            throw new NotImplementedException();
        }

        public List<Appointment> GetAppointmentsByDoctor(Model.Doctor DoctorForAppointment)
        {
            throw new NotImplementedException();
        }

    }
}