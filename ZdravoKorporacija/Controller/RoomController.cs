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

        public String CreateRoom(String roomName, String roomDescription, RoomType roomType)
        {
            return RoomService.CreateRoom(roomName, roomDescription, roomType);
        }

        public List<Room> GetAllRooms()
        {
            return RoomService.GetAllRooms();
        }

        public String DeleteRoom(int id)
        {
            return RoomService.DeleteRoom(id);
        }

        public String ModifyRoom(int roomId, String roomName, String roomDescription)
        {

            return RoomService.ModifyRoom(roomId, roomName, roomDescription);
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