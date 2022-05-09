using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using ZdravoKorporacija.DTO;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;

namespace ZdravoKorporacija.Service
{
    public class EquipmentService
    {


        private readonly EquipmentRepository EquipmentRepository;
        private readonly RoomRepository RoomRepository;
        private readonly DisplacementRepository DisplacementRepository;

        public EquipmentService(EquipmentRepository equipmentRepository, RoomRepository roomRepository, DisplacementRepository displacementRepository)
        {
            this.EquipmentRepository = equipmentRepository;
            this.RoomRepository = roomRepository;
            this.DisplacementRepository = displacementRepository;
        }

        public void CreateEquipment(String equipmentName, Boolean isStatic, int Quantitity, int? RoomId)
        {
            int equipmentId = GenerateNewId();

            if (EquipmentRepository.FindOneById(equipmentId) != null)
            {
                throw new Exception("Equipment with that identification number already exists!");
            }
            else
            {
                if (isStatic == true)
                {
                    if (RoomRepository.FindOneById((int)RoomId) == null)
                    {
                        throw new Exception("Room doesn't exist");
                    }
                    else
                    {
                        Equipment newEquipmentStatic = new Equipment(equipmentId, equipmentName, true, 1, RoomId);
                        if (!newEquipmentStatic.validateEquipment())
                        {
                            throw new Exception("Something went wrong, equipment isn't saved");
                        }
                        EquipmentRepository.SaveEquipment(newEquipmentStatic);
                    }
                }
                else
                {
                    Equipment newEquipmentDinamic = new Equipment(equipmentId, equipmentName, false, Quantitity, null);
                    if (!newEquipmentDinamic.validateEquipment())
                    {
                        throw new Exception("Something went wrong, equipment isn't saved");
                    }
                    EquipmentRepository.SaveEquipment(newEquipmentDinamic);

                }

            }
        }

        public void DeleteEquipment(int equipmentId)
        {
            if (EquipmentRepository.FindOneById(equipmentId) == null)
            {
                throw new Exception("Equipment with that identification number doesn't exist");
            }
            else
            {
                EquipmentRepository.RemoveEquipment(equipmentId);
            }
        }

        public int GenerateNewId()
        {
            try
            {
                List<Equipment> equipment = EquipmentRepository.FindAll();
                int currentMax = equipment.Max(obj => obj.Id);
                return currentMax + 1;
            }
            catch
            {
                return 1;
            }
        }

        public List<Equipment> GetAllEquipment()
        {
            return EquipmentRepository.FindAll();
        }


        public List<EquipmentDTO> GetEquipmentDTOs()
        {
            List<Equipment> EquipmentList = GetAllEquipment();
            List<EquipmentDTO> EquipmentDTOList = new List<EquipmentDTO>();


            foreach (Equipment equipment in EquipmentList)
            {
                EquipmentDTO equipmentDTO = new EquipmentDTO();
                equipmentDTO.Id = equipment.Id;
                equipmentDTO.Name = equipment.Name;
                equipmentDTO.Quantity = equipment.Quantity;
                equipmentDTO.RoomId = equipment.RoomId;

                if (equipment.IsStatic == true)
                {
                    equipmentDTO.IsStatic = "STATIC";
                }
                else
                {
                    equipmentDTO.IsStatic = "DYNAMIC";
                }

                if (equipment.RoomId == null)
                {
                    equipmentDTO.RoomName = "/";
                }
                else
                {
                    equipmentDTO.RoomName = RoomRepository.FindOneById(equipment.RoomId).Name;
                }

                EquipmentDTOList.Add(equipmentDTO);

            }

            return EquipmentDTOList;
        }

        public int GenerateNewIdDisplacement()
        {
            try
            {
                List<Displacement> displacements = DisplacementRepository.FindAll();
                int currentMax = displacements.Max(obj => obj.Id);
                return currentMax + 1;
            }
            catch
            {
                return 1;
            }
        }


        public void CreateDisplacement(int startRoom, int endRoom, int equiomentId, DateTime displacementDate)
        {
            if (EquipmentRepository.FindOneById(equiomentId).IsStatic == true)
            {

                int displacementId = GenerateNewIdDisplacement();

                if (RoomRepository.FindOneById(startRoom) == null)
                {

                    throw new Exception("Room with that identification number doesn't exist!");
                }
                else if (RoomRepository.FindOneById(endRoom) == null)
                {

                    throw new Exception("Room with that identification number doesn't exist!");
                }
                else if (EquipmentRepository.FindOneById(equiomentId) == null)
                {
                    throw new Exception("Equipment with that identification number doesn't exist!");
                }
                else if (DisplacementRepository.FindOneById(displacementId) != null)
                {
                    throw new Exception("Displacement with that identification number already exists!");
                }
                else
                {
                    Displacement displacement = new Displacement(displacementId, startRoom, endRoom, equiomentId, 1, displacementDate);
                    if (!displacement.validateDisplacement())
                    {
                        throw new Exception("Something went wrong, displacement isn't saved");
                    }

                    DisplacementRepository.SaveDisplacement(displacement);
                }

            }
            else
            {
                throw new Exception("Equipment must be static!");
            }

        }


        public List<Displacement> GetAllDisplacements()
        {
            return DisplacementRepository.FindAll();
        }




        public void EquipmentDisplacement()
        {
            List<Displacement> displacements = new List<Displacement>(GetAllDisplacements());
            foreach (Displacement displacement in displacements)
            {
                Equipment equipment = EquipmentRepository.FindOneById(displacement.StaticEquipmentId);


                if (displacement.DisplacementDate == System.DateTime.Today)
                {
                    if (equipment != null)
                    {
                        equipment.RoomId = displacement.EndRoom;
                    }
                    else
                    {
                        throw new Exception("Equipment with that identification number doesn't exist.");
                    }

                    if (EquipmentRepository.FindOneByRoomId(displacement.EndRoom) == null) //da mi se ne bi svaki put upisivala 
                    {
                        EquipmentRepository.SaveEquipment(equipment);
                    }
                    EquipmentRepository.RemoveEquipmentByRoom(displacement.StartRoom);


                }
            }

        }


        public List<EquipmentDTO> Filter(Boolean isStaticEquipment)
        {

            List<EquipmentDTO> equipmentDTOs = new List<EquipmentDTO>(GetEquipmentDTOs());
            List<EquipmentDTO> staticEquipment = new List<EquipmentDTO>();
            List<EquipmentDTO> dynamicEquipment = new List<EquipmentDTO>();

            if(isStaticEquipment == true)
            {

                foreach (EquipmentDTO equipmentDTO in equipmentDTOs)
                {
                    if(equipmentDTO.IsStatic == "STATIC")
                    {
                        staticEquipment.Add(equipmentDTO);
                    }
                }

                return staticEquipment;


            }
            else
            {

                foreach (EquipmentDTO equipmentDTO in equipmentDTOs)
                {
                    if (equipmentDTO.IsStatic == "DYNAMIC")
                    {
                        dynamicEquipment.Add(equipmentDTO);
                    }
                }

                return dynamicEquipment;

            }
        }

        public List<EquipmentDTO> Search(string name)
        {
            List<EquipmentDTO> equipmentDTOs = new List<EquipmentDTO>(GetEquipmentDTOs());
            List<EquipmentDTO> resultingEquipment = new List<EquipmentDTO>();


            foreach(EquipmentDTO equipmentDTO in equipmentDTOs)
            {

                if(equipmentDTO.Name == name)
                {
                    resultingEquipment.Add(equipmentDTO);
                }
            }

            return resultingEquipment;

        }





    }
}
