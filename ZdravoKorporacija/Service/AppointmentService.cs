using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ZdravoKorporacija;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;

namespace Service
{
    public class AppointmentService
    {
        readonly AppointmentRepository AppointmentRepository = new AppointmentRepository();
        readonly PatientRepository PatientRepository = new PatientRepository();
        readonly DoctorRepository DoctorRepository = new DoctorRepository();
        readonly RoomRepository RoomRepository = new RoomRepository();
        readonly BasicRenovationRepository BasicRenovationRepository = new BasicRenovationRepository();
        public AppointmentService(AppointmentRepository appointmentRepository, PatientRepository patientRepository,
            DoctorRepository doctorRepository, RoomRepository roomRepository, BasicRenovationRepository basicRenovationRepository)
        {
            this.AppointmentRepository = appointmentRepository;
            this.PatientRepository = patientRepository;
            this.DoctorRepository = doctorRepository;
            this.RoomRepository = roomRepository;
            this.BasicRenovationRepository = basicRenovationRepository;
        }
        public AppointmentService() { }

        public List<Appointment> GetAllAppointments()
        {
            return AppointmentRepository.FindAll();
        }

        public List<PossibleAppointmentsDTO> GetAllAppointmentsBySecretary()
        {
            List<Appointment> appointments = AppointmentRepository.FindAll();
            List<PossibleAppointmentsDTO> possibleAppointmentsDTO = new List<PossibleAppointmentsDTO>();
            foreach (var ap in appointments)
            {
                Doctor doctor = DoctorRepository.FindOneByJmbg(ap.DoctorJmbg);
                Patient patient = PatientRepository.FindOneByJmbg(ap.PatientJmbg);
                Room room = RoomRepository.FindOneById(ap.RoomId);
                possibleAppointmentsDTO.Add(new PossibleAppointmentsDTO(ap.PatientJmbg, patient.FirstName + " " + patient.LastName,
                    ap.DoctorJmbg, doctor.FirstName + " " + doctor.LastName, doctor.SpecialtyType, ap.RoomId, room.Name, ap.StartTime, ap.Duration, ap.Id));
            }
            return possibleAppointmentsDTO;
        }


        public List<PossibleAppointmentsDTO> GetAllFutureAppointmentsByPatient()
        {
            List<Appointment> appointments = AppointmentRepository.GetAllFutureByPatient(App.loggedUser.Jmbg);
            List<PossibleAppointmentsDTO> possibleAppointmentsDTO = new List<PossibleAppointmentsDTO>();
            foreach (var ap in appointments)
            {
                Doctor? doctor = DoctorRepository.FindOneByJmbg(ap.DoctorJmbg);
                Patient? patient = PatientRepository.FindOneByJmbg(ap.PatientJmbg);
                Room? room = RoomRepository.FindOneById(ap.RoomId);
                possibleAppointmentsDTO.Add(new PossibleAppointmentsDTO(ap.PatientJmbg, patient.FirstName + " " + patient.LastName,
                    ap.DoctorJmbg, doctor.FirstName + " " + doctor.LastName, doctor.SpecialtyType, ap.RoomId, room.Name, ap.StartTime, ap.Duration,ap.Id));
            }
            return possibleAppointmentsDTO;
        }

        private int GenerateNewId()
        {
            try
            {
                List<Appointment> appointments = AppointmentRepository.FindAll();
                int currentMax = appointments.Max(obj => obj.Id);
                return currentMax + 1;
            }
            catch
            {
                return 1;
            }
        }
        public void DeleteAppointment(int appointmentId)
        {
            if (AppointmentRepository.FindOneById(appointmentId) == null)
            {
                throw new Exception("Appointment with that id doesn't exist!");
            }
            else
            {
                AppointmentRepository.RemoveAppointment(appointmentId);
            }
        }

        public void ModifyAppointment(int appointmentId, DateTime newDate)
        {

            var oneAppointment = AppointmentRepository.FindOneById(appointmentId);
            if (oneAppointment == null)
            {
                throw new Exception("Appointment with that id doesn't exist!");
            }
            else
            {
                Appointment newAppointment = new Appointment(newDate, oneAppointment.Duration, oneAppointment.Id, oneAppointment.PatientJmbg,
                    oneAppointment.DoctorJmbg, oneAppointment.RoomId);
                if (!newAppointment.validateAppointment())
                {
                    throw new Exception("Something went wrong, new appointment isn't modified!");
                }
                AppointmentRepository.UpdateAppointment(newAppointment);
            }

        }

        public Model.Appointment? GetOneById(int appointmentId)
        {
            return AppointmentRepository.FindOneById(appointmentId);
        }


        public List<Appointment> GetAppointmentsByDoctorJmbg(String doctorJmbg)
        {
            return AppointmentRepository.FindAllByDoctorJmbg(doctorJmbg);
        }

        public List<Appointment> GetAppointmentsByPatientJmbg(String patientJmbg)
        {
            return AppointmentRepository.FindAllByPatientJmbg(patientJmbg);
        }

        public String CreateAppointmentByPatient(DateTime date, String doctorJmbg)
        {
            if (DoctorRepository.FindOneByJmbg(doctorJmbg) == null)
            {
                return "Doctor with that JMBG doesn't exist!";
            }
            int id = GenerateNewId();
            Appointment appointment = new Appointment(date, 15, id, App.loggedUser.Jmbg, doctorJmbg, 11);
            AppointmentRepository.SaveAppointment(appointment);
            if (!appointment.validateAppointment())
            {
                return "Something went wrong, new appointment isn't created!";
            }
            else
            {
                return "";
            }

        }

        public String CreateAppointmentByDoctor(DateTime startTime, int duration, String patientJmbg)
        {
            if (PatientRepository.FindOneByJmbg(patientJmbg) == null)
            {
                return "Patient with that JMBG doesn't exist!";
            }
            int id = GenerateNewId();
            Appointment appointment = new Appointment(startTime, duration, id, patientJmbg, "4444444444444", 11);
            if (!appointment.validateAppointment())
            {
                return "Something went wrong, new appointment isn't created!";
            }
            else
            {
                AppointmentRepository.SaveAppointment(appointment);
                return "";
            }

        }

        public void CreateAppointmentBySecretary(String patientJmbg, String doctorJmbg, int roomId,
            DateTime startTime, int duration)
        {
            if (PatientRepository.FindOneByJmbg(patientJmbg) == null)
                throw new Exception("Patient with that JMBG doesn't exist!");
            else if (DoctorRepository.FindOneByJmbg(doctorJmbg) == null)
                throw new Exception("Doctor with that JMBG doesn't exist!");
            else if (RoomRepository.FindOneById(roomId) == null)
                throw new Exception("Room with that id doesn't exist!");
            int id = GenerateNewId();
            Appointment appointment = new Appointment(startTime, duration, id, patientJmbg, doctorJmbg, roomId);
            if (!appointment.validateAppointment())
            {
                throw new Exception("Something went wrong, new appointment isn't created!");
            }
            AppointmentRepository.SaveAppointment(appointment);
        }

        public List<PossibleAppointmentsDTO> GetPossibleAppointmentsByManager(int roomId,
            DateTime dateFrom, DateTime dateUntil, int duration)
        {
            if (RoomRepository.FindOneById(roomId) == null)
                throw new Exception("Room with that id doesn't exist!");
            else if (dateFrom > dateUntil)
                throw new Exception("Dates are not valid!");
            List<DateTime> possibleAppointments = new List<DateTime>();
            possibleAppointments = findPossibleStartTimesOfAppointment("", "", roomId,
                    dateFrom, dateUntil, duration);
            if (possibleAppointments.Count == 0)
                throw new Exception("There are not free appointments for given parameters!");
            List<PossibleAppointmentsDTO> retValue = new List<PossibleAppointmentsDTO>();
            Room selectedRoom = RoomRepository.FindOneById(roomId);
            foreach (var pa in possibleAppointments)
            {
                if (pa > DateTime.Now.AddHours(1))
                {
                    PossibleAppointmentsDTO possibleAppointmentsDTO = new PossibleAppointmentsDTO("", "", "", "", "", roomId, selectedRoom.Name, pa, duration, -1);
                    retValue.Add(possibleAppointmentsDTO);
                }
            }
            return retValue;
        }

        public List<PossibleAppointmentsDTO> GetPossibleAppointmentsBySecretary(String patientJmbg, String doctorJmbg, int roomId,
            DateTime dateFrom, DateTime dateUntil, int duration, String priority)
        {
            if (patientJmbg == null || PatientRepository.FindOneByJmbg(patientJmbg) == null)
                throw new Exception("Patient with that JMBG doesn't exist!");
            else if (doctorJmbg == null || DoctorRepository.FindOneByJmbg(doctorJmbg) == null)
                throw new Exception("Doctor with that JMBG doesn't exist!");
            else if (RoomRepository.FindOneById(roomId) == null)
                throw new Exception("Room with that id doesn't exist!");
            else if (dateFrom > dateUntil)                 
                throw new Exception("Dates are not valid!");
            List<DateTime> possibleAppointments = new List<DateTime>();
            Doctor sentDoctor = DoctorRepository.FindOneByJmbg(doctorJmbg);
            List<Doctor> doctorsNeeded = DoctorRepository.FindAllBySpeciality(sentDoctor.SpecialtyType);
            doctorsNeeded.Remove(sentDoctor);
            if (priority == "doctor")
            {
                possibleAppointments = findPossibleStartTimesOfAppointment(patientJmbg, doctorJmbg, roomId,
                    dateFrom, dateUntil, duration);
                while (possibleAppointments.Count == 0)
                {
                    dateFrom = dateUntil;
                    dateUntil = dateUntil.AddDays(5);
                    possibleAppointments = findPossibleStartTimesOfAppointment(patientJmbg, doctorJmbg, roomId,
                    dateFrom, dateUntil, duration);
                }
            }
            else
            {
                possibleAppointments = findPossibleStartTimesOfAppointment(patientJmbg, doctorJmbg, roomId,
                    dateFrom, dateUntil, duration);
                while (possibleAppointments.Count == 0)
                {
                    foreach (var doc in doctorsNeeded)
                    {
                        doctorJmbg = doc.Jmbg;
                        if (roomId == -1)
                            roomId = doc.RoomId;
                        possibleAppointments = findPossibleStartTimesOfAppointment(patientJmbg, doctorJmbg, roomId,
                        dateFrom, dateUntil, duration);
                        if (possibleAppointments.Count != 0)
                            break;
                    }
                    if (possibleAppointments.Count == 0)
                        throw new Exception("There are not free appointments for given parameters!");
                }
            }
            List<PossibleAppointmentsDTO> retValue = new List<PossibleAppointmentsDTO>();
            Patient selectedPatient = PatientRepository.FindOneByJmbg(patientJmbg);
            Doctor selectedDoctor = DoctorRepository.FindOneByJmbg(doctorJmbg);
            Room selectedRoom = RoomRepository.FindOneById(roomId);
            foreach (var pa in possibleAppointments)
            {
                if (pa > DateTime.Now.AddHours(1))
                {
                    PossibleAppointmentsDTO possibleAppointmentsDTO = new PossibleAppointmentsDTO(patientJmbg, selectedPatient.FirstName + " " + selectedPatient.LastName, doctorJmbg,
                       selectedDoctor.FirstName + " " + selectedDoctor.LastName, sentDoctor.SpecialtyType, roomId, selectedRoom.Name, pa, duration, -1);
                    retValue.Add(possibleAppointmentsDTO);
                }
            }
            return retValue;
        }

        public List<DateTime> findPossibleStartTimesOfAppointment(String patientJmbg, String doctorJmbg, int roomId, DateTime dateFrom,
            DateTime dateUntil, int duration)
        {
            List<Appointment> neededAppointments = findAppointmentsToWatch(patientJmbg, doctorJmbg, roomId, dateFrom, dateUntil);
            List<(DateTime, DateTime)> freePeriod = new List<(DateTime, DateTime)>();
            List<(DateTime, DateTime)> newFreePeriod = new List<(DateTime, DateTime)>();
            List<DateTime> possibleAppointments = new List<DateTime>();
            if (neededAppointments.Count == 0)
            {
                var firstDate = dateFrom;
                while (firstDate < dateUntil && firstDate < dateFrom.AddDays(5))
                {
                    freePeriod.Add((firstDate.AddHours(8).AddMinutes(15), firstDate.AddDays(1).AddMinutes(-15)));
                    firstDate = firstDate.AddDays(1);
                }

            }
            else
                freePeriod = findFreePeriods(neededAppointments, dateFrom, dateUntil, duration);

            for (int i = 0; i < freePeriod.Count; i++)
            {
                if (freePeriod[i].Item1.AddHours(9) < freePeriod[i].Item2)
                {
                    newFreePeriod.AddRange(splitPeriod(freePeriod[i]));
                }
                else
                    newFreePeriod.Add(freePeriod[i]);

            }
            if (freePeriod.Count > 0)
            {
                possibleAppointments = findFreeAppointments(newFreePeriod, duration);
            }
            return possibleAppointments;
        }

        private List<Appointment> findAppointmentsToWatch(String patientJmbg, String doctorJmbg, int? roomId, DateTime dateFrom,
            DateTime dateUntil)
        {
            List<Appointment> allAppointments = this.GetAllAppointments();
            List<BasicRenovation> allBasicRenovations = BasicRenovationRepository.FindAll();
            List <Appointment> neededAppointments = new List<Appointment>();
            foreach (var app in allAppointments)
            {
                if (app.StartTime.AddMinutes(app.Duration) > dateFrom && app.StartTime < dateUntil && (
                    (doctorJmbg != null && app.DoctorJmbg == doctorJmbg) ||
                    (patientJmbg != null && app.PatientJmbg == patientJmbg) || (roomId != null && app.RoomId == roomId)))
                    neededAppointments.Add(app);
            }
            foreach (var br in allBasicRenovations)
            {
                if (br.StartTime.AddMinutes(br.Duration) > dateFrom && br.StartTime < dateUntil && ( (roomId != null && br.RoomId == roomId)))
                {
                    Appointment basicRenovationAppointment = new Appointment(br.StartTime, br.Duration, -1, "", "", br.RoomId);
                    neededAppointments.Add(basicRenovationAppointment);
                }
            }
            neededAppointments.Sort((x, y) => DateTime.Compare(x.StartTime.AddMinutes(x.Duration), y.StartTime.AddMinutes(y.Duration)));
            return neededAppointments;
        }

        private List<(DateTime, DateTime)> findFreePeriods(List<Appointment> neededAppointments, DateTime dateFrom, DateTime dateUntil, int duration)
        {
            List<(DateTime, DateTime)> freePeriod = new List<(DateTime, DateTime)>();
            if (neededAppointments[0].StartTime > dateFrom.AddMinutes(duration + 15))
            {
                freePeriod.Add((dateFrom.AddMinutes(15), neededAppointments[0].StartTime.AddMinutes(-15)));
            }
            for (int i = 0; i < neededAppointments.Count - 1; i++)
            {
                if (neededAppointments[i].StartTime.AddMinutes(neededAppointments[i].Duration + 15) < neededAppointments[i + 1].StartTime)
                {
                    (DateTime, DateTime) newPeriod = (neededAppointments[i].StartTime.AddMinutes(neededAppointments[i].Duration + 15),
                        neededAppointments[i + 1].StartTime.AddMinutes(-15));
                    freePeriod.Add(newPeriod);
                }

            }
            if (neededAppointments.Last().StartTime.AddMinutes(neededAppointments.Last().Duration) < dateUntil.AddMinutes(-duration - 15))
            {
                freePeriod.Add((neededAppointments.Last().StartTime.AddMinutes(neededAppointments.Last().Duration + 15), dateUntil.AddMinutes(-15)));
            }
            return freePeriod;
        }

        private List<(DateTime, DateTime)> splitPeriod((DateTime, DateTime) period)
        {
            List<(DateTime, DateTime)> retValue = new List<(DateTime, DateTime)>();
            while (period.Item1.AddHours(9) < period.Item2)
            {
                retValue.Add((period.Item1, period.Item1.AddHours(8)));
                period = (period.Item1.AddHours(8).AddMinutes(15), period.Item2);
            }
            retValue.Add((period.Item1, period.Item2));
            return retValue;
        }

        private List<DateTime> findFreeAppointments(List<(DateTime, DateTime)> newFreePeriod, int duration)
        {
            List<DateTime> possibleAppointments = new List<DateTime>();
            int counter = 0;
            while (counter < 10)
            {
                Boolean change = false;
                for (int i = 0; i < newFreePeriod.Count; i++)
                {
                    if (counter == 10)
                        break;
                    if (newFreePeriod[i].Item1.AddMinutes(duration) < newFreePeriod[i].Item2)
                    {
                        possibleAppointments.Add(newFreePeriod[i].Item1);
                        newFreePeriod[i] = (newFreePeriod[i].Item1.AddMinutes(duration + 15), newFreePeriod[i].Item2);
                        counter++;
                        change = true;
                    }
                }
                if (!change)
                    break;
            }
            possibleAppointments.Sort((x, y) => DateTime.Compare(x, y));
            return possibleAppointments;
        }
    }
}