using System;
using System.Collections.Generic;

namespace Model
{
    public class Doctor : User
    {
        public Boolean specialization;
        public String specializationType;

        public List<Appointment> appointment;

        public Doctor(bool specialization, string specializationType, List<Appointment> appointment,
            string firstName, string lastName, string username, string password,
            string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? telephone,
            string? adress): base(firstName, lastName, username, password, jmbg, dateOfBirth, gender, email, telephone, adress)
        {
            this.specializationType = specializationType;
            this.specialization = specialization;
            Appointment = appointment;
            Appointment = appointment;
            this.room = room;
        }

        public System.Collections.Generic.List<Appointment> Appointment
        {
            get
            {
                if (appointment == null)
                    appointment = new System.Collections.Generic.List<Appointment>();
                return appointment;
            }
            set
            {
                RemoveAllAppointment();
                if (value != null)
                {
                    foreach (Appointment oAppointment in value)
                        AddAppointment(oAppointment);
                }
            }
        }


        public void AddAppointment(Appointment newAppointment)
        {
            if (newAppointment == null)
                return;
            if (this.appointment == null)
                this.appointment = new System.Collections.Generic.List<Appointment>();
            if (!this.appointment.Contains(newAppointment))
            {
                this.appointment.Add(newAppointment);
                newAppointment.Doctor = this;
            }
        }


        public void RemoveAppointment(Appointment oldAppointment)
        {
            if (oldAppointment == null)
                return;
            if (this.appointment != null)
                if (this.appointment.Contains(oldAppointment))
                {
                    this.appointment.Remove(oldAppointment);
                    oldAppointment.Doctor = null;
                }
        }


        public void RemoveAllAppointment()
        {
            if (appointment != null)
            {
                System.Collections.ArrayList tmpAppointment = new System.Collections.ArrayList();
                foreach (Appointment oldAppointment in appointment)
                    tmpAppointment.Add(oldAppointment);
                appointment.Clear();
                foreach (Appointment oldAppointment in tmpAppointment)
                    oldAppointment.Doctor = null;
                tmpAppointment.Clear();
            }
        }
        public Room room;

    }
}