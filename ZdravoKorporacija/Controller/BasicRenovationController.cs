using System;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.Controller
{
    public class BasicRenovationController
    {

        private readonly BasicRenovationService _basicRenovationService;

        public BasicRenovationController(BasicRenovationService basicRenovationService)
        {
            this._basicRenovationService = basicRenovationService;
        }


        public void CreateBasicRenovation(int roomId, DateTime startTime, int duration, string description)
        {
            _basicRenovationService.CreateBasicRenovation(roomId, startTime, duration, description);
        }
    }
}
