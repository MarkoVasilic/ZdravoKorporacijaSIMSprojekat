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

        public Medication GetOneByName(String medicationName)
        {
            return MedicationService.GetOneByName(medicationName);
        }

        public Medication GetOneById(int medicationId)
        {
            return MedicationService.GetOneById(medicationId);
        }

        public void Create(String name, List<String> ingredients, String alternative)
        {
            MedicationService.Create(name, ingredients, alternative);
        }

        public void Modify(int id, String name, List<String> ingredients, String alternative)
        {
            MedicationService.Modify(id, name, ingredients, alternative);
        }

        public List<Medication> GetRejected()
        {
            return MedicationService.GetRejected();
        }

    }


}
