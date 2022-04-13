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

        public Model.Room? FindRoomByName(String name)
        {
            return RoomService.FindRoomByName(name);
        }

        public Model.Room? FindRoomByType(RoomType roomType)
        {
            return RoomService.FindRoomByType(roomType);
        }

    }
}