using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKorporacija.Interfaces;
using ZdravoKorporacija.Model;

namespace ZdravoKorporacija.Service
{
    public class DisplacementService
    {

        private readonly IDisplacementRepository DisplacementRepository;
        private readonly IEquipmentRepository EquipmentRepository;
        private readonly IRoomRepository RoomRepository;

        public DisplacementService(IDisplacementRepository displacementRepository, IEquipmentRepository equipmentRepository, IRoomRepository roomRepository)
        {
            this.DisplacementRepository = displacementRepository;
            this.EquipmentRepository = equipmentRepository;
            this.RoomRepository = roomRepository;
        }

        public int GenerateNewId()
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


        public void Create(int startRoom, int endRoom, int equiomentId, DateTime displacementDate)
        {
            if (EquipmentRepository.FindOneById(equiomentId).IsStatic == true)
            {
                int displacementId = GenerateNewId();
                CheckBeforeCreating(startRoom, endRoom, equiomentId, displacementDate, displacementId);
                Displacement displacement = new Displacement(displacementId, startRoom, endRoom, equiomentId, 1, displacementDate);
                Validate(displacement);
                DisplacementRepository.SaveDisplacement(displacement);

            }
            else
            {

                throw new Exception("Equipment must be static");
            }

        }

        public void Validate(Displacement displacement)
        {
            if (!displacement.validate())
            {
                throw new Exception("Something went wrong, displacement isn't saved");
            }
        }


        public void CheckBeforeCreating(int startRoom, int endRoom, int equipmentId, DateTime displacementDate, int displacementId)
        {
            if (RoomRepository.FindOneById(startRoom) == null)
            {

                throw new Exception("Room with that identification number doesn't exist!");
            }
            else if (RoomRepository.FindOneById(endRoom) == null)
            {

                throw new Exception("Room with that identification number doesn't exist!");
            }
            else if (EquipmentRepository.FindOneById(equipmentId) == null)
            {
                throw new Exception("Equipment with that identification number doesn't exist!");
            }
            else if (DisplacementRepository.FindOneById(displacementId) != null)
            {
                throw new Exception("Displacement with that identification number already exists!");
            }
        }



        public void DisplaceEquipment()
        {
            List<Displacement> displacements = new List<Displacement>(GetAll());
            foreach (Displacement displacement in displacements)
            {
                Equipment equipment = EquipmentRepository.FindOneById(displacement.StaticEquipmentId);

                if (displacement.DisplacementDate <= System.DateTime.Today)
                {
                    CheckConditionsAndDisplace(equipment, displacement);
                }
            }

        }

        public void CheckConditionsAndDisplace(Equipment equipment, Displacement displacement)
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

        public List<Displacement> GetAll()
        {
            return DisplacementRepository.FindAll();
        }


        public void DeleteByStartRoomId(int id)
        {
            DisplacementRepository.RemoveDisplacementByStartRoomId(id);
        }

        public void DeleteByEndRoomId(int id)
        {
            DisplacementRepository.RemoveDisplacementByEndRoomId(id);
        }

    }
}
