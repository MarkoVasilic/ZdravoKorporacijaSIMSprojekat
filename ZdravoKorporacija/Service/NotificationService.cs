using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                throw new Exception("Appointment with that id doesn't exist!");
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
            Notification notification = new Notification(title, description,startTime,receiverJmbg,seen, id);
            notificationRepository.SaveNotification(notification);
            return notification;

        }

        public List<Notification> CreatePatientNotifications() 
        {
            List<Notification> notificationsList = new List<Notification>();
            List<Prescription>  prescriptionsList = new List<Prescription>();
            int numberOfMedNotification = 0;
            int Id = 0;
            String Desc = "";
            String Title = "";
            DateTime StartTime = System.DateTime.Now;
            String userJmbg = "1111111111111"; //ostaje
            bool Seen = false; //ostaje


            prescriptionsList = prescriptionService.GetAllByPatient("1111111111111"); //dobavljamo sve terapije po pacijentu koji je ulogovan
            foreach(Prescription prescription in prescriptionsList) //prolazimo kroz sve terapije i za svaku pojedinacno kreiramo sve notifikacije
            {
                numberOfMedNotification = (prescription.To - prescription.From).Days*(24/prescription.Frequency); //koliko ukupno puta treba popiti lijek
               for(int i = 0; i < numberOfMedNotification; i++) //kreiramo koliko je potrebno notifikacija
                {
                    Id = GenerateNewId(); //generisemo novi ID za lijek
                    Title ="Popijte lijek  "+prescription.Medication; //naslov = paracetamol
                    StartTime = prescription.From.AddHours(i*prescription.Frequency); //startTime  = StartTime lijeka
                    Desc = "Obavjestenje: " + "Morate da popijete lijek " + prescription.Medication + " " + "Kolicina: " + prescription.Amount + " " + "Satnica: " + StartTime.Hour + "h !";
                    
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
            notificationsListToDisplay = notificationRepository.FindAllByUserJmbg("1111111111111");
            Console.WriteLine(notificationsListToDisplay[0].StartTime);
            Console.WriteLine(System.DateTime.Now);

                for (int i=0; i < notificationsListToDisplay.Count; i++) {
                Console.WriteLine("Razlika je " + (System.DateTime.Now - notificationsListToDisplay[i].StartTime).Hours);
                if ((System.DateTime.Now - notificationsListToDisplay[i].StartTime).Hours<=1)
                {
                    Console.WriteLine("Ovo upisujem " + notificationsListToDisplay[i].StartTime);
                    returnList.Add(notificationsListToDisplay[i]);
                }
                    

            }
            return returnList;

        }


    }
}
