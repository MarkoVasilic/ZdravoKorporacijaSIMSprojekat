using System;

namespace ZdravoKorporacija.Model
{
    public class Displacement
    {

        public int Id { get; set; }
        public int StartRoom { get; set; }
        public int EndRoom { get; set; }
        public int StaticEquipmentId { get; set; }
        public int StaticEquipmentQuantity { get; set; }
        public DateTime DisplacementDate { get; set; }

        public Displacement(int id, int startRoom, int endRoom, int staticEquipmentId, int staticEquipmentQuantity, DateTime displacementDate)
        {
            Id = id;
            StartRoom = startRoom;
            EndRoom = endRoom;
            StaticEquipmentId = staticEquipmentId;
            StaticEquipmentQuantity = 1;
            DisplacementDate = displacementDate;
        }



        public Boolean validate()
        {
            if (StartRoom == null)
            {
                return false;
            }
            else if (EndRoom == null)
            {
                return false;
            }
            else if (StaticEquipmentId == null)
            {
                return false;
            }
            else if (StaticEquipmentQuantity == null)
            {
                return false;
            }
            /*else if (DisplacementDate == null || DisplacementDate < DateTime.Now)
            {
                return false;
            }*/
            else
                return true;
        }


        public void toString()
        {
            Console.WriteLine("ID = " + Id);
            Console.WriteLine("StartRoom = " + StartRoom);
            Console.WriteLine("EndRoom = " + EndRoom);
            Console.WriteLine("StaticEquipment ID = " + StaticEquipmentId);
            Console.WriteLine("Equipment Quantity = " + StaticEquipmentQuantity);
            Console.WriteLine("Date = " + DisplacementDate);
        }
    }
}
