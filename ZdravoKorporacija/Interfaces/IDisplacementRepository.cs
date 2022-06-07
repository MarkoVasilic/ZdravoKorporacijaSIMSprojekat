using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKorporacija.Model;

namespace ZdravoKorporacija.Interfaces
{
    public interface IDisplacementRepository
    {
        public void SaveDisplacement(Displacement displacementToMake);

        public List<Displacement> FindAll();

        public void RemoveDisplacementByStartRoomId(int roomId);

        public void RemoveDisplacementByEndRoomId(int roomId);

        public Model.Displacement? FindOneById(int? id);
    }
}
