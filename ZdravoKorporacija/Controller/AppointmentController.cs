using Model;
using Service;
using System;
using System.Collections.Generic;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Service;

namespace Controller
{
    public class AppointmentController
    {
        private readonly AppointmentService AppointmentService;
        private readonly ScheduleService ScheduleService;
        private readonly EmergencyService EmergencyService;

        public AppointmentController(AppointmentService appointmentService, ScheduleService scheduleService, EmergencyService emergencyService)
        {
            this.AppointmentService = appointmentService;
            this.ScheduleService = scheduleService;
            this.EmergencyService = emergencyService;
        }

        public List<Appointment> GetAllAppointments()
        {
            return AppointmentService.GetAllAppointments();
        }
        public Appointment GetOneById(int appointmentId)
        {
            return AppointmentService.GetOneById(appointmentId);
        }
        public List<Model.Appointment> GetAppointmentsByPatientJmbg(String patientId)
        {
            return AppointmentService.GetAppointmentsByPatientJmbg(patientId);
        }
        public List<Model.Appointment> GetAppointmentsByDoctorJmbg(String doctorJmbg)
        {
            return AppointmentService.GetAppointmentsByDoctorJmbg(doctorJmbg);
        }
        public List<PossibleAppointmentsDTO> GetPossibleAppointmentsDtos()
        {
            return AppointmentService.GetPossibleAppointmentsDtos();
        }
        public List<PossibleAppointmentsDTO> GetAllFutureAppointmentsByPatient()
        {
            return AppointmentService.GetAllFutureAppointmentsByPatient();
        }

        public List<PossibleAppointmentsDTO> GetAllPastAppointmentsByPatient()
        {
            return AppointmentService.GetAllPastAppointmentsByPatient();
        }
        public List<PossibleAppointmentsDTO> GetAllByJmbgAndDate(DateTime dateTime)
        {
            return AppointmentService.GetAllByJmbgAndDate(dateTime);
        }
        public List<AppointmentDTO> GetAppointmentsByDoctorJmbgDTO(String doctorJmbg)
        {
            return AppointmentService.GetAppointmentsByDoctorJmbgDTO(doctorJmbg);
        }
        public void DeleteAppointment(int appointmentId)
        {
            AppointmentService.DeleteAppointment(appointmentId);
        }

        public void DeleteAppointmentsForOnePatient(String patientJmbg)
        {
            AppointmentService.DeleteAppointmentsForOnePatient(patientJmbg);
        }
        public void DeleteAppointmentByRoomId(int roomId)
        {
            AppointmentService.DeleteAppointmentByRoomId(roomId);
        }
        public void ModifyAppointment(int appointmentId, DateTime newDate)
        {
            AppointmentService.ModifyAppointment(appointmentId, newDate);
        }
        public void CreateAppointmentByPatient(DateTime startTime, String doctorJmbg)
        {
            AppointmentService.CreateAppointmentByPatient(startTime, doctorJmbg);
        }
        public void CreateAppointmentBySecretary(String patientJmbg, String doctorJmbg, int roomId,
            DateTime startTime, int duration)
        {
            AppointmentService.CreateAppointmentBySecretary(patientJmbg, doctorJmbg, roomId,
                startTime, duration);
        }

        public void CreateAppointmentByDoctor(PossibleAppointmentsDTO appointmentToCreate)
        {
            AppointmentService.CreateAppointmentByDoctor(appointmentToCreate);
        }
        public void CreateOperationAppointment(PossibleAppointmentsDTO appointmentToCreate)
        {
            AppointmentService.CreateOperationAppointment(appointmentToCreate);
        }
        public List<PossibleMeetingDTO> GetPossibleMeetingAppointments(List<String> userJmbgs, int roomId,
            DateTime dateFrom, DateTime dateUntil, int duration)
        {
            return ScheduleService.GetPossibleMeetingAppointments(userJmbgs, roomId, dateFrom, dateUntil, duration);
        }
        public PossibleAppointmentsDTO GetPossibleAppointmentsForAbsence(String doctorJmbg,
            DateTime dateFrom, DateTime dateUntil, int duration)
        {
            return ScheduleService.GetPossibleAppointmentsForAbsence(doctorJmbg, dateFrom, dateUntil, duration);
        }
        public List<PossibleAppointmentsDTO> GetPossibleAppointmentsForRoomJoin(int firstRoomId, int secondRoomId,
            DateTime dateFrom, DateTime dateUntil, int duration)
        {
            return ScheduleService.GetPossibleAppointmentsForRoomJoin(firstRoomId, secondRoomId, dateFrom, dateUntil, duration);
        }
        public List<PossibleAppointmentsDTO> GetPossibleAppointmentsByDoctor(String patientJmbg, String doctorJmbg,
            DateTime dateFrom, DateTime dateUntil, int duration, String priority)
        {
            return ScheduleService.GetPossibleAppointmentsByDoctor(patientJmbg, doctorJmbg,
                dateFrom, dateUntil, duration, priority);
        }
        public List<PossibleAppointmentsDTO> GetPossibleAppointmentsByManager(int roomId, DateTime dateFrom, DateTime dateUntil, int duration)
        {
            return ScheduleService.GetPossibleAppointmentsByManager(roomId, dateFrom, dateUntil, duration);
        }
        public List<PossibleAppointmentsDTO> GetPossibleAppointmentsBySecretary(String patientJmbg, String doctorJmbg, int roomId,
            DateTime dateFrom, DateTime dateUntil, int duration, String priority)
        {
            return ScheduleService.GetPossibleAppointmentsBySecretary(patientJmbg, doctorJmbg, roomId,
                dateFrom, dateUntil, duration, priority);
        }
        public PossibleAppointmentsDTO FindPossibleEmergencyAppointment(String patientJmbg, String doctorSpeciality)
        {
            return EmergencyService.FindPossibleEmergencyAppointment(patientJmbg, doctorSpeciality);
        }
        public List<ModifyAppointmentForEmergencyDto> FindAppointmentsToRescheduleForEmergency(String patientJmbg, String doctorSpeciality)
        {
            return EmergencyService.FindAppointmentsToRescheduleForEmergency(patientJmbg, doctorSpeciality);
        }

        
    }
}