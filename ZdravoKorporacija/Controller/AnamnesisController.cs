using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.Controller
{
    public class AnamnesisController
    {
        private readonly AnamnesisService AnamnesisService;

        public AnamnesisController(AnamnesisService anamnesisService)
        {
            this.AnamnesisService = anamnesisService;
        }

        public List<Anamnesis> GetAll()
        {
            return AnamnesisService.GetAll();
        }

        public Anamnesis? GetOneById(int anamnesisId)
        {
            return AnamnesisService.GetOneById(anamnesisId);
        }
        public List<Anamnesis>? GetAllByDoctor(String doctorJmbg)
        {
            return AnamnesisService.GetAllByDoctor(doctorJmbg);
        }

        public void CreateAnamnesis(String patientJmbg, String diagnosis, String report)
        {
            AnamnesisService.CreateAnamnesis(patientJmbg, diagnosis, report);
        }

        public void ModifyAnamnesis(int anamnesisId, String diagnosis, String report)
        {
            AnamnesisService.ModifyAnamnesis(anamnesisId, diagnosis, report);
        }
    }
}
