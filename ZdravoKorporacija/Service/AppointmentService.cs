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
        public void CreateAppointment(DateTime date, String doctorJMBG)
        {
            Appointment appointment = new Appointment();    
            int id = GenerateNewId();
             appointment.id = id;
            appointment.doctorJmbg = doctorJMBG;
            appointment.startTime = date;
            appointment.patientJmbg = "111111111";
          //  Model.Doctor doctor = new Doctor();
      //  doctor = DoctorRepository.findOneById(doctorJMBG);
        //    appointment.roomId = doctor.roomId;
            appointmentRepository.SaveAppointment(appointment);

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

        //PROMJENIO SAM PARAMETRE FUNKCIJE, ISPRAVITI NA DIJAGRAMU
        public void ModifyAppointment(DateTime newDate, int appointmentId)
        {
            Appointment newAppointment= new Appointment();
           newAppointment = appointmentRepository.FindOneById(appointmentId);
            newAppointment.startTime = newDate;
        }


        // da li je ova metoda potrebna ako postoji FindOneById
        public Model.Appointment GetOneAppointment(int AppointmentId)
        {
            throw new NotImplementedException();
        }

        //prosledi jmbg
        public List<Appointment> GetAppointmentsByDoctorJmbg(String Jmbg)
        {
            throw new NotImplementedException();
        }
        public List<Appointment> FindAllByPatientId(String patientId)
        {
            return appointmentRepository.FindAllByPatientId(patientId);
        }
    }
}