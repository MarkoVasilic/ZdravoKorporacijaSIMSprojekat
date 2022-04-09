using System;

namespace Model
{
    public class Room
    {
        public String name { get; set;}
        public int id { get; set; }
        public String description { get; set; }
        public RoomType type { get; set; }

        public Room(string name, int id, string description, RoomType type)
        {
            this.name = name;
            this.id = id;
            this.description = description;
            this.type = type;
        }
    }
}