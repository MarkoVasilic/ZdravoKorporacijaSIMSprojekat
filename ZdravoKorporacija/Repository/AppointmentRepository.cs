using System;
using System.Collections.Generic;

namespace Repository
{
    public class AppointmentRepository
    {
        private String appointmentFilePath;

        public List<Model.Appointment> FindAll()
        {
            throw new NotImplementedException();
        }

        public Model.Appointment SaveAppointment(Model.Appointment AppointmentToSave)
        {
            throw new NotImplementedException();
        }

        public Boolean RemoveAppointment(int AppointmentId)
        {
            throw new NotImplementedException();
        }

        public Model.Appointment FindOneById(int AppointmentId)
        {
            throw new NotImplementedException();
        }

        public List<Model.Appointment> FindAllByDoctor(Model.Doctor DoctorForAppointment)
        {
            throw new NotImplementedException();
        }

    }
}