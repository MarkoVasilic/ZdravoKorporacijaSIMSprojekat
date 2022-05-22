using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class MedicationController
    {
        private readonly MedicationService MedicationService;

        public MedicationController(MedicationService medicationService)
        {
            this.MedicationService = medicationService;
        }

        public List<Medication> GetAll()
        {
            return MedicationService.GetAll();
        }

        public Medication GetOneByName(String name)
        {
            return MedicationService.GetOneByName(name);
        }

        public Medication GetOneById(int id)
        {
            return MedicationService.GetOneById(id);
        }

        public void Create(String name, List<String> ingredients, String alternative)
        {
            MedicationService.Create(name, ingredients, alternative);
        }

        public void Modify(int id, String name, List<String> ingredients, String alternative)
        {
            MedicationService.Modify(id, name, ingredients, alternative);
        }

        public List<Medication> GetAllRejected()
        {
            return MedicationService.GetAllRejected();
        }

        public List<Medication> GetAllUnverified()
        {
            return MedicationService.GetAllUnverified();
        }

        public void Verify(int id)
        {
            MedicationService.Verify(id);
        }

        public void Reject(int id)
        {
            MedicationService.Reject(id);
        }

    }


}
