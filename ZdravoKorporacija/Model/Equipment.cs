using Repository;
using System;
using System.Text.RegularExpressions;

namespace ZdravoKorporacija.Model
{
    public class Equipment
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public Boolean IsStatic { get; set; }
        public int Quantity { get; set; }
        public int? RoomId { get; set; }

        public Equipment(int id, string name, bool isStatic, int quantity, int roomId)
        {
            Id = id;
            Name = name;
            IsStatic = isStatic;
            Quantity = quantity;
            RoomId = roomId;
        }

        public Boolean validateEquipment()
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
    }
}
