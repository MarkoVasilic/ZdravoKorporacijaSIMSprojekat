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

        public void CreateEquipment(String equipmentName, Boolean isStatic, int Quantitity, int? RoomId)
        {
            EquipmentService.CreateEquipment(equipmentName, isStatic, Quantitity, RoomId);
        }

        public List<Equipment> GetAllEquipment()
        {
            return EquipmentService.GetAllEquipment();
        }

        public List<EquipmentDTO> GetEquipmentDTOs()
        {
            return EquipmentService.GetEquipmentDTOs();
        }

        public void CreateDisplacement(int startRoom, int endRoom, int equiomentId, DateTime displacementDate)
        {
            EquipmentService.CreateDisplacement(startRoom, endRoom, equiomentId, displacementDate);
        }

        public List<Displacement> GetAllDisplacements()
        {
            return EquipmentService.GetAllDisplacements();
        }

        public void EquipmentDisplacement()
        {
            EquipmentService.EquipmentDisplacement();
        }

        public List<EquipmentDTO> Filter(Boolean isStaticEquipment)
        {
            return EquipmentService.Filter(isStaticEquipment);
        }

        public List<EquipmentDTO> Search(string name)
        {
            return EquipmentService.Search(name);
        }



    }
}
