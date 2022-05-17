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
        private readonly NotificationRepository notificationRepository;
        private readonly PrescriptionService prescriptionService;


        public NotificationService() { }
        public NotificationService(NotificationRepository repository, PrescriptionService prescriptionService)
        {
            this.notificationRepository = repository;
            this.prescriptionService = prescriptionService;
        }

        private int GenerateNewId()
        {
            try
            {
                List<Notification> notifications = notificationRepository.FindAll();
                int currentMax = notifications.Max(obj => obj.Id);
                return currentMax + 1;
            }
            catch
            {
                return 1;
            }
        }

        public List<Notification> GetNotificationsByPatientJmbg(String userJmbg)
        {
            List<Notification> notifications = new List<Notification>();
            notifications = notificationRepository.FindAllByUserJmbg(userJmbg);
            return notifications;
        }

        public void DeleteNotification(int notificationId)
        {
            if (notificationRepository.FindOneById(notificationId) == null)
            {
                throw new Exception("Notification with that id doesn't exist!");
            }
            else
            {
                notificationRepository.RemoveNotification(notificationId);
            }
        }

        public List<Notification> GetAllNotifications()
        {
            return notificationRepository.FindAll();
        }
        public Notification GetOneById(int notificationId)
        {
            return notificationRepository.FindOneById(notificationId);
        }

        public Notification CreateNotification(string title, string description, DateTime startTime, string receiverJmbg, bool seen, int newId)
        {
            int id = GenerateNewId();
            Notification notification = new Notification(title, description, startTime, receiverJmbg, seen, id);
            notificationRepository.SaveNotification(notification);
            return notification;

        }

        public void CreatePatientNotificationForAppointmentReschedule(String patientJmbg, String doctorFullName, DateTime oldAppointmentTime, DateTime newAppointmentTime, String roomName)
        {
            int id = GenerateNewId();
            String title = "Reschedule of appointment";
            String description = "Your appointment with doctor " + doctorFullName + " on date " + oldAppointmentTime + " in room " + roomName + " have been rescheduled to date " + newAppointmentTime;
            Notification notification = new Notification(title, description, DateTime.Now, patientJmbg, false, id);
            notificationRepository.SaveNotification(notification);
        }

        public void CreateDoctorNotificationForEmergency(String doctorJmbg, String patientFullName, DateTime oldAppointmentTime, DateTime newAppointmentTime, DateTime emergencyTime, String roomName)
        {
            int id = GenerateNewId();
            String title = "Emergency appointment";
            String description = "Your appointment with patient " + patientFullName + " on date " + oldAppointmentTime + " in room " + roomName + " have been rescheduled to date " + newAppointmentTime + ", there is emergency you need to attend now at room " +
                roomName + " on date " + emergencyTime;
            Notification notification = new Notification(title, description, DateTime.Now, doctorJmbg, false, id);
            notificationRepository.SaveNotification(notification);
        }

        public List<Notification> CreatePatientNotifications()
        {
            List<Notification> notificationsList = new List<Notification>();
            List<Prescription> prescriptionsList = new List<Prescription>();
            int numberOfMedNotification = 0;
            int Id = 0;
            String Desc = "";
            String Title = "";
            DateTime StartTime = System.DateTime.Now;
            String userJmbg = App.loggedUser.Jmbg; //ostaje
            bool Seen = false; //ostaje


            prescriptionsList = prescriptionService.GetAllByPatient(App.loggedUser.Jmbg); //dobavljamo sve terapije po pacijentu koji je ulogovan
            foreach (Prescription prescription in prescriptionsList) //prolazimo kroz sve terapije i za svaku pojedinacno kreiramo sve notifikacije
            {
                numberOfMedNotification = (prescription.To - prescription.From).Days * (24 / prescription.Frequency); //koliko ukupno puta treba popiti lijek
                for (int i = 0; i < numberOfMedNotification; i++) //kreiramo koliko je potrebno notifikacija
                {
                    Id = GenerateNewId(); //generisemo novi ID za lijek
                    Title = prescription.Medication; //naslov = paracetamol
                    StartTime = prescription.From.AddHours(i*prescription.Frequency); //startTime  = StartTime lijeka
                    Desc = "Morate da popijete lijek " + prescription.Medication + " , " + "Kolicina: " + prescription.Amount + " , " + "Satnica: " + StartTime.Hour +":"+StartTime.Minute + "h !";
                    
                        Notification notification = new Notification(Title, Desc, StartTime, userJmbg, Seen, Id);
                        CreateNotification(Title, Desc, StartTime, userJmbg, Seen, Id);
                        notificationsList.Add(notification);
                    
                }
            }

            return notificationsList;
        }

        public List<Notification> ShowPatientNotification()
        {
            List<Notification> notificationsListToDisplay = new List<Notification>();
            List<Notification> returnList = new List<Notification>();
            notificationsListToDisplay = notificationRepository.FindAllByUserJmbg(App.loggedUser.Jmbg);
            Console.WriteLine("Medication StartTime: " + notificationsListToDisplay[0].StartTime);
            Console.WriteLine("Current DateTime :  " + System.DateTime.Now);
            Console.WriteLine("Notifications to be Made");
            Console.WriteLine("-------------------------");

                for (int i=0; i < notificationsListToDisplay.Count; i++) {
                    if ((((System.DateTime.Now.Hour - notificationsListToDisplay[i].StartTime.Hour) <= 1) && ((System.DateTime.Now.Hour - notificationsListToDisplay[i].StartTime.Hour) > -8)) || (System.DateTime.Now > notificationsListToDisplay[i].StartTime))
                        {   
                        returnList.Add(notificationsListToDisplay[i]);
                        }
            }
            return returnList;

        }


    }
}
