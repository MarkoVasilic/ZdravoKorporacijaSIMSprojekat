using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
    public class AppointmentRepository
    {
        private readonly String AppointmentFilePath = @"..\..\..\Resources\Appointments.json";

        public List<Appointment> FindAll()
        {
            var values = GetValues();
            return values;
        }

        public List<Appointment> FindAllByPatientJmbg(String patientJmbg)
        {
            var values = GetValues();
            List<Model.Appointment> result = new List<Model.Appointment>();
            foreach (Appointment appointment in values)
                if (appointment.PatientJmbg.Equals(patientJmbg))
                    result.Add(appointment);
            return result;
        }

        public List<Appointment> GetAllFutureByPatient(String patientJmbg)
        {
            var values = GetValues();
            List<Model.Appointment> result = new List<Model.Appointment>();
            foreach (Appointment appointment in values)
                if (appointment.PatientJmbg.Equals(patientJmbg))
                {
                    if (appointment.StartTime > System.DateTime.Now)
                    {
                        result.Add(appointment);
                    }
                }
            return result;
        }




        public void SaveAppointment(Appointment appointmentToSave)
        {
            var values = GetValues();
            values.Add(appointmentToSave);
            Save(values);
        }

        public void RemoveAppointment(int appointmentId)
        {
            var values = GetValues();
            values.RemoveAll(val => val.Id == appointmentId);
            Save(values);
        }


        public Appointment? FindOneById(int appointmentId)
        {
            var values = GetValues();
            foreach (var val in values)
            {
                if (val.Id == appointmentId)
                {
                    return val;
                }
            }

            return null;
        }

        public List<Appointment> FindAllByDoctorJmbg(String doctorJmbg)
        {
            var values = GetValues();
            List<Appointment> result = new List<Appointment>();
            foreach (Appointment appointment in values)
                if (appointment.DoctorJmbg.Equals(doctorJmbg))
                    result.Add(appointment);
            return result;
        }

        public List<Appointment> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<Appointment>>(File.ReadAllText(AppointmentFilePath));
            if (values == null)
            {
                values = new List<Appointment>();
            }

            return values;
        }

        public void UpdateAppointment(Appointment appointmentToModify)
        {
            var oneAppointment = FindOneById(appointmentToModify.Id);
            if (oneAppointment != null)
            {
                var values = GetValues();
                values.RemoveAll(value => value.Id.Equals(appointmentToModify.Id));
                values.Add(appointmentToModify);
                Save(values);
            }
        }

        public void Save(List<Appointment> values)
        {
            File.WriteAllText(AppointmentFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }

    }
}