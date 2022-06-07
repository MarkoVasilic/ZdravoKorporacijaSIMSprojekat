using System;
using System.Text.RegularExpressions;

namespace ZdravoKorporacija.Model
{
    public class Equipment
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public Boolean IsStatic { get; set; }
        public int? Quantity { get; set; }
        public int? RoomId { get; set; }

        public DateTime? DynamicAddDate { get; set; }

        public Equipment(int id, String name, bool isStatic, int? quantity, int? roomId, DateTime? dynamicAddDate)
        {
            Id = id;
            Name = name;
            IsStatic = isStatic;
            Quantity = quantity;
            RoomId = roomId;
            DynamicAddDate = dynamicAddDate;
        }

        public Boolean validate()
        {
            Regex nameRegex = new Regex("^$|[a-zA-Z]+[a-zA-Z0-9_\\.\\s]*$");

            if (Name == null || Name.Length < 3 || !nameRegex.IsMatch(Name))
            {
                return false;
            }
            else if (Quantity == null || Quantity == 0)
            {
                return false;
            }
            else if (IsStatic == null)
            {
                return false;
            }
            else return true;
        }


        public void toString()
        {
            Console.WriteLine("ID = " + Id);
            Console.WriteLine("EquipmentName = " + Name);
            Console.WriteLine("isStatic = " + IsStatic);
            Console.WriteLine("Quantity = " + Quantity);
            Console.WriteLine("RoomId = " + RoomId);
            Console.WriteLine();
        }

    }
}
