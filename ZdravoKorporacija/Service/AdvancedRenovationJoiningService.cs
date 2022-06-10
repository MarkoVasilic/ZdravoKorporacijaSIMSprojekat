using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using Repository;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Interfaces;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;

namespace ZdravoKorporacija.Service
{
    public class AdvancedRenovationJoiningService
    {

        private readonly IAdvancedRenovationJoiningRepository _advancedRenovationJoiningRepository;
        private readonly RoomService _roomService;
        private readonly AppointmentService _appointmentService;
        private readonly BasicRenovationService _basicRenovationService;
        private readonly EquipmentService _equipmentService;
        private readonly ScheduleService _scheduleService;
        private readonly DisplacementService _displacementService;

        public AdvancedRenovationJoiningService(IAdvancedRenovationJoiningRepository advancedRenovationJoiningRepository, RoomService roomService, AppointmentService appointmentService, BasicRenovationService basicRenovationService, EquipmentService equipmentService, ScheduleService scheduleService, DisplacementService displacementService)
        {
            _advancedRenovationJoiningRepository = advancedRenovationJoiningRepository;
            _roomService = roomService;
            _appointmentService = appointmentService;
            _basicRenovationService = basicRenovationService;
            _equipmentService = equipmentService;
            _scheduleService = scheduleService;
            _displacementService = displacementService;
        }
        public List<PossibleAppointmentsDTO> GetPossibleAppointments(int firstRoomId, int secondRoomId,
            DateTime dateFrom, DateTime dateUntil, int duration)
        {
            List<String> doctorJmbgs = new List<String>();
            doctorJmbgs.Add("");
            ValidateInputParameters(firstRoomId, secondRoomId, dateFrom, dateUntil);
            List<DateTime> possibleAppointmentsForFirstRoom = _scheduleService.FindPossibleStartTimesOfAppointment("", doctorJmbgs, firstRoomId,
                dateFrom, dateUntil, duration);
            List<DateTime> possibleAppointmentsForSecondRoom = _scheduleService.FindPossibleStartTimesOfAppointment("", doctorJmbgs, secondRoomId,
                dateFrom, dateUntil, duration);
            if (possibleAppointmentsForFirstRoom.Count == 0 || possibleAppointmentsForSecondRoom.Count == 0)
                throw new Exception("There are not free appointments for given parameters!");
            return CreatePossibleAppointmentsDtos(duration, possibleAppointmentsForFirstRoom, possibleAppointmentsForSecondRoom);
        }

        private static List<PossibleAppointmentsDTO> CreatePossibleAppointmentsDtos(int duration, List<DateTime> possibleAppointmentsForFirstRoom,
            List<DateTime> possibleAppointmentsForSecondRoom)
        {
            List<PossibleAppointmentsDTO> retValue = new List<PossibleAppointmentsDTO>();
            foreach (var firstRoom in possibleAppointmentsForFirstRoom)
            {
                foreach (var secondRoom in possibleAppointmentsForSecondRoom)
                {
                    if (firstRoom == secondRoom && firstRoom > DateTime.Now.AddHours(1))
                    {
                        PossibleAppointmentsDTO possibleAppointmentsDTO =
                            new PossibleAppointmentsDTO("", "", "", "", "", -1, "", firstRoom, duration, -1);
                        retValue.Add(possibleAppointmentsDTO);
                    }
                }
            }

            return retValue;
        }

        private void ValidateInputParameters(int firstRoomId, int secondRoomId, DateTime dateFrom,
            DateTime dateUntil)
        {
            if (_roomService.GetRoomById(firstRoomId) == null || _roomService.GetRoomById(secondRoomId) == null)
                throw new Exception("One of the rooms doesn't exist!");
            else if (dateFrom > dateUntil)
                throw new Exception("Dates are not valid!");
        }

        public void Create(int firstStartRoom, int secondStartroom, DateTime startTime, int duration, String resultRoomName, String resultRoomDescription, RoomType resultRoomType)
        {
            int advancedRenovationId = GenerateNewId();

            AdvancedRenovationJoining advancedRenovationJoining = new AdvancedRenovationJoining(advancedRenovationId, firstStartRoom, secondStartroom, startTime, duration, resultRoomName, resultRoomDescription, resultRoomType);
            if (!advancedRenovationJoining.validate())
            {
                throw new Exception("Something went wrong, renovation isn't saved");
            }

            _advancedRenovationJoiningRepository.SaveJoining(advancedRenovationJoining);
        }



        public void JoinRooms()
        {
            List<AdvancedRenovationJoining> advancedRenovations = GetAll();
            List<int> advancedRenovationIds = new List<int>();

            foreach (AdvancedRenovationJoining advancedRenovation in advancedRenovations)
            {
                PerformJoining(advancedRenovation, advancedRenovationIds);
            }

            for (int i = 0; i < advancedRenovationIds.Count; i++)
            {
                _advancedRenovationJoiningRepository.RemoveJoining(advancedRenovationIds[i]);
            }

        }

        public void PerformJoining(AdvancedRenovationJoining advancedRenovation, List<int> renovationIds)
        {

            Room oldRoom = _roomService.GetRoomById(advancedRenovation.FirstStartRoom);

            if (advancedRenovation.StartTime <= DateTime.Today)
            {
                if (oldRoom != null)
                {
                    ChangeRoomInformation(advancedRenovation, oldRoom);
                    _roomService.ModifyRoomForRenovation(oldRoom);
                    DeleteEverythingForRoom(advancedRenovation.SecondStartRoom);
                    renovationIds.Add(advancedRenovation.Id);

                }
                else
                {
                    throw new Exception("Room with that identification number doesn't exist");
                }
            }
        }


        public void ChangeRoomInformation(AdvancedRenovationJoining advancedRenovationJoining, Room room)
        {
            room.Name = advancedRenovationJoining.ResultRoomName;
            room.Description = advancedRenovationJoining.ResultRoomDescription;
            room.Type = advancedRenovationJoining.ResultRoomType;

        }

        public void DeleteEverythingForRoom(int roomId)
        {
            _appointmentService.DeleteAppointmentByRoomId(roomId);
            _basicRenovationService.DeleteByRoomId(roomId);
            _equipmentService.DeleteByRoomId(roomId);
            _displacementService.DeleteByStartRoomId(roomId);
            _displacementService.DeleteByEndRoomId(roomId);
            _roomService.DeleteRoom(roomId);
        }


        public List<AdvancedRenovationJoining> GetAll()
        {
            return _advancedRenovationJoiningRepository.FindAll();
        }

        public int GenerateNewId()
        {
            try
            {
                List<AdvancedRenovationJoining> renovations = _advancedRenovationJoiningRepository.FindAll();
                int currentMax = renovations.Max(obj => obj.Id);
                return currentMax + 1;
            }
            catch
            {
                return 1;
            }
        }

    }
}
