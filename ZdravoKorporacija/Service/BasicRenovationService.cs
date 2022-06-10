using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using ZdravoKorporacija.Interfaces;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;

namespace ZdravoKorporacija.Service
{
    public class BasicRenovationService
    {

        private readonly IBasicRenovationRepository _basicRenovationRepository;
        private readonly IRoomRepository _roomRepository;
        public BasicRenovationService(IBasicRenovationRepository basicRenovationRepository, IRoomRepository roomRepository)
        {
            this._basicRenovationRepository = basicRenovationRepository;
            this._roomRepository = _roomRepository;
        }


        public void CreateBasicRenovation(int roomId, DateTime startTime, int duration, string description)
        {
            int basicRenovationId = GenerateNewId();

            BasicRenovation basicRenovation = new BasicRenovation(basicRenovationId, roomId, startTime, duration, description);

            if (!basicRenovation.validateBasicRenovation())
            {
                throw new Exception("Something went wrong, basic renovation isn't saved");
            }

            _basicRenovationRepository.SaveBasicRenovation(basicRenovation);


        }

        public void DeleteByRoomId(int roomId)
        {
            _basicRenovationRepository.RemoveBasicRenovationByRoomId(roomId);
        }


        public int GenerateNewId()
        {
            try
            {
                List<BasicRenovation> renovations = _basicRenovationRepository.FindAll();
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
