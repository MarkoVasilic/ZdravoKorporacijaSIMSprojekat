using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using ZdravoKorporacija.Interfaces;

namespace Repository
{
    public class AppointmentRepository: IAppointmentRepository
    {
        private readonly String _appointmentFilePath = @"..\..\..\Resources\Appointments.json";

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

        public List<Appointment> GetAllPastAppointmentsByPatient(String patientJmbg)
        {
            var values = GetValues();
            List<Model.Appointment> result = new List<Model.Appointment>();
            foreach (Appointment appointment in values)
                if (appointment.PatientJmbg.Equals(patientJmbg))
                {
                    if (appointment.StartTime < System.DateTime.Now || appointment.StartTime.Year < System.DateTime.Now.Year)
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

        public void RemoveAppointmentsForOnePatient(String patientjmbg)
        {
            var values = GetValues();
            values.RemoveAll(val => val.PatientJmbg == patientjmbg);
            Save(values);
        }

        public void RemoveAppointmentByRoomId(int roomId)
        {
            var values = GetValues();
            values.RemoveAll(val => val.RoomId == roomId);
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

        public List<Appointment> FindAllByRoomId(int id)
        {
            var values = GetValues();
            List<Appointment> result = new List<Appointment>();
            foreach (Appointment appointment in values)
                if (appointment.RoomId.Equals(id))
                    result.Add(appointment);
            return result;
        }

        private List<Appointment> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<Appointment>>(File.ReadAllText(_appointmentFilePath));
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

        private void Save(List<Appointment> values)
        {
            File.WriteAllText(_appointmentFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }


    }
}