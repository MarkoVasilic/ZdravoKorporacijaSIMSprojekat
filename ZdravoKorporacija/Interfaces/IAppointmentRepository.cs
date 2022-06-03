using System;
using System.Collections.Generic;
using Model;
using Newtonsoft.Json;

namespace ZdravoKorporacija.Interfaces;

public interface IAppointmentRepository
{
    public List<Appointment> FindAll();
    public List<Appointment> FindAllByPatientJmbg(String patientJmbg);
    public List<Appointment> GetAllFutureByPatient(String patientJmbg);
    public List<Appointment> GetAllPastAppointmentsByPatient(String patientJmbg);
    public void SaveAppointment(Appointment appointmentToSave);
    public void RemoveAppointment(int appointmentId);
    public void RemoveAppointmentsForOnePatient(String patientjmbg);
    public void RemoveAppointmentByRoomId(int roomId);
    public Appointment? FindOneById(int appointmentId);
    public List<Appointment> FindAllByDoctorJmbg(String doctorJmbg);
    public List<Appointment> FindAllByRoomId(int id);
    public void UpdateAppointment(Appointment appointmentToModify);
}