using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKorporacija.Model;

namespace ZdravoKorporacija.Interfaces
{
    public interface IEquipmentRepository
    {
        public List<Equipment> FindAll();

        public List<Equipment> FindAllByRoomId(int roomId);

        public void SaveEquipment(Equipment equipmentToMake);

        public Model.Equipment? FindOneById(int? id);

        public Model.Equipment? FindOneByRoomId(int? roomId);

        public Model.Equipment? FindOneByName(String name);

        public void UpdateEquipment(Equipment equipmentToModify);

        public void RemoveEquipment(int equipmentId);

        public void RemoveEquipmentByRoom(int roomId);



    }
}
