using System;

namespace Model
{
    public class Room
    {
        public String name { get => name; set => name = value; }
        public int id { get => id; set => id = value; }
        public String description { get => description; set => description = value; }
        public RoomType type { get => type; set => type = value; }

    }
}