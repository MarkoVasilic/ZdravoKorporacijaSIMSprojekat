using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
    public class RoomRepository
    {
        private readonly String RoomFilePath = @"..\..\..\Resources\rooms.json";

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

        public void Save(List<Room> values)
        {
            File.WriteAllText(RoomFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }

        public List<Room> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<Room>>(File.ReadAllText(RoomFilePath));

            if (values == null)
            {
                values = new List<Room>();
            }

            return values;
        }
    }
}