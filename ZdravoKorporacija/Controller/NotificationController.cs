using System;
using System.Collections.Generic;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.Controller
{
    public class NotificationController
    {

        private readonly NotificationService notificationService = new NotificationService();

        public NotificationController(NotificationService Service)
        {
            this.notificationService = Service;
        }

        public List<Notification> GetAll()
        {
            return notificationService.GetAll();
        }

        public List<Notification> GetAllByPatientJmbg()
        {
            return notificationService.GetAllByPatientJmbg(App.loggedUser.Jmbg);
        }

        public List<Notification> GetAllByUserJmbg(String jmbg)
        {
            return notificationService.GetAllByUserJmbg(jmbg);
        }

        public void Delete(int notificationId)
        {
            notificationService.Delete(notificationId);
        }

        public Notification GetOneById(int notificationId)
        {
            return notificationService.GetOneById(notificationId);
        }

        public void CreatePatientNotificationForAppointmentReschedule(String patientJmbg, String doctorFullName, DateTime oldAppointmentTime, DateTime newAppointmentTime, String roomName)
        {
            notificationService.CreatePatientNotificationForAppointmentReschedule(patientJmbg, doctorFullName, oldAppointmentTime, newAppointmentTime, roomName);
        }

        public void CreateUserNotification(String title, String description, String doctorJmbg)
        {
            notificationService.CreateUserNotification(title, description, doctorJmbg);
        }

        public void CreateDoctorNotificationForEmergency(String doctorJmbg, String patientFullName, DateTime oldAppointmentTime, DateTime newAppointmentTime, DateTime emergencyTime, String roomName)
        {
            notificationService.CreateDoctorNotificationForEmergency(doctorJmbg, patientFullName, oldAppointmentTime, newAppointmentTime, emergencyTime, roomName);
        }

        public Notification Create(string title, string description, DateTime startTime, string receiverJmbg, bool seen)
        {
            return notificationService.Create(title, description, startTime, receiverJmbg, seen);

        }
        public void DeleteAll(String patientId)
        {
            notificationService.DeleteAll(patientId);
        }

        public void CreatePatientNotification(String patientJmbg)
        {
            notificationService.CreatePatientNotifications(patientJmbg);
        }
    }
}
