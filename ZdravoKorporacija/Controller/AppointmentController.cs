using Model;
using Service;
using System;
using System.Collections.Generic;
using ZdravoKorporacija.DTO;

namespace Controller
{
    public class AppointmentController
    {
        private readonly AppointmentService AppointmentService;

        public AppointmentController(AppointmentService appointmentService)
        {
            this.AppointmentService = appointmentService;
        }

        public List<Model.Appointment> GetAllAppointments()
        {
            return AppointmentService.GetAllAppointments();
        }
        public List<PossibleAppointmentsDTO> GetAllAppointmentsBySecretary()
        {
            return AppointmentService.GetAllAppointmentsBySecretary();
        }

        public List<PossibleAppointmentsDTO> GetAllFutureAppointmentsByPatient()
        {
            return AppointmentService.GetAllFutureAppointmentsByPatient();
        }

        public List<PossibleAppointmentsDTO> GetAllPastAppointmentsByPatient()
        {
            return AppointmentService.GetAllPastAppointmentsByPatient();
        }

        public void CreateAppointmentByPatient(DateTime startTime, String doctorJmbg)
        {
            AppointmentService.CreateAppointmentByPatient(startTime, doctorJmbg);
        }

        public void DeleteAppointment(int appointmentId)
        {
            AppointmentService.DeleteAppointment(appointmentId);
        }

        public void DeleteAppointmentsForOnePatient(String patientJmbg)
        {
            AppointmentService.DeleteAppointmentsForOnePatient(patientJmbg);
        }
        public void ModifyAppointment(int appointmentId, DateTime newDate)
        {
            AppointmentService.ModifyAppointment(appointmentId, newDate);
        }

        public Appointment GetOneById(int appointmentId)
        {
            return AppointmentService.GetOneById(appointmentId);
        }

        public List<Model.Appointment> GetAppointmentsByDoctorJmbg(String doctorJmbg)
        {
            return AppointmentService.GetAppointmentsByDoctorJmbg(doctorJmbg);
        }

        public List<AppointmentDTO> GetAppointmentsByDoctorJmbgDTO(String doctorJmbg)
        {
            return AppointmentService.GetAppointmentsByDoctorJmbgDTO(doctorJmbg);
        }

        public List<Model.Appointment> GetAppointmentsByPatientJmbg(String patientId)
        {
            return AppointmentService.GetAppointmentsByPatientJmbg(patientId);
        }
        public PossibleAppointmentsDTO FindPossibleEmergencyAppointment(String patientJmbg, String doctorSpeciality)
        {
            return AppointmentService.FindPossibleEmergencyAppointment(patientJmbg, doctorSpeciality);
        }
        public List<ModifyAppointmentForEmergencyDto> FindAppointmentsToRescheduleForEmergency(String patientJmbg, String doctorSpeciality)
        {
            return AppointmentService.FindAppointmentsToRescheduleForEmergency(patientJmbg, doctorSpeciality);
        }

        public List<PossibleAppointmentsDTO> GetPossibleAppointmentsBySecretary(String patientJmbg, String doctorJmbg, int roomId,
            DateTime dateFrom, DateTime dateUntil, int duration, String priority)
        {
            return AppointmentService.GetPossibleAppointmentsBySecretary(patientJmbg, doctorJmbg, roomId,
                dateFrom, dateUntil, duration, priority);
        }

        public List<PossibleAppointmentsDTO> GetPossibleAppointmentsByManager(int roomId, DateTime dateFrom, DateTime dateUntil, int duration)
        {
            return AppointmentService.GetPossibleAppointmentsByManager(roomId, dateFrom, dateUntil, duration);
        }
        public List<PossibleAppointmentsDTO> GetPossibleAppointmentsByDoctor(String patientJmbg, String doctorJmbg,
            DateTime dateFrom, DateTime dateUntil, int duration, String priority)
        {
            return AppointmentService.GetPossibleAppointmentsByDoctor(patientJmbg, doctorJmbg,
                dateFrom, dateUntil, duration, priority);
        }
        public List<PossibleAppointmentsDTO> GetPossibleAppointmentsForRoomJoin(int firstRoomId, int secondRoomId,
            DateTime dateFrom, DateTime dateUntil, int duration)
        {
            return AppointmentService.GetPossibleAppointmentsForRoomJoin(firstRoomId, secondRoomId, dateFrom, dateUntil, duration);
        }

        public PossibleAppointmentsDTO GetPossibleAppointmentsForAbsence(String doctorJmbg,
            DateTime dateFrom, DateTime dateUntil, int duration)
        {
            return AppointmentService.GetPossibleAppointmentsForAbsence(doctorJmbg, dateFrom, dateUntil, duration);
        }
        public void CreateAppointmentBySecretary(String patientJmbg, String doctorJmbg, int roomId,
            DateTime startTime, int duration)
        {
            AppointmentService.CreateAppointmentBySecretary(patientJmbg, doctorJmbg, roomId,
                startTime, duration);
        }

        public void CreateOperationAppointment(PossibleAppointmentsDTO appointmentToCreate)
        {
            AppointmentService.CreateOperationAppointment(appointmentToCreate);
        }

        public void CreateAppointmentByDoctor(PossibleAppointmentsDTO appointmentToCreate)
        {
            AppointmentService.CreateAppointmentByDoctor(appointmentToCreate);
        }

    }
}