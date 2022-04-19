using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class RoomService
    {

        private readonly RoomRepository RoomRepository;

        public RoomService(RoomRepository roomRepository)
        {
            this.RoomRepository = roomRepository;
        }


        public void CreateRoom(String roomName, String roomDescription, RoomType roomType)
        {
            int roomId = GenerateNewId();
            if (RoomRepository.FindOneById(roomId) != null)
            {
                throw new Exception("Room with that identification number already exists!");
            }
            else if (RoomRepository.FindOneByName(roomName) != null)
            {
                throw new Exception("Room with that name already exists!");
            }
            else
            {
                Room newRoom = new Room(roomName, roomId, roomDescription, roomType);
                if (!newRoom.validateRoom())
                {
                    throw new Exception("Something went wrong, room isn't saved!");
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
                throw new Exception("Room with that identification number already exists!");
            }
            else
            {
                Room oldRoom = RoomRepository.FindOneById(roomId);
                Room newRoom = new Room(roomName, oldRoom.Id, roomDescription, oldRoom.Type);

                if (!newRoom.validateRoom())
                {
                    throw new Exception("Something went wrong, room isn't changed!");
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

    }
}