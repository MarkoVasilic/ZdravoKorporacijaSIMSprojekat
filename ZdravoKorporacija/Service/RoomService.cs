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

            try 
            {
                int roomId = GenerateNewId();

                Room room = new Room(roomName, roomId, roomDescription, roomType);

                RoomRepository.SaveRoom(room);

            } catch (Exception e) when (roomName == "" || roomDescription == "" || roomType == null || RoomRepository.FindOneByName(roomName) != null)
            {
                Console.WriteLine("Proverite parametre za unos sobe!");

            }
            

        }

        public List<Room> GetAllRooms()
        {
            return RoomRepository.FindAll();
        }

        public void DeleteRoom(int roomId)
        {
            RoomRepository.RemoveRoom(roomId);
        }


        public void ModifyRoom(int roomId, String roomName, String roomDescription)
        {
            Room oldRoom = RoomRepository.FindOneById(roomId);
            if(oldRoom != null)
            {
                Room newRoom = new Room(roomName, oldRoom.Id, roomDescription, oldRoom.Type);
                RoomRepository.ModifyRoom(newRoom);
            }
        }

        public Model.Room? FindRoomByName(String name)
        {
            return RoomRepository.FindOneByName(name);
        }

        public Model.Room? FindRoomByType(RoomType roomType)
        {
            return RoomRepository.FindOneByType(roomType);
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