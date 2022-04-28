﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repository;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Repository;

namespace ZdravoKorporacija.Service
{
    public class MedicationService
    {

        private readonly MedicationRepository MedicationRepository;

        public MedicationService(MedicationRepository medicationRepository)
        {
            this.MedicationRepository = medicationRepository;
        }

        public MedicationService()
        {
        }

        public List<Medication> GetAll()
        {
            return MedicationRepository.FindAll();
        }

        public Medication GetOneByName(String medicationName)
        {
            return MedicationRepository.FindOneByName(medicationName);
        }

        public Medication GetOneById(int medicationId)
        {
            return MedicationRepository.FindOneById(medicationId);
        }

        private int GenerateNewId()
        {
            try
            {
                List<Medication> medications = MedicationRepository.FindAll();
                int currentMax = medications.Max(obj => obj.Id);
                return currentMax + 1;
            }
            catch
            {
                return 1;
            }
        }

        public void CreateMedication(String name, List<String> ingredients)
        {
            int id = GenerateNewId();
            Medication medication = new Medication(id, name, ingredients);
            MedicationRepository.SaveMedication(medication);
        }


    }
}
