using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        /* public void EquipmentDisplacement() //prolazim kroz premestanja i kolicinu robe u pocetnoj sobi smanjujem, a u krajnjoj povecavam
         { 
             List<Displacement> displacements = new List<Displacement>(GetAllDisplacements());
             foreach (Displacement displacement in displacements)
             {
                 if(displacement.DisplacementDate == DateTime.Now)
                 {
                         if (EquipmentRepository.FindOneByRoomId(displacement.EndRoom) == EquipmentRepository.FindOneById(displacement.StaticEquipmentId) && EquipmentRepository.FindOneByRoomId(displacement.EndRoom) != null) //ako postoji u sobi krajnjoj
                         {
                             EquipmentRepository.FindOneByRoomId(displacement.EndRoom).Quantity += displacement.StaticEquipmentQuantity;
                             EquipmentRepository.FindOneByRoomId(displacement.StartRoom).Quantity -= displacement.StaticEquipmentQuantity;

                             if(EquipmentRepository.FindOneByRoomId(displacement.StartRoom).Quantity == 0) //ako je kolicina u toj pocentoj sobi 0 brisem opremu u toj sobi
                             {

                             EquipmentRepository.RemoveEquipment(EquipmentRepository.FindOneByRoomId(displacement.StartRoom).Id);
                             }

                         }
                         else 
                         {

                             Equipment equipment = new Equipment(displacement.StaticEquipmentId, EquipmentRepository.FindOneById(displacement.StaticEquipmentId).Name, true, displacement.StaticEquipmentQuantity, displacement.EndRoom); //kreiram novu opremu u krajnjoj sobi koja ima kolicinu koju sam prosledila u promenama ako ne mogu da je pronadjem
                             EquipmentRepository.SaveEquipment(equipment);

                             EquipmentRepository.FindOneByRoomId(displacement.StartRoom).Quantity -= displacement.StaticEquipmentQuantity; //smanjujem kolicinu u pocetnoj opremi

                             if (EquipmentRepository.FindOneByRoomId(displacement.StartRoom).Quantity == 0) //ako je kolicina u toj pocentoj sobi 0 brisem opremu u toj sobi
                             {

                                 EquipmentRepository.RemoveEquipment(EquipmentRepository.FindOneByRoomId(displacement.StartRoom).Id);
                             }

                     }



                 }
             }
         }*/

        public void EquipmentDisplacement()
        {
            List<Displacement> displacementList = new List<Displacement>(GetAllDisplacements()); //samo menjam u opremi id sobe u kojoj se nalayi
            foreach (Displacement displacement in displacementList)
            {

                if (displacement.DisplacementDate == System.DateTime.Today)
                {
                    Equipment equipment = EquipmentRepository.FindOneById(displacement.StaticEquipmentId);

                    if (equipment != null)
                    {
                        int? equipmentChangeQuantityId;

                        int equipmentQuantity = equipment.Quantity;


                        if (equipmentQuantity < displacement.StaticEquipmentQuantity)
                        {
                            throw new Exception("There is not enough equipment ");

                        }
                        else if (equipmentQuantity >= displacement.StaticEquipmentQuantity)
                        {

                            //pronalazim svaki equipment u krajnjoj sobi 
                            List<Equipment> equipmentsInEndRoom = new List<Equipment>(EquipmentRepository.FindAllByRoomId(displacement.EndRoom));

                            foreach (Equipment eq in equipmentsInEndRoom)
                            {
                                Boolean change = false;
                                if (eq.Id == displacement.StaticEquipmentId)
                                {
                                    change = true;
                                    break;
                                    {

                                    }

                                    if (EquipmentRepository.FindOneById(equipmentChangeQuantityId) != null)
                                    {
                                         //povecam kolicinu u eq u start room-u

                                    }
                                    else //ako ne postoji equipment sa tim id-em kreiram novi u end room-u
                                    {
                                        Equipment newEquipment = new Equipment(displacement.StaticEquipmentId, EquipmentRepository.FindOneById(displacement.StaticEquipmentId).Name, true, displacement.StaticEquipmentQuantity, displacement.EndRoom);
                                        EquipmentRepository.SaveEquipment(newEquipment);
                                    }

                                }
                                if (change)
                                {
                                    EquipmentRepository.FindOneById(eq.Id).Quantity += displacement.StaticEquipmentQuantity; //smanjujem taj eq ciji se id poklapa sa onim u end room-u
                                    
                                }
                                else
                                {
                                    Equipment newEquipment = new Equipment(displacement.StaticEquipmentId, EquipmentRepository.FindOneById(displacement.StaticEquipmentId).Name, true, displacement.StaticEquipmentQuantity, displacement.EndRoom);
                                    EquipmentRepository.SaveEquipment(newEquipment);
                                }
                                EquipmentRepository.FindOneByRoomId(displacement.StartRoom).Quantity -= displacement.StaticEquipmentQuantity;
                                if (EquipmentRepository.FindOneByRoomId(displacement.StartRoom).Quantity == 0)
                                    EquipmentRepository.RemoveEquipment(EquipmentRepository.FindOneByRoomId(displacement.StartRoom).Id);
                              // DisplacementRepository; // izbrisi displacment koji se desio
                            }

                        }
                        else
                        {
                            throw new Exception("Equipment doesn't exist.");
                        }

                    }
                }



            }


        }





    }
}
