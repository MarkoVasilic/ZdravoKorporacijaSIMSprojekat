using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKorporacija.Repository;

namespace ZdravoKorporacija.Service
{
    public class EquipmentService
    {


        private readonly EquipmentRepository EquipmentRepository;

        public EquipmentService(EquipmentRepository equipmentRepository)
        {
            this.EquipmentRepository = equipmentRepository;
        }




    }
}
