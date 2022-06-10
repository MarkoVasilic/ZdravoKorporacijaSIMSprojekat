using System;
using System.Collections.Generic;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.Controller
{
    public class NotificationController
    {

        private readonly NotificationService _notificationService = new NotificationService();

        public NotificationController(NotificationService Service)
        {
            this._notificationService = Service;
        }

        public List<Notification> GetAll()
        {
            return _notificationService.GetAll();
        }

        public List<Notification> GetAllByPatientJmbg()
        {
            return _notificationService.GetAllByPatientJmbg(App.loggedUser.Jmbg);
        }

        public List<Notification> GetAllByUserJmbg(String jmbg)
        {
            return _notificationService.GetAllByUserJmbg(jmbg);
        }

        public void Delete(int notificationId)
        {
            _notificationService.Delete(notificationId);
        }

        public Notification GetOneById(int notificationId)
        {
            return _notificationService.GetOneById(notificationId);
        }

        public void CreatePatientNotificationForAppointmentReschedule(String patientJmbg, String doctorFullName, DateTime oldAppointmentTime, DateTime newAppointmentTime, String roomName)
        {
            _notificationService.CreatePatientNotificationForAppointmentReschedule(patientJmbg, doctorFullName, oldAppointmentTime, newAppointmentTime, roomName);
        }

        public void CreateUserNotification(String title, String description, String doctorJmbg)
        {
            _notificationService.CreateUserNotification(title, description, doctorJmbg);
        }

        public void CreateDoctorNotificationForEmergency(String doctorJmbg, String patientFullName, DateTime oldAppointmentTime, DateTime newAppointmentTime, DateTime emergencyTime, String roomName)
        {
            _notificationService.CreateDoctorNotificationForEmergency(doctorJmbg, patientFullName, oldAppointmentTime, newAppointmentTime, emergencyTime, roomName);
        }

        public Notification Create(string title, string description, DateTime startTime, string receiverJmbg, bool seen)
        {
            return _notificationService.Create(title, description, startTime, receiverJmbg, seen);

        }
        public void DeleteAll(String patientId)
        {
            _notificationService.DeleteAll(patientId);
        }

        public void CreatePatientNotification(String patientJmbg)
        {
            _notificationService.CreatePatientNotifications(patientJmbg);
        }
    }
}
