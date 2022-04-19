using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.Controller
{
    public class EquipmentController
    {

        private readonly EquipmentService EquipmentService;

        public EquipmentController(EquipmentService equipmentService)
        {
            this.EquipmentService = equipmentService;
        }


    }
}
