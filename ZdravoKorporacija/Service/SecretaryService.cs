using Model;
using Repository;
using System;
using System.Collections.Generic;
using ZdravoKorporacija.Interfaces;

namespace Service
{
    public class SecretaryService
    {
        private readonly ISecretaryRepository _secretaryRepository;

        public SecretaryService(ISecretaryRepository SecretaryRepository)
        {
            this._secretaryRepository = SecretaryRepository;
        }

        public Secretary? GetOneByJmbg(String jmbg)
        {
            return _secretaryRepository.FindOneByJmbg(jmbg);

        }

        public Secretary? GetOneByUsername(String username)
        {
            return _secretaryRepository.FindOneByUsername(username);

        }

        public List<Secretary> GetAllSecretaries()
        {
            return _secretaryRepository.FindAll();
        }

        public void CreateSecretary(string firstName, string lastName, string username, string password,
            string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? telephone,
            string? address)
        {
            if (_secretaryRepository.FindOneByJmbg(jmbg) != null)
                throw new Exception("Secretary with that jmbg already exists!");
            if (_secretaryRepository.FindOneByUsername(username) != null)
                throw new Exception("Secretary with that username already exists!");

            Secretary newSecretary = new Secretary(firstName, lastName, username, password,
                jmbg, dateOfBirth, gender, email, telephone, address);
            _secretaryRepository.SaveSecretary(newSecretary);
        }

        public void DeleteSecretary(string jmbg)
        {
            if (_secretaryRepository.FindOneByJmbg(jmbg) == null)
            {
                throw new Exception("Secretary with that jmbg doesn't exist!");
            }
            _secretaryRepository.RemoveSecretary(jmbg);
        }
    }
}