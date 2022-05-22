﻿using Model;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;

namespace ZdravoKorporacija.Service
{
    public class AdvancedRenovationSeparationService
    {

        private readonly AdvancedRenovationSeparationRepository AdvancedRenovationSeparationRepository;
        private readonly RoomService RoomService;

        public AdvancedRenovationSeparationService() { }

        public AdvancedRenovationSeparationService(AdvancedRenovationSeparationRepository advancedRenovationSeparationRepository, RoomService roomService)
        {
            AdvancedRenovationSeparationRepository = advancedRenovationSeparationRepository;
            RoomService = roomService;
        }


        public void Create(int startRoomId, DateTime startTime, int duration, String resultFirstRoomName, String resultSecondRoomName, String resultFirstRoomDescription, String resultSecondRoomDescription, RoomType firstRoomType, RoomType secondRoomType)
        {
            int advancedRenovationId = GenerateNewId();

            AdvancedRenovationSeparation advancedRenovationSeparation = new AdvancedRenovationSeparation(advancedRenovationId, startRoomId, startTime, duration, resultFirstRoomName, resultSecondRoomName, resultFirstRoomDescription, resultSecondRoomDescription, firstRoomType, secondRoomType);

            if (!advancedRenovationSeparation.Validate())
            {
                throw new Exception("Something went wrong, renovation isn't saved");
            }

            AdvancedRenovationSeparationRepository.SaveSeparation(advancedRenovationSeparation);
        }


        public List<AdvancedRenovationSeparation> GetAll()
        {
            return AdvancedRenovationSeparationRepository.FindAll();
        }



        public void SeparateRooms()
        {
            List<AdvancedRenovationSeparation> advancedRenovationSeparations = GetAll();
            List<int> advancedRenovationIds = new List<int>();

            foreach(AdvancedRenovationSeparation advancedRenovationSeparation in advancedRenovationSeparations)
            {
                PerformSeparation(advancedRenovationSeparation, advancedRenovationIds);
            }

            for (int i = 0; i <advancedRenovationIds.Count; i++)
            {
                AdvancedRenovationSeparationRepository.RemoveSeparation(advancedRenovationSeparations[i].Id);
            }
        }


        public void PerformSeparation(AdvancedRenovationSeparation advancedRenovationSeparation, List<int> renovationIds)
        {

            Room oldRoom = RoomService.GetRoomById(advancedRenovationSeparation.StartRoomId);
            if(advancedRenovationSeparation.StartTime <= DateTime.Today)
            {
                if (oldRoom != null)
                {
                    ChangeRoomInformation(advancedRenovationSeparation, oldRoom);
                    RoomService.ModifyRoomForRenovation(oldRoom);
                    RoomService.CreateRoom(advancedRenovationSeparation.ResultSecondRoomName, advancedRenovationSeparation.ResultSecondRoomDescription, advancedRenovationSeparation.SecondRoomType);
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
                List<AdvancedRenovationSeparation> renovations = AdvancedRenovationSeparationRepository.FindAll();
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
