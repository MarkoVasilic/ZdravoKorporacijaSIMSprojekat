using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class AppointmentService
    {
        AppointmentRepository appointmentRepository = new AppointmentRepository();
        public List<Appointment> GetAllAppointments()
        {
            return appointmentRepository.FindAll();
        }

        //void
        public void CreateAppointment(Model.Appointment appointmentToMake)
        {
            int id = GenerateNewId();
            appointmentToMake.id = id;
            appointmentRepository.SaveAppointment(appointmentToMake);

        }

        private int GenerateNewId()
        {
            try
            {
                List<Appointment> appointments = appointmentRepository.FindAll();
                int currentMax = appointments.Max(obj => obj.id);
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
            appointmentRepository.RemoveAppointment(AppointmentId);
        }

        //vraca void
        public void ModifyAppointment(Model.Appointment AppointmentToModify)
        {
            throw new NotImplementedException();
        }

        public Model.Appointment GetOneAppointment(int AppointmentId)
        {
            throw new NotImplementedException();
        }

        //prosledi jmbg
        public List<Appointment> GetAppointmentsByDoctorJmbg(String Jmbg)
        {
            throw new NotImplementedException();
        }

    }
}