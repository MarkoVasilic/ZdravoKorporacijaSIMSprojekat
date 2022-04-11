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
                if (appointment.patientJmbg.Equals(patientId))
                    result.Add(appointment);
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
            values.RemoveAll(val => val.id == AppointmentId);
            Save(values);
        }


        public Model.Appointment FindOneById(int AppointmentId)
        {
            var values = GetValues();
            foreach (var val in values)
            {
                if (val.id == AppointmentId)
                {
                    return val;
                }
            }

            return null;
        }

        //doctors appointments by jmbg
        public List<Model.Appointment> FindAllByDoctor(String Jmbg)
        {
            throw new NotImplementedException();
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