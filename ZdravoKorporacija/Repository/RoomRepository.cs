using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Repository
{
    public class RoomRepository
    {
        private String roomFilePath = @"..\..\..\Resources\rooms.json";

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
            values.RemoveAll(val => val.id == roomId);
            Save(values);
        }

        public Model.Room? FindOneByName(String name)
        {
            List<Room> rooms = GetValues();
            foreach (Room room in rooms)
            {
                if (room.name == name)
                    return room;
            }
            return null;
        }

        public void ModifyRoom(Room roomToModify)
        {
            var values = GetValues();
            values[values.FindIndex(val => val.id == roomToModify.id)] = roomToModify;
            Save(values);
        }

        public Model.Room? FindOneByType(RoomType roomType)
        {
            List<Room> rooms = GetValues();
            foreach (Room room in rooms)
            {
                if (room.type == roomType)
                    return room;
            }

            return null;

        }

        public void Save(List<Room> values)
        {
            File.WriteAllText(roomFilePath, JsonConvert.SerializeObject(values, Formatting.Indented));
        }

        public List<Room> GetValues()
        {
            var values = JsonConvert.DeserializeObject<List<Room>>(File.ReadAllText(roomFilePath));

            if (values == null)
            {
                values = new List<Room>();
            }

            return values;
        }
    }
}