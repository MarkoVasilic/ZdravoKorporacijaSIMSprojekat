using System;

namespace ZdravoKorporacija.DTO
{
    public class EquipmentDTO
    {

        public int Id { get; set; }
        public String Name { get; set; }
        public String IsStatic { get; set; }
        public int? Quantity { get; set; }
        public String RoomName { get; set; }
        public int? RoomId { get; set; }

        public EquipmentDTO()
        {

        }
        public EquipmentDTO(int id, string name, string isStatic, int? quantity, string roomName, int? roomId)
        {
            Id = id;
            Name = name;
            IsStatic = isStatic;
            Quantity = quantity;
            RoomName = roomName;
            RoomId = roomId;
        }


        public String toString()
        {

            String txt = "";
            txt += "Equipment name:" + Name + "\n";
            txt += "Type:" + IsStatic + "\n";
            txt += "Quantity:" + Quantity + "\n";
            txt += "Room:" + RoomName + "\n";
            txt += "\n";
            return txt;
        }
    }
}
