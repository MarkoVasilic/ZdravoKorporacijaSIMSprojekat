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

        public void CreateMedication(String name, List<String> ingredients)
        {
            MedicationService.CreateMedication(name, ingredients);
        }

    }


}
