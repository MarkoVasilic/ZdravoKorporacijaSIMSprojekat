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

        private readonly AdvancedRenovationJoiningService _advancedRenovationJoiningService;

        public AdvancedRenovationJoiningController(AdvancedRenovationJoiningService advancedRenovationJoiningService)
        {
            _advancedRenovationJoiningService = advancedRenovationJoiningService;
        }

        public void Create(int firstStartRoom, int secondStartroom, DateTime startTime, int duration, String resultRoomName, String resultRoomDescription, RoomType resultRoomType)
        {

            _advancedRenovationJoiningService.Create(firstStartRoom, secondStartroom, startTime, duration, resultRoomName, resultRoomDescription, resultRoomType);
        }

        public void JoinRooms()
        {
            _advancedRenovationJoiningService.JoinRooms();
        }

        public List<AdvancedRenovationJoining> GetAll()
        {
            return _advancedRenovationJoiningService.GetAll();
        }
        public List<PossibleAppointmentsDTO> GetPossibleAppointments(int firstRoomId, int secondRoomId,
            DateTime dateFrom, DateTime dateUntil, int duration)
        {
            return _advancedRenovationJoiningService.GetPossibleAppointments(firstRoomId, secondRoomId, dateFrom, dateUntil, duration);
        }
    }

}
