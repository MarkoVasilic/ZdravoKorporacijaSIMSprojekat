using Controller;
using Model;
using Repository;
using Service;
using System;
using System.Windows;
using ZdravoKorporacija.Controller;
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

        public static RatingController? ratingController { get; set; }

        public static NoteController? noteController { get; set; }
        public App()
        {

            ManagerRepository managerRepository = new ManagerRepository();
            SecretaryRepository secretaryRepository = new SecretaryRepository();
            DoctorRepository doctorRepository = new DoctorRepository();
            BasicRenovationRepository basicRenovationRepository = new BasicRenovationRepository();
            AdvancedRenovationJoiningRepository advancedRenovationJoining = new AdvancedRenovationJoiningRepository();
            AdvancedRenovationSeparationRepository advancedRenovationSeparation =
                new AdvancedRenovationSeparationRepository();
            MeetingRepository meetingRepository = new MeetingRepository();
            RoomRepository roomRepository = new RoomRepository();
            PatientRepository patientRepository = new PatientRepository();
            DisplacementRepository displacementRepository = new DisplacementRepository();
            PatientService patientService = new PatientService(patientRepository);
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            AppointmentService appointmentService = new AppointmentService(appointmentRepository, patientRepository, doctorRepository, roomRepository);
            patientController = new PatientController(patientService, appointmentService);
            ScheduleService scheduleService = new ScheduleService(appointmentRepository, patientRepository,
                doctorRepository, roomRepository, basicRenovationRepository, advancedRenovationJoining,
                advancedRenovationSeparation, managerRepository, secretaryRepository, meetingRepository);
            EmergencyService emergencyService = new EmergencyService(appointmentRepository, patientRepository,
                doctorRepository, roomRepository, basicRenovationRepository, advancedRenovationJoining,
                advancedRenovationSeparation, scheduleService);
            appointmentController = new AppointmentController(appointmentService, scheduleService, emergencyService);
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
            AnamnesisService anamnesisService = new AnamnesisService(anamnesisRepository, medicalRecordRepository, doctorRepository);
            PrescriptionRepository prescriptionRepository = new PrescriptionRepository();
            MedicalRecordService medicalRecordService = new MedicalRecordService(medicalRecordRepository, anamnesisRepository, prescriptionRepository, patientRepository, appointmentRepository);
            MedicationRepository medicationRepository = new MedicationRepository();
            MedicationService medicationService = new MedicationService(medicationRepository);
            MedicationController medicationController = new MedicationController(medicationService);
            PrescriptionService prescriptionService = new PrescriptionService(prescriptionRepository, medicalRecordRepository, patientRepository, medicationRepository);
            medicalRecordController = new MedicalRecordController(medicalRecordService, anamnesisService, prescriptionService);
            NotificationRepository notificationRepository = new NotificationRepository();
            NotificationService notificationService = new NotificationService(notificationRepository, prescriptionService);
            notificationController = new NotificationController(notificationService);
            BasicRenovationService basicRenovationService = new BasicRenovationService(basicRenovationRepository, roomRepository);
            BasicRenovationController basicRenovationController = new BasicRenovationController(basicRenovationService);
            RoomService roomService = new RoomService(roomRepository);
            AdvancedRenovationSeparationRepository advancedRenovationSeparationRepository = new AdvancedRenovationSeparationRepository();
            AdvancedRenovationSeparationService advancedRenovationSeparationService = new AdvancedRenovationSeparationService(advancedRenovationSeparationRepository, roomService);
            AdvancedRenovationSeparationController advancedRenovationSeparationController = new AdvancedRenovationSeparationController(advancedRenovationSeparationService);
            AdvancedRenovationJoiningRepository advancedRenovationJoiningRepository = new AdvancedRenovationJoiningRepository();
            AdvancedRenovationJoiningService advancedRenovationJoiningService = new AdvancedRenovationJoiningService(advancedRenovationJoiningRepository, roomService, appointmentService, basicRenovationService, equipmentService);
            AdvancedRenovationJoiningController advancedRenovationJoiningController = new AdvancedRenovationJoiningController(advancedRenovationJoiningService);
            RatingRepository ratingRepository = new RatingRepository();
            RatingService ratingService = new RatingService(ratingRepository, appointmentRepository);
            ratingController = new RatingController(ratingService);
            AbsenceRequestRepository absenceRequestRepository = new AbsenceRequestRepository();
            AbsenceRequestService absenceRequestService = new AbsenceRequestService(absenceRequestRepository, scheduleService, doctorRepository);
            AbsenceRequestController absenceRequestController = new AbsenceRequestController(absenceRequestService);
            MeetingService meetingService = new MeetingService(meetingRepository, doctorRepository, managerRepository,
                secretaryRepository, roomRepository);
            MeetingControler meetingControler = new MeetingControler(meetingService);
            NoteRepository noteRepository = new NoteRepository();
            NoteService noteService = new NoteService(noteRepository);
            noteController = new NoteController(noteService);


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

            /*List<AbsenceRequest> rs = absenceRequestController.GetAllByDoctorJmbg("1231231231231");
            foreach(AbsenceRequest r in rs)
            {
                r.ToString();
            }*/
            //medicalRecordController.GetAllMedicalRecords();
            /*List<MedicalRecordDTO> medicalRecordDTOs = new List<MedicalRecordDTO>(medicalRecordController.GetAllMedicalRecords());
            foreach (MedicalRecordDTO medicalRecordDTO in medicalRecordDTOs)
                medicalRecordDTO.ToString();*/


            //LISTA ZAKAZANIH TERMINA I KARTON ZA ODREDJENI TERMIN
            /*List<AppointmentDTO> appointmentListDoctor = new List<AppointmentDTO>(appointmentController.GetAppointmentsByDoctorJmbgDTO("4444444444444"));
            foreach (AppointmentDTO appointmentDTO in appointmentListDoctor)
                appointmentDTO.ToString();
            Console.WriteLine("Unesite Id appointmenta za koji zelite medicinski karton");
            int app = Int16.Parse(Console.ReadLine());
            MedicalRecordDTO medicalRecordDTO = medicalRecordController.GetOneMedicalRecorByAppointmentId(app);
            medicalRecordDTO.ToString();*/

            //medicalRecordController.GetOneMedicalRecorByPatientJmbg("7778889994445");
            //medicalRecordController.GetOneAnamnesisById(2);

            //ANAMNEZE
            //medicalRecordController.CreateAnamnesis("7778889994445", "novaDijagnoza", "noviIzvestaj");
            //medicalRecordController.ModifyAnamnesis(8, "modOdbranaDiagnose", "modOdbranaReport");

            //RECEPTI
            //medicalRecordController.CreatePrescription("1111111111111", "Berodual", "200mg", 8, new DateTime(2022, 6,6), new DateTime(2022, 7, 7));
            //medicalRecordController.ModifyPrescription(4, "Berodual", "500mg", 3, new DateTime(2022, 6, 6), new DateTime(2022, 7, 10));

            //ISPIS KARTONA PACIJENT JMBG(Anamneze pacijenta i recepti)
            /*MedicalRecordDTO medicalRecordDTO = medicalRecordController.GetOneMedicalRecorByPatientJmbg("7778889994445");
            medicalRecordDTO.ToString();*/

            //****************************
            //********************************************************KT4*****************************************************************************
            //****************************

            //* Create Appointment Doctor *//

            /*List<PossibleAppointmentsDTO> possibleAppointments = appointmentController.GetPossibleAppointmentsByDoctor("7458963215963", "1231231231231", new DateTime(2022, 7, 9), new DateTime(2022, 7, 10), 60, "doctor");
            int i = -1;
            foreach (PossibleAppointmentsDTO appointment in possibleAppointments)
            {
                i++;
                Console.WriteLine("Id = " + i);
                appointment.ToStringPossible();
            }
            Console.WriteLine("Choose appointment: ");
            int app = Int16.Parse(Console.ReadLine());
            PossibleAppointmentsDTO choosenAppointment = possibleAppointments[app];
            appointmentController.CreateAppointmentByDoctor(choosenAppointment);
            Console.WriteLine("Created appointment: ");
            choosenAppointment.ToStringChoosen();
            Console.WriteLine("Doctor notification created: ");
            notificationController.CreateNotification("Appointment", "You have new appointment scheduled!", choosenAppointment.StartTime, choosenAppointment.DoctorJmbg, false).ToStringNotification();
            Console.WriteLine("Patient notification created: ");
            notificationController.CreateNotification("Appointment", "You have new appointment scheduled!", choosenAppointment.StartTime, choosenAppointment.PatientJmbg, false).ToStringNotification();
            */


            //*  Create Operation - samo kod sebe moze da zakaze i samo specijalista *// 

            //ako nije specijalista ne moze da zakaze
            //List<PossibleAppointmentsDTO> possibleAppointments = appointmentController.GetPossibleAppointmentsByDoctor("7458963215963", "4444444444444", new DateTime(2022, 7, 10), new DateTime(2022, 7, 11), 60, "doctor");

            /*List<PossibleAppointmentsDTO> possibleAppointments = appointmentController.GetPossibleAppointmentsByDoctor("7458963215963", "1231231231231", new DateTime(2022, 7, 10), new DateTime(2022, 7, 11), 60, "doctor");
            int i = -1;
            if (!doctorRepository.FindOneByJmbg("1231231231231").Specialty)
            {
                Console.WriteLine("Only doctors with specialization can schedule operation!");
            }
            else
            {
                foreach (PossibleAppointmentsDTO appointment in possibleAppointments)
                {
                    i++;
                    Console.WriteLine("Id = " + i);
                    appointment.ToStringPossible();
                }
                Console.WriteLine("Choose appointment: ");
                int app = Int16.Parse(Console.ReadLine());
                PossibleAppointmentsDTO choosenAppointment = possibleAppointments[app];
                appointmentController.CreateOperationAppointment(choosenAppointment);
                Console.WriteLine("Created appointment: ");
                choosenAppointment.ToStringChoosen();
                Console.WriteLine("Doctor notification created: ");
                notificationController.CreateNotification("Appointment", "You have new operation scheduled!", choosenAppointment.StartTime, choosenAppointment.DoctorJmbg, false).ToStringNotification();
                Console.WriteLine("Patient notification created: ");
                notificationController.CreateNotification("Appointment", "You have new operation scheduled!", choosenAppointment.StartTime, choosenAppointment.PatientJmbg, false).ToStringNotification();

            }*/

            /*List<Notification> notifications = notificationController.GetAllByUserJmbg("1231231231231");
            foreach(Notification n in notifications)
            {
                n.ToStringNotification();
            }*/

            //* VERIFIKACIJA I ODBIJANJE LEKA *//

            /*List<Medication> medications = medicationController.GetAllUnverifiedMedications();
            foreach(Medication medication in medications)
            {
                medication.toString();
            }*/

            //Verifikcija

            /*Console.WriteLine("Choose medication Id to verify: ");
            int app = Int16.Parse(Console.ReadLine());
            medicationController.VerifyMedication(app);*/

            //* Odbijanje *//

            /*Console.WriteLine("Choose medication Id to reject: ");
            int app = Int16.Parse(Console.ReadLine());
            Console.WriteLine("Enter reason of rejection: ");
            String reason = Console.ReadLine();
            medicationController.RejectMedication(app, reason);
            notificationController.CreateNotification("Medication rejection", reason, DateTime.Now.AddMinutes(1), "3434343434343", false); //manager jmbg
            */

            /* zahtev za odsustvo */
            //absenceRequestController.CreateAbsenceRequest(new DateTime(2022, 7, 20), new DateTime(2022, 7, 25), false, "Godisnji Odmor");

            //mora biti 2 dana ranije
            //absenceRequestController.CreateAbsenceRequest(new DateTime(2022, 5, 21), new DateTime(2022, 8, 12), false, "Godisnji odnomr");

            /* hitan zahtev */
            //absenceRequestController.CreateAbsenceRequest(new DateTime(2022, 7, 5), new DateTime(2022, 7, 10), true, "Hitno bolovanje");










            //*************************************
            //***********************************************************************************************************************************************
            //*************************************




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
            //equipmentController.CreateDisplacement(5, 7, 1, DateTime.Today);
            //equipmentService.EquipmentDisplacement();

            //POMERANJE OPREME - buducnost
            //equipmentController.CreateDisplacement(7, 5, 1, new DateTime(2022, 2, 2));
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

            //KREIRANJE LEKA
            /*List<String> ingredients = new List<String>();
            ingredients.Add("sastojak1");
            ingredients.Add("sastojak2");
            ingredients.Add("sastojak3");*/

            //medicationController.Create("Aspirin", ingredients, "zamena1");

            //PREGLED SVIH LEKOVA
            /*List<Medication> medications = new List<Medication>(medicationController.GetAll());
            foreach (Medication m in medications)
                m.toString();*/

            /*List<String> ingredients1 = new List<String>();
            ingredients1.Add("sastojak6");
            ingredients1.Add("sastojak5");
            ingredients1.Add("sastojak7");*/


            //MODIFIKACIJA LEKOVA
            //medicationController.Modify(3, "neki lek", ingredients, "zamena2");

            //PREGLED SVIH LEKOVA NAKON MODIFIKACIJE
            /*List<Medication> medications = new List<Medication>(medicationController.GetAll());
            foreach (Medication m in medications)
                m.toString();*/

            //PREGLED SVIH ODBIJENIH
            /*List<Medication> rejectedMedications = new List<Medication>(medicationController.GetRejected());
            foreach (Medication med in rejectedMedications)
                med.toString();*/


            //NAPREDNO RENOVIRANJE - RAZDVAJANJE
            /* int index = 0;
             List<PossibleAppointmentsDTO> possibleAppointmentsRenovation = new List<PossibleAppointmentsDTO>(appointmentController.GetPossibleAppointmentsByManager(12, new DateTime(2023, 5, 15), new DateTime(2023, 5, 16), 60));
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

             for (int i = 0; i < possibleAppointmentsRenovation.Count; i++)
             {
                 if (checkedAppointmentIndex == i)
                 {
                     advancedRenovationSeparationController.Create(possibleAppointmentsRenovation[i].RoomId, possibleAppointmentsRenovation[i].StartTime, possibleAppointmentsRenovation[i].Duration, "soba razdvojena 1", "soba razdvojena 2", "razdvojena je1", "razdvojena je2", RoomType.EXAMINATION, RoomType.CONFERENCE);
                 }
             }*/

            //advancedRenovationSeparationController.Separate();
            //appointmentController.GetPossibleAppointmentsForAbsence("1231231231231", DateTime.Now.AddDays(3), DateTime.Now.AddDays(15), 5);

            //NAPREDNO RENOVIRANJE - SPAJANJE
            /* int index = 0;
              List<PossibleAppointmentsDTO> possibleAppointmentsRenovation = new List<PossibleAppointmentsDTO>(appointmentController.GetPossibleAppointmentsForRoomJoin(23, 5, new DateTime(2023, 5, 4), new DateTime(2023, 5, 16), 60));
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

              for (int i = 0; i < possibleAppointmentsRenovation.Count; i++)
              {
                  if (checkedAppointmentIndex == i)
                  {
                     advancedRenovationJoiningController.Create(12,5, possibleAppointmentsRenovation[i].StartTime, possibleAppointmentsRenovation[i].Duration, "spojena soba", "moja spojena soba", RoomType.CONFERENCE);
                  }
              }*/


            //advancedRenovationJoiningController.Join();


            //ratingController.Create(1, 5, 5, "Fenomenalno");
            //int a = (int)(DateTime.Today.AddDays(2) - DateTime.Today).TotalDays;
            //Console.WriteLine(a);

            /* List<EquipmentDTO> equipmentDTOs = equipmentController.GetAllByRoomId(7);
             foreach (EquipmentDTO eq in equipmentDTOs)
                 eq.toString();*/


            //NE KOMENTARISATI - KAKO BI SE DESILO SVAKI PUT KAD SE POKRENE APLIKACIJA
            //equipmentController.EquipmentDisplacement();
            //advancedRenovationJoiningController.JoinRooms();
            //advancedRenovationSeparationController.SeparateRooms();

        }

    }
}
