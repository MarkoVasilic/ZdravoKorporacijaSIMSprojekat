using Model;
using System;
using System.Collections.Generic;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.Controller
{
    public class ManagerController
    {
        private readonly ManagerService ManagerService;

        public ManagerController(ManagerService managerService)
        {
            this.ManagerService = managerService;
        }

        public Manager? getManagerByUsername(String username)
        {
            return ManagerService.GetOneByUsername(username);
        }

        public List<Manager> GetAllManager()
        {
            return ManagerService.GetAllManagers();
        }

        public void CreateManager(string firstName, string lastName, string username, string password,
            string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? phoneNumber,
            string? address)
        {
            ManagerService.CreateManager(firstName, lastName, username, password,
            jmbg, dateOfBirth, gender, email, phoneNumber, address);
        }

        public void DeleteManager(string jmbg)
        {
            ManagerService.DeleteManager(jmbg);
        }

        public Manager? GetOneManager(string jmbg)
        {
            return ManagerService.GetOneByJmbg(jmbg);
        }

    }
}

