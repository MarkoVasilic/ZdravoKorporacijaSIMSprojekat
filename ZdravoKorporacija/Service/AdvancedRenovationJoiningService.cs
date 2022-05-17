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

            AdvancedRenovationJoiningRepository.SaveAdvancedRenovationJoining(advancedRenovationJoining);
        }



        public void Join()
        {
            List<AdvancedRenovationJoining> advancedRenovations = GetAll();
            List<int> advancedRenovationIds = new List<int>();

            foreach (AdvancedRenovationJoining advancedRenovation in advancedRenovations)
            {

                Room oldRoom = RoomService.GetRoomById(advancedRenovation.FirstStartRoom);

                if (oldRoom != null)
                {
                    oldRoom.Name = advancedRenovation.ResultRoomName;
                    oldRoom.Description = advancedRenovation.ResultRoomDescription;
                    oldRoom.Type = advancedRenovation.ResultRoomType;
                    RoomService.ModifyRoomForRenovation(oldRoom.Id, oldRoom.Name, oldRoom.Description, oldRoom.Type);
                    AppointmentService.DeleteAppointmentByRoomId(advancedRenovation.SecondStartRoom);
                    BasicRenovationService.DeleteByRoomId(advancedRenovation.SecondStartRoom);
                    EquipmentService.DeleteByRoomId(advancedRenovation.SecondStartRoom);
                    EquipmentService.DeleteDisplacementByStartRoomId(advancedRenovation.SecondStartRoom);
                    EquipmentService.DeleteDisplacementByEndRoomId(advancedRenovation.SecondStartRoom);
                    RoomService.DeleteRoom(advancedRenovation.SecondStartRoom);
                    advancedRenovationIds.Add(advancedRenovation.Id);

                }
                else
                {
                    throw new Exception("Room with that identification number doesn't exist");
                }
               

            }

            for (int i = 0; i < advancedRenovationIds.Count; i++)
            {
                AdvancedRenovationJoiningRepository.RemoveAdvancedRenovationJoining(advancedRenovationIds[i]);
            }

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
