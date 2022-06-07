using System;
using System.Collections.Generic;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.Controller
{
    public class EquipmentController
    {

        private readonly EquipmentService EquipmentService;

        public EquipmentController(EquipmentService equipmentService)
        {
            this.EquipmentService = equipmentService;
        }

        public void Create(String name, Boolean isStatic, int? quantity, int? roomId, DateTime? dynamicAddDate)
        {
            EquipmentService.Create(name, isStatic, quantity, roomId, dynamicAddDate);
        }

        public List<Equipment> GetAll()
        {
            return EquipmentService.GetAll();
        }

        public List<EquipmentDTO> GetEquipmentDTOs()
        {
            return EquipmentService.GetEquipmentDTOs();
        }

        public List<EquipmentDTO> Filter(String equipmentType)
        {
            return EquipmentService.Filter(equipmentType);
        }

        public List<EquipmentDTO> Search(string name)
        {
            return EquipmentService.Search(name);
        }

        public List<EquipmentDTO> GetAllByRoomId(int id)
        {
            return EquipmentService.GetAllByRoomId(id);
        }


    }
}
