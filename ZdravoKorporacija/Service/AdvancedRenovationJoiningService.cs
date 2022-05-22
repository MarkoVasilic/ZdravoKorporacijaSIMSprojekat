using Model;
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
    public class AdvancedRenovationJoiningService
    {

        private readonly AdvancedRenovationJoiningRepository AdvancedRenovationJoiningRepository;
        private readonly RoomService RoomService;
        private readonly AppointmentService AppointmentService;
        private readonly BasicRenovationService BasicRenovationService;
        private readonly EquipmentService EquipmentService;

        public AdvancedRenovationJoiningService(AdvancedRenovationJoiningRepository advancedRenovationJoiningRepository, RoomService roomService, AppointmentService appointmentService, BasicRenovationService basicRenovationService, EquipmentService equipmentService)
        {
            AdvancedRenovationJoiningRepository = advancedRenovationJoiningRepository;
            RoomService = roomService;
            AppointmentService = appointmentService;
            BasicRenovationService = basicRenovationService;
            EquipmentService = equipmentService;
        }


        public void Create(int firstStartRoom, int secondStartroom, DateTime startTime, int duration, String resultRoomName, String resultRoomDescription, RoomType resultRoomType)
        {
            int advancedRenovationId = GenerateNewId();

            AdvancedRenovationJoining advancedRenovationJoining = new AdvancedRenovationJoining(advancedRenovationId, firstStartRoom, secondStartroom, startTime, duration, resultRoomName, resultRoomDescription, resultRoomType);
            if (!advancedRenovationJoining.validate())
            {
                throw new Exception("Something went wrong, renovation isn't saved");
            }

            AdvancedRenovationJoiningRepository.SaveJoining(advancedRenovationJoining);
        }



        public void JoinRooms()
        {
            List<AdvancedRenovationJoining> advancedRenovations = GetAll();
            List<int> advancedRenovationIds = new List<int>();

            foreach (AdvancedRenovationJoining advancedRenovation in advancedRenovations)
            {
                PerformJoining(advancedRenovation, advancedRenovationIds);
            }

            for (int i = 0; i < advancedRenovationIds.Count; i++)
            {
                AdvancedRenovationJoiningRepository.RemoveJoining(advancedRenovationIds[i]);
            }

        }

        public void PerformJoining(AdvancedRenovationJoining advancedRenovation, List<int> renovationIds)
        {

            Room oldRoom = RoomService.GetRoomById(advancedRenovation.FirstStartRoom);

            if(advancedRenovation.StartTime <= DateTime.Today)
            {
                if (oldRoom != null)
                {
                    ChangeRoomInformation(advancedRenovation, oldRoom);
                    RoomService.ModifyRoomForRenovation(oldRoom);
                    DeleteEverythingForRoom(advancedRenovation.SecondStartRoom);
                    renovationIds.Add(advancedRenovation.Id);

                }
                else
                {
                    throw new Exception("Room with that identification number doesn't exist");
                }
            }
        }


        public void ChangeRoomInformation(AdvancedRenovationJoining advancedRenovationJoining, Room room)
        {
            room.Name = advancedRenovationJoining.ResultRoomName;
            room.Description = advancedRenovationJoining.ResultRoomDescription;
            room.Type = advancedRenovationJoining.ResultRoomType;

        }

        public void DeleteEverythingForRoom(int roomId)
        {
            AppointmentService.DeleteAppointmentByRoomId(roomId);
            BasicRenovationService.DeleteByRoomId(roomId);
            EquipmentService.DeleteByRoomId(roomId);
            EquipmentService.DeleteDisplacementByStartRoomId(roomId);
            EquipmentService.DeleteDisplacementByEndRoomId(roomId);
            RoomService.DeleteRoom(roomId);
        }


        public List<AdvancedRenovationJoining> GetAll()
        {
            return AdvancedRenovationJoiningRepository.FindAll();
        }

        public int GenerateNewId()
        {
            try
            {
                List<AdvancedRenovationJoining> renovations = AdvancedRenovationJoiningRepository.FindAll();
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
