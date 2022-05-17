using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.Controller
{
    public class AdvancedRenovationSeparationController
    {

        private AdvancedRenovationSeparationService AdvancedRenovationSeparationService;

        public AdvancedRenovationSeparationController(AdvancedRenovationSeparationService advancedRenovationSeparationService)
        {
            this.AdvancedRenovationSeparationService = advancedRenovationSeparationService;
        }

        public void Create(int startRoomId, DateTime startTime, int duration, String resultFirstRoomName, String resultSecondRoomName, String resultFirstRoomDescription, String resultSecondRoomDescription, RoomType firstRoomType, RoomType secondRoomType)
        {
            AdvancedRenovationSeparationService.Create(startRoomId, startTime, duration, resultFirstRoomName, resultSecondRoomName, resultFirstRoomDescription, resultSecondRoomDescription, firstRoomType, secondRoomType);
        }

        public void Separate()
        {
            AdvancedRenovationSeparationService.Separate();
        }
    }
}
