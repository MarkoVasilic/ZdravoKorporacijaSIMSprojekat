using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class RoomController
    {
        private readonly RoomService RoomService;

        public RoomController(RoomService roomService)
        {
            this.RoomService = roomService;
        }

        public void CreateRoom(String roomName, String roomDescription, RoomType roomType)
        {
            RoomService.CreateRoom(roomName, roomDescription, roomType);
        }

        public List<Room> GetAllRooms()
        {
            return RoomService.GetAllRooms();
        }

        public void DeleteRoom(int id)
        {
            RoomService.DeleteRoom(id);
        }

        public void ModifyRoom(int roomId, String roomName, String roomDescription)
        {

            RoomService.ModifyRoom(roomId, roomName, roomDescription);
        }

        public Model.Room? GetRoomByName(String name)
        {
            return RoomService.GetRoomByName(name);
        }

        public Model.Room? GetRoomByType(RoomType roomType)
        {
            return RoomService.GetRoomByType(roomType);
        }

        public Model.Room? GetRoomById(int roomId)
        {
            return RoomService.GetRoomById(roomId);
        }

    }
}