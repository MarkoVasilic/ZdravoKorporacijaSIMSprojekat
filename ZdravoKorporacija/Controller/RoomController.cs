using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class RoomController
    {
        readonly RoomService roomService;

        public RoomController(RoomService roomService)
        {
            this.roomService = roomService;
        }

        public void CreateRoom(Model.Room roomToMake)
        {
            roomService.CreateRoom(roomToMake);
        }

        public List<Room> GetAllRooms()
        {
            return roomService.GetAllRooms();
        }

        public void DeleteRoom(int id)
        {
            roomService.DeleteRoom(id);
        }

        public void ModifyRoom(Model.Room roomToModify)
        {
            roomService.ModifyRoom(roomToModify);
        }

        public Model.Room? FindRoomByName(String name)
        {
            return roomService.FindRoomByName(name);
        }

        public Model.Room? FindRoomByType(RoomType roomType)
        {
            return roomService.FindRoomByType(roomType);
        }

    }
}