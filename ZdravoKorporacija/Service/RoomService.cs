using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using ZdravoKorporacija.Interfaces;

namespace Service
{
    public class RoomService
    {

        private readonly IRoomRepository RoomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            this.RoomRepository = roomRepository;
        }


        public void CreateRoom(String roomName, String roomDescription, RoomType roomType)
        {
            int roomId = GenerateNewId();
            if (RoomRepository.FindOneById(roomId) != null)
            {
                throw new Exception("Soba sa tim ID-em vec postoji!");
            }
            else if (RoomRepository.FindOneByName(roomName) != null)
            {
                throw new Exception("Prostorija sa tim nazivom vec postoji!");
            }
            else
            {
                Room newRoom = new Room(roomName, roomId, roomDescription, roomType);
                if (!newRoom.validateRoom())
                {
                    throw new Exception("Greska, prostorija nije sacuvana! Proverite da li ste popunili sva polja.");
                }
                RoomRepository.SaveRoom(newRoom);
            }
        }

        public List<Room> GetAllRooms()
        {
            return RoomRepository.FindAll();
        }

        public void DeleteRoom(int roomId)
        {
            if (RoomRepository.FindOneById(roomId) == null)
            {
                throw new Exception("Room with that identification number doesn't exist");
            }
            else
            {
                RoomRepository.RemoveRoom(roomId);
            }
        }


        public void ModifyRoom(int roomId, String roomName, String roomDescription)
        {

            if (RoomRepository.FindOneById(roomId) == null)
            {
                throw new Exception("Prostorija sa tim ID-em ne postoji");
            }
            else if (RoomRepository.FindOneByName(roomName) != null)
            {
                throw new Exception("Prostorija sa tim nazivom vec postoji.");
            }
            else
            {
                Room oldRoom = RoomRepository.FindOneById(roomId);
                Room newRoom = new Room(roomName, oldRoom.Id, roomDescription, oldRoom.Type);

                if (!newRoom.validateRoom())
                {
                    throw new Exception("Greska, prostorija nije modifikovana! Proverite da li ste popunili sva polja.");
                }

                RoomRepository.UpdateRoom(newRoom);

            }

        }

        public void ModifyRoomForRenovation(Room room)
        {
            if (RoomRepository.FindOneById(room.Id) == null)
            {
                throw new Exception("Prostorija sa tim ID-em ne postoji");
            }
            else if (RoomRepository.FindOneByName(room.Name) != null)
            {
                throw new Exception("Prostorija sa tim nazivom vec postoji.");
            }
            else
            {
                Room oldRoom = RoomRepository.FindOneById(room.Id);
                Room newRoom = new Room(room.Name, oldRoom.Id, room.Description, room.Type);

                if (!newRoom.validateRoom())
                {
                    throw new Exception("Greska, prostorija nije modifikovana! Proverite da li ste popunili sva polja.");
                }

                RoomRepository.UpdateRoom(newRoom);

            }
        }

        public Model.Room? GetRoomByName(String name)
        {
            return RoomRepository.FindOneByName(name);
        }

        public Model.Room? GetRoomByType(RoomType roomType)
        {
            return RoomRepository.FindOneByType(roomType);
        }

        public Model.Room? GetRoomById(int roomId)
        {
            return RoomRepository.FindOneById(roomId);
        }

        public int GenerateNewId()
        {
            try
            {
                List<Room> rooms = RoomRepository.FindAll();
                int currentMax = rooms.Max(obj => obj.Id);
                return currentMax + 1;
            }
            catch
            {
                return 1;
            }
        }

        public List<Room> GetAllExceptOne(int id)
        {
            List <Room> rooms = GetAllRooms();
            List<Room> resultRooms = new List<Room>();

            foreach(Room room in rooms)
            {
                if (room.Id != id)
                {
                    resultRooms.Add(room);
                }
            }

            return resultRooms;
        }

    }
}