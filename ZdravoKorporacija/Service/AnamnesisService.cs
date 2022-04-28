using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;

namespace ZdravoKorporacija.Service
{
    public class AnamnesisService
    {
        private readonly AnamnesisRepository AnamnesisRepository;
        private readonly MedicalRecordRepository MedicalRecordRepository;

        public AnamnesisService(AnamnesisRepository anamnesisRepository, MedicalRecordRepository medicalRecordRepository)
        {
            this.AnamnesisRepository = anamnesisRepository;
            this.MedicalRecordRepository = medicalRecordRepository;
        }

        public AnamnesisService()
        {
        }

        public List<Anamnesis> GetAll()
        {
            return AnamnesisRepository.FindAll();
        }

        public Anamnesis? GetOneById(int anamnesisId)
        {
            return AnamnesisRepository.FindOneById(anamnesisId);
        }
        public List<Anamnesis>? GetAllByDoctor(String doctorJmbg)
        {
            return AnamnesisRepository.FindAllByDoctor(doctorJmbg);
        }
        private int GenerateNewId()
        {
            try
            {
                List<Anamnesis> anamneses = AnamnesisRepository.FindAll();
                int currentMax = anamneses.Max(obj => obj.Id);
                return currentMax + 1;
            }
            catch
            {
                return 1;
            }
        }

        public void CreateAnamnesis(String patientJmbg, String diagnosis, String report)
        {
            int id = GenerateNewId();
            Anamnesis anamnesis = new Anamnesis(id, diagnosis, report, DateTime.Now, App.loggedUser.Jmbg);   //doctorJmbg
            AnamnesisRepository.SaveAnamnesis(anamnesis);

            List<int> newAnamnesis = MedicalRecordRepository.FindOneByPatientJmbg(patientJmbg).AnamnesisIds;
            newAnamnesis.Add(anamnesis.Id);
            MedicalRecord oneMedicalRecord = new MedicalRecord(patientJmbg,
                MedicalRecordRepository.FindOneByPatientJmbg(patientJmbg).PrescriptionIds, newAnamnesis);

            MedicalRecordRepository.UpdateMedicalRecord(oneMedicalRecord);

        }

        public void ModifyAnamnesis(int anamnesisId, String diagnosis, String report)
        {

            var oneAnamnesis = AnamnesisRepository.FindOneById(anamnesisId);
            Anamnesis newAnamnesis = new Anamnesis(oneAnamnesis.Id, diagnosis, report, DateTime.Now, App.loggedUser.Jmbg); //vreme postaje vreme izmene,
                                                                                                                          //docotorJmbg iz ulogovanog

            AnamnesisRepository.UpdateAnamnesis(newAnamnesis);

        }
    }
}
