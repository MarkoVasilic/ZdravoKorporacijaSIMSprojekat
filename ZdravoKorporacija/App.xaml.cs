using Controller;
using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Windows;
using ZdravoKorporacija.Controller;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Model;
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
            EquipmentService equipmentService = new EquipmentService(equipmentRepository, roomRepository);
            EquipmentController equipmentController = new EquipmentController(equipmentService);
            MedicalRecordRepository medicalRecordRepository = new MedicalRecordRepository();
            AnamnesisRepository anamnesisRepository = new AnamnesisRepository();
            AnamnesisService anamnesisService = new AnamnesisService(anamnesisRepository, medicalRecordRepository);
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
            DisplacementService displacementService = new DisplacementService(displacementRepository, equipmentRepository, roomRepository);
            AdvancedRenovationSeparationRepository advancedRenovationSeparationRepository = new AdvancedRenovationSeparationRepository();
            AdvancedRenovationSeparationService advancedRenovationSeparationService = new AdvancedRenovationSeparationService(advancedRenovationSeparationRepository, roomService);
            AdvancedRenovationSeparationController advancedRenovationSeparationController = new AdvancedRenovationSeparationController(advancedRenovationSeparationService);
            AdvancedRenovationJoiningRepository advancedRenovationJoiningRepository = new AdvancedRenovationJoiningRepository();
            AdvancedRenovationJoiningService advancedRenovationJoiningService = new AdvancedRenovationJoiningService(advancedRenovationJoiningRepository, roomService, appointmentService, basicRenovationService, equipmentService, scheduleService, displacementService);
            AdvancedRenovationJoiningController advancedRenovationJoiningController = new AdvancedRenovationJoiningController(advancedRenovationJoiningService);
            RatingRepository ratingRepository = new RatingRepository();
            RatingService ratingService = new RatingService(ratingRepository, appointmentRepository);
            ratingController = new RatingController(ratingService);
            AbsenceRequestRepository absenceRequestRepository = new AbsenceRequestRepository();
            AbsenceRequestService absenceRequestService = new AbsenceRequestService(absenceRequestRepository, scheduleService, doctorRepository, appointmentRepository);
            AbsenceRequestController absenceRequestController = new AbsenceRequestController(absenceRequestService);
            UserService userService = new UserService(doctorRepository, managerRepository, secretaryRepository);
            MeetingService meetingService = new MeetingService(meetingRepository, roomRepository, scheduleService, userService);
            MeetingControler meetingControler = new MeetingControler(meetingService);
            NoteRepository noteRepository = new NoteRepository();
            NoteService noteService = new NoteService(noteRepository);
            noteController = new NoteController(noteService);

            /*List<PossibleAppointmentsDTO> possibleAppointments = appointmentController.GetPossibleAppointmentsByDoctor(
                "1111111111111", "1231231231231", new DateTime(2022, 10, 10),
                new DateTime(2022, 10, 20), 90, "doctor", 7);

            int i = -1;
            foreach (PossibleAppointmentsDTO appointment in possibleAppointments)
            {
                i++;
                Console.WriteLine("Id = " + i);
                appointment.ToStringPossible();
            }*/
            /*Console.WriteLine("Izaberite jedan od ponudjenih termina");
            Console.WriteLine("Choose appointment: ");
            int app = Int16.Parse(Console.ReadLine());
            PossibleAppointmentsDTO choosenAppointment = possibleAppointments[app];
            appointmentController.CreateAppointmentByDoctor(choosenAppointment);*/

            //String a = doctorRepository.FindOneByJmbg("4444444444444").SpecialtyType;
            //Console.Write(a);
        }


        


    }
}
