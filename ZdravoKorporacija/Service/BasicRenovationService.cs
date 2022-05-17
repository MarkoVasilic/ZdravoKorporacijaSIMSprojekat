﻿using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;

namespace ZdravoKorporacija.Service
{
    public class BasicRenovationService
    {

        private readonly BasicRenovationRepository BasicRenovationRepository;
        private readonly RoomRepository RoomRepository;
        public BasicRenovationService(BasicRenovationRepository basicRenovationRepository, RoomRepository roomRepository)
        {
            this.BasicRenovationRepository = basicRenovationRepository;
            this.RoomRepository = RoomRepository;
        }


        public void CreateBasicRenovation(int roomId, DateTime startTime, int duration, string description)
        {
            int basicRenovationId = GenerateNewId();

            BasicRenovation basicRenovation = new BasicRenovation(basicRenovationId, roomId, startTime, duration, description);

            if (!basicRenovation.validateBasicRenovation())
            {
                throw new Exception("Something went wrong, basic renovation isn't saved");
            }

            BasicRenovationRepository.SaveBasicRenovation(basicRenovation);


        }

        public void DeleteByRoomId(int roomId)
        {
            BasicRenovationRepository.RemoveBasicRenovationByRoomId(roomId);
        }


        public int GenerateNewId()
        {
            try
            {
                List<BasicRenovation> renovations = BasicRenovationRepository.FindAll();
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
