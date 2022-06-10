using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Interfaces;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;

namespace ZdravoKorporacija.Service
{
    public class EquipmentService
    {


        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IRoomRepository _roomRepository;

        public EquipmentService(IEquipmentRepository equipmentRepository, IRoomRepository roomRepository)
        {
            this._equipmentRepository = equipmentRepository;
            this._roomRepository = roomRepository;

        }

        public void Create(String name, Boolean isStatic, int? quantity, int? roomId, DateTime? dynamicAddDate)
        {
            int equipmentId = GenerateNewId();

            if (_equipmentRepository.FindOneById(equipmentId) != null)
            {
                throw new Exception("Equipment with that identification number already exists!");
            }
            else
            {
                CheckTypeAndCreate(isStatic, equipmentId, name, roomId, quantity, dynamicAddDate);
            }
        }


        public void CheckTypeAndCreate(Boolean isStatic, int id, String name, int? roomId, int? quantity, DateTime? dynamicAddDate)
        {
            if(isStatic == true)
            {
                CreateStatic(id, name, roomId);
            }
            else
            {
                CreateDynamic(id, name, quantity, dynamicAddDate);
            }
            
        }



        public void CreateStatic(int id, String name, int? roomId)
        {

            if (_roomRepository.FindOneById((int)roomId) == null)
            {
                throw new Exception("Room doesn't exist");
            }
            else
            {
                Equipment newEquipmentStatic = new Equipment(id, name, true, 1, roomId, null);
                if (!newEquipmentStatic.validate())
                {
                    throw new Exception("Something went wrong, equipment isn't saved");
                }
                _equipmentRepository.SaveEquipment(newEquipmentStatic);
            }
        }


        public void CreateDynamic(int id, String name, int? quaninty, DateTime? dynamicAddDate)
        {

            Equipment equipment = _equipmentRepository.FindOneByName(name);
            if (equipment != null)
            {
                equipment.Quantity = quaninty + equipment.Quantity;
                _equipmentRepository.UpdateEquipment(equipment);
            }
            else
            {
                Equipment newEquipmentDynamic = new Equipment(id, name, false, quaninty,
                    null, dynamicAddDate);
                if (!newEquipmentDynamic.validate())
                {
                    throw new Exception("Something went wrong, equipment isn't saved");
                }

                _equipmentRepository.SaveEquipment(newEquipmentDynamic);
            }

        }

        public void Delete(int id)
        {
            if (_equipmentRepository.FindOneById(id) == null)
            {
                throw new Exception("Equipment with that identification number doesn't exist");
            }
            _equipmentRepository.RemoveEquipment(id);
            
        }

        public int GenerateNewId()
        {
            try
            {
                List<Equipment> equipment = _equipmentRepository.FindAll();
                int currentMax = equipment.Max(obj => obj.Id);
                return currentMax + 1;
            }
            catch
            {
                return 1;
            }
        }

        public List<Equipment> GetAll()
        {
            return _equipmentRepository.FindAll();
        }


        public List<EquipmentDTO> GetEquipmentDTOs()
        {
            List<Equipment> EquipmentList = GetAll();
            List<EquipmentDTO> EquipmentDTOList = new List<EquipmentDTO>();


            foreach (Equipment equipment in EquipmentList)
            {
                EquipmentDTO equipmentDTO = SetEquipmentDTO(equipment);

                if (!equipment.IsStatic && equipment.DynamicAddDate <= DateTime.Now)
                    EquipmentDTOList.Add(equipmentDTO);
                else if (equipment.IsStatic)
                    EquipmentDTOList.Add(equipmentDTO);

            }

            return EquipmentDTOList;
        }


        public EquipmentDTO SetEquipmentDTO(Equipment equipment)
        {
            EquipmentDTO equipmentDTO = new EquipmentDTO();
            equipmentDTO.Id = equipment.Id;
            equipmentDTO.Name = equipment.Name;
            equipmentDTO.Quantity = equipment.Quantity;
            equipmentDTO.RoomId = equipment.RoomId;

            if (equipment.IsStatic == true)
            {
                equipmentDTO.IsStatic = "TRAJNA";
            }
            else
            {
                equipmentDTO.IsStatic = "POTROŠNA";
            }

            if (equipment.RoomId == null)
            {
                equipmentDTO.RoomName = "/";
            }
            else
            {
                equipmentDTO.RoomName = _roomRepository.FindOneById(equipment.RoomId).Name;
            }

            return equipmentDTO;
        }

     
        public List<EquipmentDTO> Filter(String equipmentType)
        {
            return GetByType(equipmentType);

        }


        public List<EquipmentDTO> GetByType(String equipmentType)
        {

            List<EquipmentDTO> equipmentDTOs = new List<EquipmentDTO>(GetEquipmentDTOs());
            List<EquipmentDTO> foundEquipment = new List<EquipmentDTO>();

            foreach (EquipmentDTO equipmentDTO in equipmentDTOs)
            {
                if (equipmentDTO.IsStatic == equipmentType)
                {
                    foundEquipment.Add(equipmentDTO);
                }
            }

            return foundEquipment;

        }

        public List<EquipmentDTO> Search(string name)
        {
            List<EquipmentDTO> equipmentDTOs = new List<EquipmentDTO>(GetEquipmentDTOs());
            List<EquipmentDTO> resultingEquipment = new List<EquipmentDTO>();


            foreach (EquipmentDTO equipmentDTO in equipmentDTOs)
            {

                if (equipmentDTO.Name == name)
                {
                    resultingEquipment.Add(equipmentDTO);
                }
            }

            return resultingEquipment;

        }

        public void DeleteByRoomId(int id)
        {
            _equipmentRepository.RemoveEquipmentByRoom(id);
        }


        public List<EquipmentDTO> GetAllByRoomId(int id)
        {
            List<Equipment> equipment = _equipmentRepository.FindAllByRoomId(id);
            List<EquipmentDTO> equipmentDTO = new List<EquipmentDTO> ();

            foreach(Equipment eq in equipment)
            {
                EquipmentDTO eqDTO = SetEquipmentDTO(eq);
                equipmentDTO.Add(eqDTO);    
            }
            return equipmentDTO;
        }

    }
}
