using Model;
using System;
using System.Collections.Generic;
using ZdravoKorporacija.Interfaces;
using ZdravoKorporacija.Repository;

namespace ZdravoKorporacija.Service
{
    public class ManagerService
    {
        private readonly IManagerRepository _managerRepository;

        public ManagerService(IManagerRepository ManagerRepository)
        {
            this._managerRepository = ManagerRepository;
        }

        public Manager? GetOneByJmbg(String jmbg)
        {
            return _managerRepository.FindOneByJmbg(jmbg);

        }

        public Manager? GetOneByUsername(String username)
        {
            return _managerRepository.FindOneByUsername(username);

        }

        public List<Manager> GetAllManagers()
        {
            return _managerRepository.FindAll();
        }

        public void CreateManager(string firstName, string lastName, string username, string password,
            string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? telephone,
            string? address)
        {
            if (_managerRepository.FindOneByJmbg(jmbg) != null)
                throw new Exception("Manager with that jmbg already exists!");
            else if (_managerRepository.FindOneByUsername(username) != null)
                throw new Exception("Manager with that username already exists!");
            else
            {
                Manager newManager = new Manager(firstName, lastName, username, password,
                    jmbg, dateOfBirth, gender, email, telephone, address);
                _managerRepository.SaveManager(newManager);
            }
        }

        public void DeleteManager(string jmbg)
        {
            if (_managerRepository.FindOneByJmbg(jmbg) == null)
            {
                throw new Exception("Manager with that jmbg doesn't exist!");
            }
            else
            {
                _managerRepository.RemoveManager(jmbg);
            }
        }


        public void ModifyManager(string firstName, string lastName, DateTime? dateOfBirth, string? email, string? telephone,
        string? address, string username, string password, string jmbg)
        {
            if (_managerRepository.FindOneByUsername(username) == null)
            {
                throw new Exception("Manager with that username does not exist");
            }
            else
            {
                Manager oldManager = _managerRepository.FindOneByJmbg(jmbg);
                Manager newManager = new Manager(firstName, lastName, username, password, oldManager.Jmbg, dateOfBirth, oldManager.Gender, email, telephone, address);

                _managerRepository.UpdateManager(newManager);

            }

        }
    }
}
