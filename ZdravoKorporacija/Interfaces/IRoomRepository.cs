using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoKorporacija.Interfaces
{
    public interface IRoomRepository
    {
        public List<Room> FindAll();

        public void SaveRoom(Room roomToMake);

        public void RemoveRoom(int roomId);

        public Room? FindOneById(int? id);

        public Room? FindOneByName(String name);

        public void UpdateRoom(Room roomToModify);

        public Room? FindOneByType(RoomType roomType);











    }
}
