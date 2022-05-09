using Model;
using System;
using System.Collections.Generic;
using ZdravoKorporacija.Repository;

namespace ZdravoKorporacija.Service
{
    public class ManagerService
    {
        private readonly ManagerRepository ManagerRepository;

        public ManagerService(ManagerRepository ManagerRepository)
        {
            this.ManagerRepository = ManagerRepository;
        }

        public Manager? GetOneByJmbg(String jmbg)
        {
            return ManagerRepository.FindOneByJmbg(jmbg);

        }

        public Manager? GetOneByUsername(String username)
        {
            return ManagerRepository.FindOneByUsername(username);

        }

        public List<Manager> GetAllManagers()
        {
            return ManagerRepository.FindAll();
        }

        public void CreateManager(string firstName, string lastName, string username, string password,
            string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? telephone,
            string? address)
        {
            if (ManagerRepository.FindOneByJmbg(jmbg) != null)
                throw new Exception("Manager with that jmbg already exists!");
            else if (ManagerRepository.FindOneByUsername(username) != null)
                throw new Exception("Manager with that username already exists!");
            else
            {
                Manager newManager = new Manager(firstName, lastName, username, password,
                    jmbg, dateOfBirth, gender, email, telephone, address);
                ManagerRepository.SaveManager(newManager);
            }
        }

        public void DeleteManager(string jmbg)
        {
            if (ManagerRepository.FindOneByJmbg(jmbg) == null)
            {
                throw new Exception("Manager with that jmbg doesn't exist!");
            }
            else
            {
                ManagerRepository.RemoveManager(jmbg);
            }
        }
    }
}
