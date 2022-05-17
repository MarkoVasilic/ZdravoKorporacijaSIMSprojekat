﻿using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.Controller
{
    public class AdvancedRenovationJoiningController
    {

        private readonly AdvancedRenovationJoiningService AdvancedRenovationJoiningService;

        public AdvancedRenovationJoiningController(AdvancedRenovationJoiningService advancedRenovationJoiningService)
        {
            AdvancedRenovationJoiningService = advancedRenovationJoiningService;
        }

        public void Create(int firstStartRoom, int secondStartroom, DateTime startTime, int duration, String resultRoomName, String resultRoomDescription, RoomType resultRoomType){

            AdvancedRenovationJoiningService.Create(firstStartRoom, secondStartroom, startTime, duration, resultRoomName, resultRoomDescription, resultRoomType);
        }

        public void Join()
        {
            AdvancedRenovationJoiningService.Join();
        }

        public List<AdvancedRenovationJoining> GetAll()
        {
            return AdvancedRenovationJoiningService.GetAll();
        }
    }

}
