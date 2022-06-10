using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;

namespace ZdravoKorporacija.Service
{
    public class NotificationService
    {
        private readonly NotificationRepository _notificationRepository;
        private readonly PrescriptionService _prescriptionService;


        public NotificationService() { }
        public NotificationService(NotificationRepository repository, PrescriptionService prescriptionService)
        {
            this._notificationRepository = repository;
            this._prescriptionService = prescriptionService;
        }


        public int GenerateNewId()
        {
            try
            {
                List<Notification> notifications = _notificationRepository.FindAll();
                int currentMax = notifications.Max(obj => obj.Id);
                return currentMax + 1;
            }
            catch
            {
                return 1;
            }
        }

        public List<Notification> GetAllByPatientJmbg(String userJmbg)
        {
            List<Notification> notifications = new List<Notification>();
            notifications = _notificationRepository.FindAllByUserJmbg(userJmbg);
            return notifications;
        }

        public void Delete(int notificationId)
        {
            if (_notificationRepository.FindOneById(notificationId) == null)
            {
                throw new Exception("Notification with that id doesn't exist!");
            }
            else
            {
                _notificationRepository.RemoveNotification(notificationId);
            }
        }

        public List<Notification> GetAll()
        {
            return _notificationRepository.FindAll();
        }
        public Notification GetOneById(int notificationId)
        {
            return _notificationRepository.FindOneById(notificationId);
        }

        public List<Notification> GetAllByUserJmbg(String Jmbg)
        {
            return _notificationRepository.FindAllByUserJmbg(Jmbg);
        }

        public Notification Create(string title, string description, DateTime startTime, string receiverJmbg, bool seen)
        {
            int id = GenerateNewId();
            Notification notification = new Notification(title, description, startTime, receiverJmbg, seen, id);
            _notificationRepository.SaveNotification(notification);
            return notification;

        }

        public void CreatePatientNotificationForAppointmentReschedule(String patientJmbg, String doctorFullName, DateTime oldAppointmentTime, DateTime newAppointmentTime, String roomName)
        {
            int id = GenerateNewId();
            String title = "Reschedule of appointment";
            String description = "Your appointment with doctor " + doctorFullName + " on date " + oldAppointmentTime + " in room " + roomName + " have been rescheduled to date " + newAppointmentTime;
            Notification notification = new Notification(title, description, DateTime.Now, patientJmbg, false, id);
            _notificationRepository.SaveNotification(notification);
        }

        public void CreateUserNotification(String title, String description, String doctorJmbg)
        {
            int id = GenerateNewId();
            Notification notification = new Notification(title, description, DateTime.Now, doctorJmbg, false, id);
            _notificationRepository.SaveNotification(notification);
        }

        public void CreateDoctorNotificationForEmergency(String doctorJmbg, String patientFullName, DateTime oldAppointmentTime, DateTime newAppointmentTime, DateTime emergencyTime, String roomName)
        {
            int id = GenerateNewId();
            String title = "Emergency appointment";
            String description = "Your appointment with patient " + patientFullName + " on date " + oldAppointmentTime + " in room " + roomName + " have been rescheduled to date " + newAppointmentTime + ", there is emergency you need to attend now at room " +
                roomName + " on date " + emergencyTime;
            Notification notification = new Notification(title, description, DateTime.Now, doctorJmbg, false, id);
            _notificationRepository.SaveNotification(notification);
        }

        public List<Notification> CreatePatientNotifications(String patientJmbg)
        {
            int numberOfMedNotification;
            string Description, Title, userJmbg;
            DateTime StartTime;
            bool Seen;
            InitializeNotificationParameters(patientJmbg, out numberOfMedNotification, out Description, out Title, out StartTime, out userJmbg, out Seen);

            List<Notification> notificationsList = new List<Notification>();
            List<Prescription> prescriptionsList = _prescriptionService.GetAllByPatient(patientJmbg);
            foreach (Prescription prescription in prescriptionsList)
            {
                numberOfMedNotification = (prescription.To - prescription.From).Days * (24 / prescription.Frequency);
                for (int i = 0; i < numberOfMedNotification; i++)
                {
                    Title = prescription.Medication;
                    StartTime = prescription.From.AddHours(i * prescription.Frequency);
                    Description = "Morate da popijete lijek " + prescription.Medication + " , " + "Kolicina: " + prescription.Amount + " , " + "Satnica: " + StartTime.Hour + ":" + StartTime.Minute + "h !";
                    Notification notification = Create(Title, Description, StartTime, userJmbg, Seen);
                    notificationsList.Add(notification);
                }
            }

            return notificationsList;
        }

        private static void InitializeNotificationParameters(string patientJmbg, out int numberOfMedNotification, out string Description, out string Title, out DateTime StartTime, out string userJmbg, out bool Seen)
        {
            numberOfMedNotification = 0;
            Description = "";
            Title = "";
            StartTime = System.DateTime.Now;
            userJmbg = patientJmbg;
            Seen = false;
        }

        public List<Notification> ShowPatientNotification()
        {
            List<Notification> returnList = new List<Notification>();
            List<Notification> notificationsListToDisplay = _notificationRepository.FindAllByUserJmbg(App.loggedUser.Jmbg);
            for (int i = 0; i < notificationsListToDisplay.Count; i++)
            {
                if (IsNotificationReadyToDisplay(notificationsListToDisplay, i))
                    returnList.Add(notificationsListToDisplay[i]);
            }
            return returnList;
        }

        private static bool IsNotificationReadyToDisplay(List<Notification> notificationsListToDisplay, int i)
        {
            return ((notificationsListToDisplay[i].StartTime - DateTime.Now).Hours <= 1 && (notificationsListToDisplay[i].StartTime - DateTime.Now).Hours > 0) || notificationsListToDisplay[i].StartTime < DateTime.Now;
        }

        public void DeleteAll(String patientJmbg)
        {
            List<Notification> notifications = new List<Notification>(_notificationRepository.FindAllByUserJmbg(patientJmbg));
            for(int i = 0; i < notifications.Count; ++i)
                Delete(notifications[i].Id);
        }

        public Notification CreateNotification(Notification notification)
        {
            notification.Id = GenerateNewId();
            _notificationRepository.SaveNotification(notification);
            return notification;
        }


    }
}
