using Model;
using System;
using System.Collections.Generic;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.Controller
{
    public class ManagerController
    {
        private readonly ManagerService _managerService;

        public ManagerController(ManagerService managerService)
        {
            this._managerService = managerService;
        }

        public Manager? getManagerByUsername(String username)
        {
            return _managerService.GetOneByUsername(username);
        }

        public List<Manager> GetAllManager()
        {
            return _managerService.GetAllManagers();
        }

        public void CreateManager(string firstName, string lastName, string username, string password,
            string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? phoneNumber,
            string? address)
        {
            _managerService.CreateManager(firstName, lastName, username, password,
            jmbg, dateOfBirth, gender, email, phoneNumber, address);
        }

        public void DeleteManager(string jmbg)
        {
            _managerService.DeleteManager(jmbg);
        }

        public Manager? GetOneManager(string jmbg)
        {
            return _managerService.GetOneByJmbg(jmbg);
        }

        public void ModifyManager(string firstName, string lastName, DateTime? dateOfBirth, string? email, string? telephone,
        string? address, string username, string password, string jmbg)
        {
            _managerService.ModifyManager(firstName, lastName, dateOfBirth, email, telephone, address, username, password, jmbg);
        }

    }
}

