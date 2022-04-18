﻿using Controller;
using Repository;
using Service;
using System;
using System.Windows;

namespace ZdravoKorporacija
{
    public partial class App : Application
    {
        public PatientController? patientController { get; set; }
        public RoomController? roomController { get; set; }

        public DoctorController? doctorController { get; set; }
        public AppointmentController? appointmentController { get; set; }

        public App()
        {
            DoctorRepository doctorRepository = new DoctorRepository();
            RoomRepository roomRepository = new RoomRepository();
            PatientRepository patientRepository = new PatientRepository();
            PatientService patientService = new PatientService(patientRepository);
            patientController = new PatientController(patientService);
            AppointmentRepository appointmentRepository = new AppointmentRepository();
            AppointmentService appointmentService = new AppointmentService(appointmentRepository, patientRepository, doctorRepository, roomRepository);
            appointmentController = new AppointmentController(appointmentService);
            DoctorService doctorService = new DoctorService(doctorRepository);
            doctorController = new DoctorController(doctorService);
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


            //ISPIS SVIH APPOINTMENTA
            /*List<Appointment> appointmentList = new List<Appointment>(appointmentController.GetAllAppointments());
            foreach (Appointment appointment in appointmentList)
              appointment.toString();*/


            //ISPIS SVIH APPOINTMENTA ZA PACIJENTA
            /*List<Appointment> appointmentListPatient = new List<Appointment>(appointmentController.GetAppointmentsByPatientJmbg("1111111111111"));
            foreach (Appointment appointment in appointmentListPatient)
              appointment.toString();*/


            //ISPIS SVIH APPOINTMENTA ZA DOKTORA
            /*List<Appointment> appointmentListDoctor = new List<Appointment>(appointmentController.GetAppointmentsByDoctorJmbg("4444444444444"));
            foreach (Appointment appointment in appointmentListDoctor)
              appointment.toString();*/

            appointmentService.GetPossibleAppointmentsBySecretary("1111111111111", "1231231231231", 11, new DateTime(2023, 3, 3),
                new DateTime(2023, 3, 6), 60, "time");

        }


    }
}
