using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
    public class AppointmentRepository
    {
        private String appointmentFilePath = @"..\..\..\Resources\Appointments.json";

        public List<Model.Appointment> FindAll()
        {
            var values = GetValues();
            return values;
        }

        public List<Model.Appointment> FindAllByPatientId(String patientId)
        {
            var values = GetValues();
            List<Model.Appointment> result = new List<Model.Appointment>();
            foreach (Appointment appointment in values)
<<<<<<< HEAD
                if (appointment.patientJmbg.Equals(patientId))
                    result.Add(appointment);
=======
            {
                if (appointment.patientJmbg.Equals(patientId))
                {
                    result.Add(appointment);
                    
                }
            }
>>>>>>> 3445fca3fac6e27e49069f58edd6587b4dc8dbb1
            return result;
        }


        public void SaveAppointment(Model.Appointment AppointmentToSave)
        {
            var values = GetValues();
            values.Add(AppointmentToSave);
            Save(values);
        }

        public void RemoveAppointment(int AppointmentId)
        {
            var values = GetValues();
            values.RemoveAll(val => val.Id == AppointmentId);
            Save(values);
        }


        public Model.Appointment FindOneById(int AppointmentId)
        {
            var values = GetValues();
            foreach (var val in values)
            {
                if (val.Id == AppointmentId)
                {
                    return val;
                }
            }

            return null;
        }

        public List<Model.Appointment> FindAllByPatientJmbg(String patientId)
        {
            var values = GetValues();
            List<Model.Appointment> result = new List<Model.Appointment>();
            foreach (Appointment appointment in values)
                if (appointment.PatientJmbg.Equals(patientId))
                    result.Add(appointment);
            return result;
        }

        //doctors appointments by jmbg
        public List<Model.Appointment> FindAllByDoctorJmbg(String DoctorJmbg)
        {
            var values = GetValues();
            List<Model.Appointment> result = new List<Model.Appointment>();
            foreach (Appointment appointment in values)
                if (appointment.DoctorJmbg.Equals(DoctorJmbg))
                    result.Add(appointment);
            return result;
        }

        public List<Appointment> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<Appointment>>(File.ReadAllText(appointmentFilePath));
            if (values == null)
            {
                values = new List<Appointment>();
            }

            return values;
        }

        public void Save(List<Appointment> values)
        {
            File.WriteAllText(appointmentFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }

    }   
}