using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class RoomService
    {

        RoomRepository roomRepository = new RoomRepository();

        public void CreateRoom(Model.Room roomToMake)
        {
            int id = GenerateNewId();
            roomToMake.id = id;
            roomRepository.SaveRoom(roomToMake);
        }

        public List<Room> GetAllRooms()
        {
            return roomRepository.FindAll();
        }

        public void DeleteRoom(int roomId)
        {
            roomRepository.RemoveRoom(roomId);
        }


        public void ModifyRoom(Model.Room roomToModify)
        {
            roomRepository.ModifyRoom(roomToModify);
        }

        public Model.Room FindRoomByName(String name)
        {
            return roomRepository.FindOneByName(name);
        }

        public Model.Room FindRoomByType(RoomType roomType)
        {
            return roomRepository.FindOneByType(roomType);
        }

        public int GenerateNewId()
        {
            try
            {
                List<Room> rooms = roomRepository.FindAll();
                int currentMax = rooms.Max(obj => obj.id);
                return currentMax + 1;
            }
            catch
            {
                return 1;
            }
        }

    }
}