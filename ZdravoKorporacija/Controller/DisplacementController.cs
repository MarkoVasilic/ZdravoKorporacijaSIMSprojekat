using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.Controller
{
    public class DisplacementController
    {
        private readonly DisplacementService DisplacementService;

        public DisplacementController(DisplacementService displacementService)
        {
            DisplacementService = displacementService;
        }

        public void Create(int startRoom, int endRoom, int equiomentId, DateTime displacementDate)
        {
            DisplacementService.Create(startRoom, endRoom, equiomentId, displacementDate);
        }

        public List<Displacement> GetAll()
        {
            return DisplacementService.GetAll();
        }

        public void DisplaceEquipment()
        {
            DisplacementService.DisplaceEquipment();
        }



    }
}
