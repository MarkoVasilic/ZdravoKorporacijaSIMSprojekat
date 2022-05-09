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

        public List<Notification> GetAllNotifications()
        {
            return notificationService.GetAllNotifications();
        }

        public List<Notification> GetAllFutureAppointmentsByPatient()
        {
            return notificationService.GetNotificationsByPatientJmbg(App.loggedUser.Jmbg);
        }

        public void DeleteNotification(int notificationId)
        {
            notificationService.DeleteNotification(notificationId);
        }

        public Notification GetOneById(int notificationId)
        {
            return notificationService.GetOneById(notificationId);
        }

        public void CreatePatientNotificationForAppointmentReschedule(String patientJmbg, String doctorFullName, DateTime oldAppointmentTime, DateTime newAppointmentTime, String roomName)
        {
            notificationService.CreatePatientNotificationForAppointmentReschedule(patientJmbg, doctorFullName, oldAppointmentTime, newAppointmentTime, roomName);
        }

        public void CreateDoctorNotificationForEmergency(String doctorJmbg, String patientFullName, DateTime oldAppointmentTime, DateTime newAppointmentTime, DateTime emergencyTime, String roomName)
        {
            notificationService.CreateDoctorNotificationForEmergency(doctorJmbg, patientFullName, oldAppointmentTime, newAppointmentTime, emergencyTime, roomName);
        }
    }
}
