﻿using System;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.Controller
{
    public class BasicRenovationController
    {

        private readonly BasicRenovationService BasicRenovationService;

        public BasicRenovationController(BasicRenovationService basicRenovationService)
        {
            this.BasicRenovationService = basicRenovationService;
        }


        public void CreateBasicRenovation(int roomId, DateTime startTime, int duration, string description)
        {
            BasicRenovationService.CreateBasicRenovation(roomId, startTime, duration, description);
        }
    }
}
