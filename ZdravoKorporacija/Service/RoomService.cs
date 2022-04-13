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


        public String CreateRoom(String roomName, String roomDescription, RoomType roomType)
        {
            int roomId = GenerateNewId();
            if (RoomRepository.FindOneById(roomId) != null)
            {
                return "Room with that identification number already exists!";
            }else if(RoomRepository.FindOneByName(roomName) != null)
            {
                return "Room with that name already exists!";
            }
            else
            {
                Room newRoom = new Room(roomName, roomId, roomDescription, roomType);
                if (!newRoom.validateRoom())
                {
                    return "Something went wrong, room isn't saved!";
                }
                RoomRepository.SaveRoom(newRoom);
                return "";
            }
        }

        public List<Room> GetAllRooms()
        {
            return RoomRepository.FindAll();
        }

        public String DeleteRoom(int roomId)
        {
            if (RoomRepository.FindOneById(roomId) == null)
            {
                return "Room with that identification number doesn't exists";
            }
            else
            {

                RoomRepository.RemoveRoom(roomId);
                return "";
            }
        }


        public String ModifyRoom(int roomId, String roomName, String roomDescription)
        {

            if(RoomRepository.FindOneById(roomId) == null)
            {
                return "Room with that identification number doesn't exists";
            }
            else
            {
                Room oldRoom = RoomRepository.FindOneById(roomId);
                Room newRoom = new Room(roomName, oldRoom.Id, roomDescription, oldRoom.Type);

                if (!newRoom.validateRoom())
                {
                    return "Something went wrong, room isn't changed";
                }

                RoomRepository.UpdateRoom(newRoom);
                return "";
                
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