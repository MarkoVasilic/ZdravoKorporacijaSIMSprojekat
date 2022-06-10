using Model;
using System;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.Controller
{
    public class AdvancedRenovationSeparationController
    {

        private readonly AdvancedRenovationSeparationService _advancedRenovationSeparationService;

        public AdvancedRenovationSeparationController(AdvancedRenovationSeparationService advancedRenovationSeparationService)
        {
            this._advancedRenovationSeparationService = advancedRenovationSeparationService;
        }

        public void Create(int startRoomId, DateTime startTime, int duration, String resultFirstRoomName, String resultSecondRoomName, String resultFirstRoomDescription, String resultSecondRoomDescription, RoomType firstRoomType, RoomType secondRoomType)
        {
            _advancedRenovationSeparationService.Create(startRoomId, startTime, duration, resultFirstRoomName, resultSecondRoomName, resultFirstRoomDescription, resultSecondRoomDescription, firstRoomType, secondRoomType);
        }

        public void SeparateRooms()
        {
            _advancedRenovationSeparationService.SeparateRooms();
        }
    }
}
