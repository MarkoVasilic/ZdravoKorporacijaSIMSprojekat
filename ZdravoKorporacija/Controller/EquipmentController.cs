using System;
using System.Collections.Generic;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.Controller
{
    public class EquipmentController
    {

        private readonly EquipmentService _equipmentService;

        public EquipmentController(EquipmentService equipmentService)
        {
            this._equipmentService = equipmentService;
        }

        public void Create(String name, Boolean isStatic, int? quantity, int? roomId, DateTime? dynamicAddDate)
        {
            _equipmentService.Create(name, isStatic, quantity, roomId, dynamicAddDate);
        }

        public List<Equipment> GetAll()
        {
            return _equipmentService.GetAll();
        }

        public List<EquipmentDTO> GetEquipmentDTOs()
        {
            return _equipmentService.GetEquipmentDTOs();
        }

        public List<EquipmentDTO> Filter(String equipmentType)
        {
            return _equipmentService.Filter(equipmentType);
        }

        public List<EquipmentDTO> Search(string name)
        {
            return _equipmentService.Search(name);
        }

        public List<EquipmentDTO> GetAllByRoomId(int id)
        {
            return _equipmentService.GetAllByRoomId(id);
        }


    }
}
