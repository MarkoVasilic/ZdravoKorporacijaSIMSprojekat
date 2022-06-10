using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class MedicationController
    {
        private readonly MedicationService _medicationService;

        public MedicationController(MedicationService medicationService)
        {
            this._medicationService = medicationService;
        }

        public List<Medication> GetAll()
        {
            return _medicationService.GetAll();
        }

        public Medication GetOneByName(String name)
        {
            return _medicationService.GetOneByName(name);
        }

        public Medication GetOneById(int id)
        {
            return _medicationService.GetOneById(id);
        }

        public void Create(String name, List<String> ingredients, String alternative)
        {
            _medicationService.Create(name, ingredients, alternative);
        }

        public void Modify(int id, String name, List<String> ingredients, String alternative)
        {
            _medicationService.Modify(id, name, ingredients, alternative);
        }

        public List<Medication> GetAllRejected()
        {
            return _medicationService.GetAllRejected();
        }

        public List<Medication> GetAllUnverified()
        {
            return _medicationService.GetAllUnverified();
        }

        public List<Medication> GetAllVerified()
        {
            return _medicationService.GetAllVerified();
        }

        public void Verify(int id)
        {
            _medicationService.Verify(id);
        }

        public void Reject(int id)
        {
            _medicationService.Reject(id);
        }

    }


}
