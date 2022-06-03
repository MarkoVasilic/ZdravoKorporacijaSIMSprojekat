using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKorporacija.Model;

namespace ZdravoKorporacija.Interfaces
{
    public interface IBasicRenovationRepository
    {
        public List<BasicRenovation> FindAll();

        public List<BasicRenovation> FindAllByRoomId(int id);

        public void SaveBasicRenovation(BasicRenovation renovationToMake);

        public void RemoveBasicRenovation(int renovationId);

        public void RemoveBasicRenovationByRoomId(int roomId);

        public BasicRenovation? FindOneById(int? id);
    }
}
