using System;
using System.Text.RegularExpressions;

namespace Model
{
    public class Room
    {
        public String Name { get; set; }
        public int Id { get; set; }
        public String Description { get; set; }
        public RoomType Type { get; set; }

        public Room() { }
        public String? typeStr { get => Enum.GetName<RoomType>(Type); set => Type = Enum.Parse<RoomType>(value); }
        public Room(String Name, int Id, String Description, RoomType Type)
        {
            this.Name = Name;
            this.Id = Id;
            this.Description = Description;
            this.Type = Type;
        }

        public Boolean validateRoom()
        {
            Regex nameRegex = new Regex("^$|[a-zA-Z]+[a-zA-Z0-9_\\.\\s]*$");

            if (Name == null || Name.Length < 2 || !nameRegex.IsMatch(Name))
            {
                return false;
            }
            else if (Description == null || Description.Length < 5)
            {
                return false;
            }
            else if (Type == null || Type == RoomType.NONE)
            {
                return false;
            }
            else return true;
        }


        public void toString()
        {
            Console.WriteLine("ID = " + Id);
            Console.WriteLine("RoomsName = " + Name);
            Console.WriteLine("RoomDescription" +Description);
            Console.WriteLine("RoomType = " + Type);
            Console.WriteLine();
        }
    }
}