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
        private readonly DisplacementService _displacementService;

        public DisplacementController(DisplacementService displacementService)
        {
            _displacementService = displacementService;
        }

        public void Create(int startRoom, int endRoom, int equiomentId, DateTime displacementDate)
        {
            _displacementService.Create(startRoom, endRoom, equiomentId, displacementDate);
        }

        public List<Displacement> GetAll()
        {
            return _displacementService.GetAll();
        }

        public void DisplaceEquipment()
        {
            _displacementService.DisplaceEquipment();
        }



    }
}
