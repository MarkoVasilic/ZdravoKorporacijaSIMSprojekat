using System;

namespace ZdravoKorporacija.DTO
{
    public class EquipmentDTO
    {

        public int Id { get; set; }
        public String Name { get; set; }
        public String IsStatic { get; set; }
        public int Quantity { get; set; }
        public String RoomName { get; set; }
        public int? RoomId { get; set; }

        public EquipmentDTO()
        {

        }
        public EquipmentDTO(int id, string name, string isStatic, int quantity, string roomName, int? roomId)
        {
            Id = id;
            Name = name;
            IsStatic = isStatic;
            Quantity = quantity;
            RoomName = roomName;
            RoomId = roomId;
        }


        public void toString()
        {
            Console.WriteLine("ID = " + Id);
            Console.WriteLine("EquipmentName = " + Name);
            Console.WriteLine("isStatic = " + IsStatic);
            Console.WriteLine("Quantity = " + Quantity);
            Console.WriteLine("RoomName = " + RoomName);
            Console.WriteLine();
        }
    }
}
