using Model;
using Repository;
using System;
using System.Collections.Generic;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Interfaces;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;

namespace ZdravoKorporacija.Service
{
    public class EmergencyService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IBasicRenovationRepository _basicRenovationRepository;
        private readonly IAdvancedRenovationJoiningRepository _advancedRenovationJoiningRepository;
        private readonly IAdvancedRenovationSeparationRepository _advancedRenovationSeparationRepository;
        private readonly ScheduleService _scheduleService;

        public EmergencyService(IAppointmentRepository appointmentRepository, IPatientRepository patientRepository,
            IDoctorRepository doctorRepository, IRoomRepository roomRepository,
            IBasicRenovationRepository basicRenovationRepository,
            IAdvancedRenovationJoiningRepository advancedRenovationJoining,
            IAdvancedRenovationSeparationRepository advancedRenovationSeparation, ScheduleService scheduleService)
        {
            this._appointmentRepository = appointmentRepository;
            this._patientRepository = patientRepository;
            this._doctorRepository = doctorRepository;
            this._roomRepository = roomRepository;
            this._basicRenovationRepository = basicRenovationRepository;
            this._advancedRenovationJoiningRepository = advancedRenovationJoining;
            this._advancedRenovationSeparationRepository = advancedRenovationSeparation;
            this._scheduleService = scheduleService;
        }
        public PossibleAppointmentsDTO FindPossibleEmergencyAppointment(String patientJmbg, String doctorSpeciality)
        {
            ValidateParametersForScheduleEmergency(patientJmbg, doctorSpeciality);
            Patient patient = _patientRepository.FindOneByJmbg(patientJmbg);
            List<Doctor> doctors = _doctorRepository.FindAllBySpeciality(doctorSpeciality);
            foreach (var doctor in doctors)
            {
                PossibleAppointmentsDTO possibleEmergencyAppointment = GetPossibleEmergencyAppointmentForOneDoctor(patient, doctor);
                if (possibleEmergencyAppointment != null)
                    return possibleEmergencyAppointment;
            }
            return null;
        }

        private PossibleAppointmentsDTO GetPossibleEmergencyAppointmentForOneDoctor(Patient patient, Doctor doctor)
        {
            List<Appointment> doctorAppointments = _appointmentRepository.FindAllByDoctorJmbg(doctor.Jmbg);
            if (!IsOccupied(doctorAppointments))
            {
                List<Room> rooms = _roomRepository.FindAll();
                foreach (var room in rooms)
                {
                    List<Appointment> roomAppointments = _appointmentRepository.FindAllByRoomId(room.Id);
                    if (!IsOccupied(roomAppointments) && !IsRoomOccupiedByBasicRenovation(room) &&
                        !IsRoomOccupiedByRenovationJoin(room) && !IsRoomOccupiedByRenovationSeparation(room))
                    {
                        return new PossibleAppointmentsDTO(patient.Jmbg, patient.FirstName + " " + patient.LastName, doctor.Jmbg,
                            doctor.FirstName + " " + doctor.LastName, doctor.SpecialtyType, room.Id, room.Name, DateTime.Now.AddMinutes(5), 60, 0);
                    }
                }
            }
            return null;
        }

        private Boolean IsOccupied(List<Appointment> appointmentList)
        {
            foreach (var appointment in appointmentList)
            {
                if (ValidateAppointmentTimeForScheduleEmergency(appointment.StartTime, appointment.Duration))
                {
                    return true;
                }
            }
            return false;
        }

        private Boolean IsRoomOccupiedByBasicRenovation(Room room)
        {
            List<BasicRenovation> roomBasicRenovations = _basicRenovationRepository.FindAllByRoomId(room.Id);
            foreach (var roomBasicRenovation in roomBasicRenovations)
            {
                if (ValidateAppointmentTimeForScheduleEmergency(roomBasicRenovation.StartTime, roomBasicRenovation.Duration))
                {
                    return true;
                }
            }
            return false;
        }
        private Boolean IsRoomOccupiedByRenovationJoin(Room room)
        {
            List<AdvancedRenovationJoining> roomJoinRenovations = _advancedRenovationJoiningRepository.FindAllByRoomId(room.Id);
            foreach (var advancedRenovationJoining in roomJoinRenovations)
            {
                if (ValidateAppointmentTimeForScheduleEmergency(advancedRenovationJoining.StartTime, advancedRenovationJoining.Duration))
                {
                    return true;
                }
            }
            return false;
        }
        private Boolean IsRoomOccupiedByRenovationSeparation(Room room)
        {
            List<AdvancedRenovationSeparation> roomJoinSeparations = _advancedRenovationSeparationRepository.FindAllByRoomId(room.Id);
            foreach (var advancedRenovationSeparation in roomJoinSeparations)
            {
                if (ValidateAppointmentTimeForScheduleEmergency(advancedRenovationSeparation.StartTime, advancedRenovationSeparation.Duration))
                {
                    return true;
                }
            }
            return false;
        }
        private Boolean ValidateAppointmentTimeForScheduleEmergency(DateTime startTime, int duration)
        {
            if ((startTime > DateTime.Now && startTime < DateTime.Now.AddMinutes(75)) ||
                (startTime.AddMinutes(duration) > DateTime.Now &&
                 startTime.AddMinutes(duration) < DateTime.Now.AddMinutes(75)) ||
                (startTime < DateTime.Now && startTime.AddMinutes(duration) > DateTime.Now.AddMinutes(75)))
                return true;
            return false;
        }

        public List<ModifyAppointmentForEmergencyDto> FindAppointmentsToRescheduleForEmergency(String patientJmbg, String doctorSpeciality)
        {
            ValidateParametersForScheduleEmergency(patientJmbg, doctorSpeciality);
            Patient patient = _patientRepository.FindOneByJmbg(patientJmbg);
            List<Doctor> doctors = _doctorRepository.FindAllBySpeciality(doctorSpeciality);
            List<ModifyAppointmentForEmergencyDto> appointmentsToReschedule = new List<ModifyAppointmentForEmergencyDto>();
            foreach (var doctor in doctors)
            {
                appointmentsToReschedule.AddRange(GetPossibleAppointmentsToReschedule(doctor, patientJmbg));
            }
            if (appointmentsToReschedule != null)
                appointmentsToReschedule.Sort((x, y) => DateTime.Compare(x.NewStartTime, y.NewStartTime));
            return appointmentsToReschedule;
        }

        private void ValidateParametersForScheduleEmergency(String patientJmbg, String doctorSpeciality)
        {
            if (patientJmbg == null || _patientRepository.FindOneByJmbg(patientJmbg) == null)
                throw new Exception("Patient with that JMBG doesn't exist!");
            else if (doctorSpeciality == null || _doctorRepository.FindAllBySpeciality(doctorSpeciality) == null)
                throw new Exception("There are no doctors with that speciality!");
            else
                return;
        }

        private List<ModifyAppointmentForEmergencyDto> GetPossibleAppointmentsToReschedule(Doctor doctor, String patientJmbg)
        {
            List<Appointment> doctorAppointments = _appointmentRepository.FindAllByDoctorJmbg(doctor.Jmbg);
            List<ModifyAppointmentForEmergencyDto> appointmentsToReschedule = new List<ModifyAppointmentForEmergencyDto>();
            foreach (var appointment in doctorAppointments)
            {
                if (CheckAppointmentTimeForReschedule(appointment.StartTime))
                {
                    Room room = _roomRepository.FindOneById(appointment.RoomId);
                    Patient patientInOldAppointment = _patientRepository.FindOneByJmbg(appointment.PatientJmbg);
                    List<PossibleAppointmentsDTO> newPossibleAppointments = _scheduleService.GetPossibleAppointmentsBySecretary(patientJmbg, doctor.Jmbg,
                        room.Id, DateTime.Now.AddHours(3), DateTime.Now.AddDays(5), appointment.Duration, "doctor");
                    newPossibleAppointments.Sort((x, y) => DateTime.Compare(x.StartTime, y.StartTime));
                    appointmentsToReschedule.Add(new ModifyAppointmentForEmergencyDto(appointment.PatientJmbg, patientInOldAppointment.FirstName + " " + patientInOldAppointment.LastName, doctor.Jmbg,
                    doctor.FirstName + " " + doctor.LastName, doctor.SpecialtyType, room.Id, room.Name, appointment.StartTime,
                    newPossibleAppointments[0].StartTime, appointment.Duration, appointment.Id));
                    break;
                }
            }
            return appointmentsToReschedule;
        }

        private Boolean CheckAppointmentTimeForReschedule(DateTime appointmentStartTime)
        {
            if (appointmentStartTime > DateTime.Now && appointmentStartTime < DateTime.Now.AddMinutes(75))
            {
                return true;
            }

            return false;
        }
    }
}
