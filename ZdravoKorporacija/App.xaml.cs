using Controller;
using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Windows;
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Repository;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija
{
    public partial class App : Application
    {
        public static User loggedUser { get; set; }
        public static String userRole { get; set; }

        public static Window currentWindow { get; set; }
        public static PatientController? patientController { get; set; }
        public static RoomController? roomController { get; set; }

        public static DoctorController? doctorController { get; set; }

        public static SecretaryController? secretaryController { get; set; }
        public static ManagerController? managerController { get; set; }
        public static AppointmentController? appointmentController { get; set; }

        public static MedicalRecordController? medicalRecordController { get; set; }

        public static NotificationController? notificationController { get; set; }
        public App()
        {

            ManagerRepository managerRepository = new ManagerRepository();
            SecretaryRepository secretaryRepository = new SecretaryRepository();
            DoctorRepository doctorRepository = new DoctorRepository();
            BasicRenovationRepository basicRenovationRepository = new BasicRenovationRepository();
            RoomRepository roomRepository = new RoomRepository();
            PatientRepository patientRepository = new PatientRepository();
            DisplacementRepository displacementRepository = new DisplacementRepository();
            PatientService patientService = new PatientService(patientRepository);
            patientController = new PatientController(patientService);
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            AppointmentService appointmentService = new AppointmentService(appointmentRepository, patientRepository, doctorRepository, roomRepository, basicRenovationRepository);
            appointmentController = new AppointmentController(appointmentService);
            DoctorService doctorService = new DoctorService(doctorRepository);
            doctorController = new DoctorController(doctorService);
            ManagerService managerService = new ManagerService(managerRepository);
            SecretaryService secretaryService = new SecretaryService(secretaryRepository);
            managerController = new ManagerController(managerService);
            secretaryController = new SecretaryController(secretaryService);
            EquipmentRepository equipmentRepository = new EquipmentRepository();
            EquipmentService equipmentService = new EquipmentService(equipmentRepository, roomRepository, displacementRepository);
            EquipmentController equipmentController = new EquipmentController(equipmentService);
            MedicalRecordRepository medicalRecordRepository = new MedicalRecordRepository();
            AnamnesisRepository anamnesisRepository = new AnamnesisRepository();
            AnamnesisService anamnesisService = new AnamnesisService(anamnesisRepository, medicalRecordRepository);
            PrescriptionRepository prescriptionRepository = new PrescriptionRepository();
            MedicalRecordService medicalRecordService = new MedicalRecordService(medicalRecordRepository, anamnesisRepository, prescriptionRepository, patientRepository, appointmentRepository);
            MedicationRepository medicationRepository = new MedicationRepository();
            MedicationService medicationService = new MedicationService();
            PrescriptionService prescriptionService = new PrescriptionService(prescriptionRepository, medicalRecordRepository, patientRepository, medicationRepository);
            medicalRecordController = new MedicalRecordController(medicalRecordService, anamnesisService, prescriptionService);
            NotificationRepository notificationRepository = new NotificationRepository();
            NotificationService notificationService = new NotificationService(notificationRepository, prescriptionService);
            notificationController = new NotificationController(notificationService);
            BasicRenovationService basicRenovationService = new BasicRenovationService(basicRenovationRepository, roomRepository);
            BasicRenovationController basicRenovationController = new BasicRenovationController(basicRenovationService);


            /*List<String> alergeni = new List<String> { "prvi alergen", "drugi alergen", "treci alergen" };
            patientController.CreatePatient(false, alergeni, BloodType.A_POSITIVE, "milos", "milosevic", "mikimilane", "mackacka",
                "7458963215963", DateTime.Now, Gender.MALE, "milos@milos.com", "456789", "neka dresa 12");*/

            /*
            patientController.CreatePatient(patient);
            patientController.CreatePatient(patient1);
            patientController.CreatePatient(patient2);
            patientController.CreatePatient(patient3);
            patientController.CreatePatient(patient4);
            patientController.CreatePatient(patient5);
            patientController.CreatePatient(patient6);
            patientController.CreatePatient(patient7);
            patientController.CreatePatient(patient8);
            patient8.Address = "jbgaa";
            patientController.ModifyPatient(patient8);
            appointmentController.CreateAppointmentByDoctor(DateTime.Now, 15, "0123");
            appointmentController.ModifyAppointment(11, new DateTime(2010, 2, 2));
            appointmentController.CreateAppointmentByPatient(DateTime.Now, "1231231231231");
            Console.WriteLine(appointmentController.CreateAppointmentByPatient(DateTime.Now, "789"));
            */

            //ODBRANA
            //Console.WriteLine("Create appointment by doctor = " + appointmentController.CreateAppointmentByDoctor(new DateTime(2000, 3, 3), 44, "1111111111111"));
            //Console.WriteLine("Create appointment by patient = " + appointmentController.CreateAppointmentByPatient(new DateTime(2090, 2, 2), "1231231231231"));
            //Console.WriteLine("Modify result = " + appointmentController.ModifyAppointment(38, new DateTime(2035, 5, 5)));
            //Console.WriteLine("Delete result = " + appointmentController.DeleteAppointment(38));

            //**********************************************************       KT3      ********************************************************************





            //LISTA ZAKAZANIH TERMINA I KARTON ZA ODREDJENI TERMIN
            /*
            List<AppointmentDTO> appointmentListDoctor = new List<AppointmentDTO>(appointmentController.GetAppointmentsByDoctorJmbgDTO("4444444444444"));
            foreach (AppointmentDTO appointmentDTO in appointmentListDoctor)
                appointmentDTO.ToString();
            Console.WriteLine("Unesite Id appointmenta za koji zelite medicinski karton");
            int app = Int16.Parse(Console.ReadLine());
            MedicalRecordDTO medicalRecordDTO = medicalRecordController.GetOneMedicalRecorByAppointmentId(app);
            medicalRecordDTO.ToString();*/

            //ANAMNEZE
            //medicalRecordController.CreateAnamnesis("7778889994445", "novaDijagnoza", "noviIzvestaj");
            //medicalRecordController.ModifyAnamnesis(8, "modOdbranaDiagnose", "modOdbranaReport");

            //RECEPTI
            //medicalRecordController.CreatePrescription("7778889994445", "Berodual", "200mg", 8, new DateTime(2022, 6,6), new DateTime(2022, 7, 7));
            //medicalRecordController.ModifyPrescription(4, "Berodual", "500mg", 3, new DateTime(2022, 6, 6), new DateTime(2022, 7, 10));

            //ISPIS KARTONA PACIJENT JMBG(Anamneze pacijenta i recepti)
            /*MedicalRecordDTO medicalRecordDTO = medicalRecordController.GetOneMedicalRecorByPatientJmbg("7778889994445");
            medicalRecordDTO.ToString();*/




            //***********************************************************************************************************************************************





            //ISPIS SVIH APPOINTMENTA
            /*List<Appointment> appointmentList = new List<Appointment>(appointmentController.GetAllAppointments());
            foreach (Appointment appointment in appointmentList)
              appointment.toString();*/

            //ISPIS SVIH APPOINTMENTA ZA DOKTORA
            /*List<Appointment> appointmentListDoctor = new List<Appointment>(appointmentController.GetAppointmentsByDoctorJmbg("1231231231231"));
            foreach (Appointment appointment in appointmentListDoctor)
              appointment.toString();*/

            //ISPIS SVIH APPOINTMENTA ZA PACIJENTA
            /*List<Appointment> appointmentListPatient = new List<Appointment>(appointmentController.GetAppointmentsByPatientJmbg("1111111111111"));
            foreach (Appointment appointment in appointmentListPatient)
              appointment.toString();*/

            //appointmentService.GetPossibleAppointmentsBySecretary("1111111111111", "1231231231231", 11, new DateTime(2023, 3, 3),
            //new DateTime(2023, 3, 6), 60, "time");

            //equipmentController.CreateEquipment("sto", true, 2, 7);
            //equipmentController.CreateEquipment("zavoj", false, 50, null);


            /*List<Displacement> displacementList = new List<Displacement>(equipmentController.GetAllDisplacements());
            foreach (Displacement d in displacementList)
                d.toString();*/





            /*secretaryController.CreateSecretary("Marko", "Vasilic", "mare", "konj", "1515151515151", new DateTime(1998, 9, 15),
                Gender.MALE, "marko@vasilic.com", "060606060", "Novi Sad");
            managerController.CreateManager("Nadja", "Kanjuh", "djana", "mama", "3434343434343", new DateTime(2000, 7, 24),
                Gender.FEMALE, "nadja@kanjuh.com", "070707070", "Novi Sad");*/
            //Notification n1 = new Notification("Nova", "2022", System.DateTime.Now, "1111111111111", false, 1);
            //  n1.ToStringNotification();

            /* List<Notification> notifications = new List<Notification>();
             notifications = notificationController.GetAllNotifications();
             foreach (Notification notification in notifications)
             {
                 notification.ToStringNotification();
             }*/

            //  Console.WriteLine(medicalRecordController.GetOneByPatientJmbg("1111111111111").PrescriptionIds[0]);

            //TESTIRANJE NOTIFIKACIJA I NJIHOVO PRIKAZIVANJE SAT VREMENA PRED EVENT
            //CreatePatientNotifications za pisanje u json i kreairanje za dati lijek, a ShowPatientNotification za slanje obavjestenja
            /* List<Notification> notificationList1 = new List<Notification>();
               notificationList1 = notificationService.CreatePatientNotifications(); 

           List<Notification> notificationList = new List<Notification>();
           notificationList = notificationService.ShowPatientNotification();
           foreach (Notification notification in notificationList)
               notification.ToStringNotification(); */

            //ISPIS OPREME - IMAM I NA FRONTU (UPRAVNIK)
            /* List<Equipment> equipmentList = new List<Equipment>(equipmentController.GetAllEquipment());
             foreach (Equipment equipment in equipmentList)
                 equipment.toString();*/


            //POMERANJE OPREME - danas
            //equipmentController.CreateDisplacement(7, 5, 1, DateTime.Today);
            //equipmentService.EquipmentDisplacement();

            //POMERANJE OPREME - buducnost
            //equipmentController.CreateDisplacement(5, 11, 1, new DateTime(2023, 3, 3));
            //equipmentService.EquipmentDisplacement();

            //ISPIS SVIH DOSTUPNIH TERMINA + OSNOVNO RENOVIRANJE (UPRAVNIK)
            /*int index = 0;
             List<PossibleAppointmentsDTO> possibleAppointmentsRenovation = new List<PossibleAppointmentsDTO>(appointmentController.GetPossibleAppointmentsByManager(12, new DateTime(2023, 3, 3), new DateTime(2023, 3, 6), 60));
             foreach (PossibleAppointmentsDTO possibleAppointment in possibleAppointmentsRenovation)
             {
                 Console.WriteLine(index.ToString());
                 possibleAppointment.toStringManager();
                 index++;
             }

             string checkedAppointment;

             Console.WriteLine("Unesite broj termina koji zelite");
             checkedAppointment = Console.ReadLine();
             int checkedAppointmentIndex = Convert.ToInt32(checkedAppointment);

             string description;
             Console.WriteLine("Unesite opis renoviranja:");
             description = Console.ReadLine();


             for (int i = 0; i<possibleAppointmentsRenovation.Count; i++)
             {
                 if(checkedAppointmentIndex == i)
                 {
                     basicRenovationController.CreateBasicRenovation(possibleAppointmentsRenovation[i].RoomId, possibleAppointmentsRenovation[i].StartTime, possibleAppointmentsRenovation[i].Duration, description);
                 }
             }*/


            //FILTRIRANJE - test
            /*List<EquipmentDTO> equipmentDTOs = new List<EquipmentDTO>(equipmentController.Filter(false));
            foreach (EquipmentDTO equipmentDTO in equipmentDTOs)
                equipmentDTO.toString();*/

            //PRETRAGA PO NAZIVU - test
           /* List<EquipmentDTO> equipmentDTOs = new List<EquipmentDTO>(equipmentController.Search("sto"));
            foreach (EquipmentDTO eq in equipmentDTOs)
                eq.toString(); */


        }

    }
}
