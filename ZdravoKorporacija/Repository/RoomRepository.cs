using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using ZdravoKorporacija.Interfaces;

namespace Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly String _roomFilePath = @"..\..\..\Resources\rooms.json";

        public List<Room> FindAll()
        {
            var values = GetValues();
            return values;
        }

        public void SaveRoom(Room roomToMake)
        {
            var values = GetValues();
            values.Add(roomToMake);
            Save(values);
        }

        public void RemoveRoom(int roomId)
        {
            var values = GetValues();
            values.RemoveAll(val => val.Id == roomId);
            Save(values);
        }

        public Model.Room? FindOneById(int? id)
        {
            List<Room> rooms = GetValues();
            foreach (Room room in rooms)
            {
                if (room.Id == id)
                    return room;
            }
            return null;
        }

        public Model.Room? FindOneByName(String name)
        {
            List<Room> rooms = GetValues();
            foreach (Room room in rooms)
            {
                if (room.Name == name)
                    return room;
            }
            return null;
        }

        public void UpdateRoom(Room roomToModify)
        {
            var values = GetValues();
            values[values.FindIndex(val => val.Id == roomToModify.Id)] = roomToModify;
            Save(values);
        }


        public Model.Room? FindOneByType(RoomType roomType)
        {
            List<Room> rooms = GetValues();
            foreach (Room room in rooms)
            {
                if (room.Type == roomType)
                    return room;
            }

            return null;

        }

        private void Save(List<Room> values)
        {
            File.WriteAllText(_roomFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }

        private List<Room> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<Room>>(File.ReadAllText(_roomFilePath));

            if (values == null)
            {
                values = new List<Room>();
            }

            return values;
        }
    }
}