using Model;
using System;
using System.Collections.Generic;
using ZdravoKorporacija.DTO;
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

        public void Create(int firstStartRoom, int secondStartroom, DateTime startTime, int duration, String resultRoomName, String resultRoomDescription, RoomType resultRoomType)
        {

            AdvancedRenovationJoiningService.Create(firstStartRoom, secondStartroom, startTime, duration, resultRoomName, resultRoomDescription, resultRoomType);
        }

        public void JoinRooms()
        {
            AdvancedRenovationJoiningService.JoinRooms();
        }

        public List<AdvancedRenovationJoining> GetAll()
        {
            return AdvancedRenovationJoiningService.GetAll();
        }
        public List<PossibleAppointmentsDTO> GetPossibleAppointmentsForRoomJoin(int firstRoomId, int secondRoomId,
            DateTime dateFrom, DateTime dateUntil, int duration)
        {
            return AdvancedRenovationJoiningService.GetPossibleAppointmentsForRoomJoin(firstRoomId, secondRoomId, dateFrom, dateUntil, duration);
        }
    }

}
