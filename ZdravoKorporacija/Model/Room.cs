using System;

namespace Model
{
    public class Room
    {
        public String Name { get; set;}
        public int Id { get; set; }
        public String Description { get; set; }
        public RoomType Type { get; set; }

        public String? typeStr { get => Enum.GetName<RoomType>(Type); set => Type = Enum.Parse<RoomType>(value); }
        public Room(String Name, int Id, String Description, RoomType Type)
        {
            this.Name = Name;
            this.Id = Id;
            this.Description = Description;
            this.Type = Type;
        }
    }
}