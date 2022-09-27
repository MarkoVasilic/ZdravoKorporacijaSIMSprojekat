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
        private readonly AppointmentService _appointmentService;
        private readonly ScheduleService _scheduleService;
        private readonly EmergencyService _emergencyService;

        public AppointmentController(AppointmentService appointmentService, ScheduleService scheduleService, EmergencyService emergencyService)
        {
            this._appointmentService = appointmentService;
            this._scheduleService = scheduleService;
            this._emergencyService = emergencyService;
        }

        public List<Appointment> GetAllAppointments()
        {
            return _appointmentService.GetAllAppointments();
        }
        public Appointment GetOneById(int appointmentId)
        {
            return _appointmentService.GetOneById(appointmentId);
        }
        public List<Model.Appointment> GetAppointmentsByPatientJmbg(String patientId)
        {
            return _appointmentService.GetAppointmentsByPatientJmbg(patientId);
        }
        public List<Model.Appointment> GetAppointmentsByDoctorJmbg(String doctorJmbg)
        {
            return _appointmentService.GetAppointmentsByDoctorJmbg(doctorJmbg);
        }
        public List<PossibleAppointmentsDTO> GetPossibleAppointmentsDtos()
        {
            return _appointmentService.GetPossibleAppointmentsDtos();
        }
        public List<PossibleAppointmentsDTO> GetAllFutureAppointmentsByPatient()
        {
            return _appointmentService.GetAllFutureAppointmentsByPatient();
        }

        public List<PossibleAppointmentsDTO> GetAllPastAppointmentsByPatient()
        {
            return _appointmentService.GetAllPastAppointmentsByPatient();
        }
        public List<PossibleAppointmentsDTO> GetAllByJmbgAndDate(DateTime dateTime)
        {
            return _appointmentService.GetAllByJmbgAndDate(dateTime);
        }
        public List<AppointmentDTO> GetAppointmentsByDoctorJmbgDTO(String doctorJmbg)
        {
            return _appointmentService.GetAppointmentsByDoctorJmbgDTO(doctorJmbg);
        }
        public void DeleteAppointment(int appointmentId)
        {
            _appointmentService.DeleteAppointment(appointmentId);
        }

        public void DeleteAppointmentsForOnePatient(String patientJmbg)
        {
            _appointmentService.DeleteAppointmentsForOnePatient(patientJmbg);
        }
        public void DeleteAppointmentByRoomId(int roomId)
        {
            _appointmentService.DeleteAppointmentByRoomId(roomId);
        }
        public void ModifyAppointment(int appointmentId, DateTime newDate)
        {
            _appointmentService.ModifyAppointment(appointmentId, newDate);
        }
        public void CreateAppointmentByPatient(DateTime startTime, String doctorJmbg)
        {
            _appointmentService.CreateAppointmentByPatient(startTime, doctorJmbg);
        }
        public void CreateAppointmentBySecretary(String patientJmbg, String doctorJmbg, int roomId,
            DateTime startTime, int duration)
        {
            _appointmentService.CreateAppointmentBySecretary(patientJmbg, doctorJmbg, roomId,
                startTime, duration);
        }

        public void CreateAppointmentByDoctor(PossibleAppointmentsDTO appointmentToCreate)
        {
            _appointmentService.CreateAppointmentByDoctor(appointmentToCreate);
        }

        public void CreateOperationAppointment(PossibleAppointmentsDTO operationToCreate)
        {
            _appointmentService.CreateOperationAppointment(operationToCreate);
        }
        public List<PossibleAppointmentsDTO> GetPossibleAppointmentsByDoctor(String patientJmbg, String doctorJmbg,
            DateTime dateFrom, DateTime dateUntil, int duration, String priority, int roomId)
        {
            return _scheduleService.GetPossibleAppointmentsByDoctor(patientJmbg, doctorJmbg,
                dateFrom, dateUntil, duration, priority, roomId);
        }
        public List<PossibleAppointmentsDTO> GetPossibleAppointmentsByManager(int roomId, DateTime dateFrom, DateTime dateUntil, int duration)
        {
            return _scheduleService.GetPossibleAppointmentsByManager(roomId, dateFrom, dateUntil, duration);
        }
        public List<PossibleAppointmentsDTO> GetPossibleAppointmentsBySecretary(String patientJmbg, String doctorJmbg, int roomId,
            DateTime dateFrom, DateTime dateUntil, int duration, String priority)
        {
            return _scheduleService.GetPossibleAppointmentsBySecretary(patientJmbg, doctorJmbg, roomId,
                dateFrom, dateUntil, duration, priority);
        }
        public PossibleAppointmentsDTO FindPossibleEmergencyAppointment(String patientJmbg, String doctorSpeciality)
        {
            return _emergencyService.FindPossibleEmergencyAppointment(patientJmbg, doctorSpeciality);
        }
        public List<ModifyAppointmentForEmergencyDto> FindAppointmentsToRescheduleForEmergency(String patientJmbg, String doctorSpeciality)
        {
            return _emergencyService.FindAppointmentsToRescheduleForEmergency(patientJmbg, doctorSpeciality);
        }

        public List<AppointmentDTO> FilterByTime(DateTime dateFrom, DateTime dateTo)
        {
            return _appointmentService.FilterByTime(dateFrom, dateTo);
        }


    }
}