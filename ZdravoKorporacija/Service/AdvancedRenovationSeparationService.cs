using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using ZdravoKorporacija.Interfaces;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;

namespace ZdravoKorporacija.Service
{
    public class AdvancedRenovationSeparationService
    {

        private readonly IAdvancedRenovationSeparationRepository _advancedRenovationSeparationRepository;
        private readonly RoomService _roomService;

        public AdvancedRenovationSeparationService() { }

        public AdvancedRenovationSeparationService(IAdvancedRenovationSeparationRepository advancedRenovationSeparationRepository, RoomService roomService)
        {
            _advancedRenovationSeparationRepository = advancedRenovationSeparationRepository;
            _roomService = roomService;
        }


        public void Create(int startRoomId, DateTime startTime, int duration, String resultFirstRoomName, String resultSecondRoomName, String resultFirstRoomDescription, String resultSecondRoomDescription, RoomType firstRoomType, RoomType secondRoomType)
        {
            int advancedRenovationId = GenerateNewId();

            AdvancedRenovationSeparation advancedRenovationSeparation = new AdvancedRenovationSeparation(advancedRenovationId, startRoomId, startTime, duration, resultFirstRoomName, resultSecondRoomName, resultFirstRoomDescription, resultSecondRoomDescription, firstRoomType, secondRoomType);

            if (!advancedRenovationSeparation.Validate())
            {
                throw new Exception("Something went wrong, renovation isn't saved");
            }

            _advancedRenovationSeparationRepository.SaveSeparation(advancedRenovationSeparation);
        }


        public List<AdvancedRenovationSeparation> GetAll()
        {
            return _advancedRenovationSeparationRepository.FindAll();
        }



        public void SeparateRooms()
        {
            List<AdvancedRenovationSeparation> advancedRenovationSeparations = GetAll();
            List<int> advancedRenovationIds = new List<int>();

            foreach (AdvancedRenovationSeparation advancedRenovationSeparation in advancedRenovationSeparations)
            {
                PerformSeparation(advancedRenovationSeparation, advancedRenovationIds);
            }

            for (int i = 0; i < advancedRenovationIds.Count; i++)
            {
                _advancedRenovationSeparationRepository.RemoveSeparation(advancedRenovationSeparations[i].Id);
            }
        }


        public void PerformSeparation(AdvancedRenovationSeparation advancedRenovationSeparation, List<int> renovationIds)
        {

            Room oldRoom = _roomService.GetRoomById(advancedRenovationSeparation.StartRoomId);
            if (advancedRenovationSeparation.StartTime <= DateTime.Today)
            {
                if (oldRoom != null)
                {
                    ChangeRoomInformation(advancedRenovationSeparation, oldRoom);
                    _roomService.ModifyRoomForRenovation(oldRoom);
                    _roomService.CreateRoom(advancedRenovationSeparation.ResultSecondRoomName, advancedRenovationSeparation.ResultSecondRoomDescription, advancedRenovationSeparation.SecondRoomType);
                    renovationIds.Add(advancedRenovationSeparation.Id);
                }
                else
                {
                    throw new Exception("Room with that identification number doesn't exits");
                }
            }

        }

        public void ChangeRoomInformation(AdvancedRenovationSeparation advancedRenovationSeparation, Room room)
        {
            room.Name = advancedRenovationSeparation.ResultFirstRoomName;
            room.Description = advancedRenovationSeparation.ResultFirstRoomDescription;
            room.Type = advancedRenovationSeparation.FirstRoomType;
        }

        public int GenerateNewId()
        {
            try
            {
                List<AdvancedRenovationSeparation> renovations = _advancedRenovationSeparationRepository.FindAll();
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
