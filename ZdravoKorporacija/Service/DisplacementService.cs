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

        private readonly IDisplacementRepository _displacementRepository;
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IRoomRepository _roomRepository;

        public DisplacementService(IDisplacementRepository displacementRepository, IEquipmentRepository equipmentRepository, IRoomRepository roomRepository)
        {
            this._displacementRepository = displacementRepository;
            this._equipmentRepository = equipmentRepository;
            this._roomRepository = roomRepository;
        }

        public int GenerateNewId()
        {
            try
            {
                List<Displacement> displacements = _displacementRepository.FindAll();
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
            if (_equipmentRepository.FindOneById(equiomentId).IsStatic == true)
            {
                int displacementId = GenerateNewId();
                CheckBeforeCreating(startRoom, endRoom, equiomentId, displacementDate, displacementId);
                Displacement displacement = new Displacement(displacementId, startRoom, endRoom, equiomentId, 1, displacementDate);
                Validate(displacement);
                _displacementRepository.SaveDisplacement(displacement);

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
            if (_roomRepository.FindOneById(startRoom) == null)
            {

                throw new Exception("Room with that identification number doesn't exist!");
            }
            else if (_roomRepository.FindOneById(endRoom) == null)
            {

                throw new Exception("Room with that identification number doesn't exist!");
            }
            else if (_equipmentRepository.FindOneById(equipmentId) == null)
            {
                throw new Exception("Equipment with that identification number doesn't exist!");
            }
            else if (_displacementRepository.FindOneById(displacementId) != null)
            {
                throw new Exception("Displacement with that identification number already exists!");
            }
        }



        public void DisplaceEquipment()
        {
            List<Displacement> displacements = new List<Displacement>(GetAll());
            foreach (Displacement displacement in displacements)
            {
                Equipment equipment = _equipmentRepository.FindOneById(displacement.StaticEquipmentId);

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

            if (_equipmentRepository.FindOneByRoomId(displacement.EndRoom) == null) //da mi se ne bi svaki put upisivala 
            {
                _equipmentRepository.SaveEquipment(equipment);
            }
            _equipmentRepository.RemoveEquipmentByRoom(displacement.StartRoom);
        }

        public List<Displacement> GetAll()
        {
            return _displacementRepository.FindAll();
        }


        public void DeleteByStartRoomId(int id)
        {
            _displacementRepository.RemoveDisplacementByStartRoomId(id);
        }

        public void DeleteByEndRoomId(int id)
        {
            _displacementRepository.RemoveDisplacementByEndRoomId(id);
        }

    }
}
