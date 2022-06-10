using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class RoomController
    {
        private readonly RoomService _roomService;

        public RoomController(RoomService roomService)
        {
            this._roomService = roomService;
        }

        public void CreateRoom(String roomName, String roomDescription, RoomType roomType)
        {
            _roomService.CreateRoom(roomName, roomDescription, roomType);
        }

        public List<Room> GetAllRooms()
        {
            return _roomService.GetAllRooms();
        }

        public void DeleteRoom(int id)
        {
            _roomService.DeleteRoom(id);
        }

        public void ModifyRoom(int roomId, String roomName, String roomDescription)
        {

            _roomService.ModifyRoom(roomId, roomName, roomDescription);
        }

        public Model.Room? GetRoomByName(String name)
        {
            return _roomService.GetRoomByName(name);
        }

        public Model.Room? GetRoomByType(RoomType roomType)
        {
            return _roomService.GetRoomByType(roomType);
        }

        public Model.Room? GetRoomById(int roomId)
        {
            return _roomService.GetRoomById(roomId);
        }

        public List<Room> GetAllExceptOne(int id)
        {
            return _roomService.GetAllExceptOne(id);
        }

    }
}