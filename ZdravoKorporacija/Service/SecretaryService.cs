using Model;
using Repository;
using System;
using System.Collections.Generic;
using ZdravoKorporacija.Interfaces;

namespace Service
{
    public class SecretaryService
    {
        private readonly ISecretaryRepository SecretaryRepository;

        public SecretaryService(ISecretaryRepository SecretaryRepository)
        {
            this.SecretaryRepository = SecretaryRepository;
        }

        public Secretary? GetOneByJmbg(String jmbg)
        {
            return SecretaryRepository.FindOneByJmbg(jmbg);

        }

        public Secretary? GetOneByUsername(String username)
        {
            return SecretaryRepository.FindOneByUsername(username);

        }

        public List<Secretary> GetAllSecretaries()
        {
            return SecretaryRepository.FindAll();
        }

        public void CreateSecretary(string firstName, string lastName, string username, string password,
            string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? telephone,
            string? address)
        {
            if (SecretaryRepository.FindOneByJmbg(jmbg) != null)
                throw new Exception("Secretary with that jmbg already exists!");
            if (SecretaryRepository.FindOneByUsername(username) != null)
                throw new Exception("Secretary with that username already exists!");

            Secretary newSecretary = new Secretary(firstName, lastName, username, password,
                jmbg, dateOfBirth, gender, email, telephone, address);
            SecretaryRepository.SaveSecretary(newSecretary);
        }

        public void DeleteSecretary(string jmbg)
        {
            if (SecretaryRepository.FindOneByJmbg(jmbg) == null)
            {
                throw new Exception("Secretary with that jmbg doesn't exist!");
            } 
            SecretaryRepository.RemoveSecretary(jmbg);
        }
    }
}