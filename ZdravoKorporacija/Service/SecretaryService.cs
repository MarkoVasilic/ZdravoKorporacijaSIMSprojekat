using Model;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
    public class SecretaryService
    {
        private readonly SecretaryRepository SecretaryRepository;

        public SecretaryService(SecretaryRepository SecretaryRepository)
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
            else if (SecretaryRepository.FindOneByUsername(username) != null)
                throw new Exception("Secretary with that username already exists!");
            else
            {
                Secretary newSecretary = new Secretary(firstName, lastName, username, password,
                    jmbg, dateOfBirth, gender, email, telephone, address);
                SecretaryRepository.SaveSecretary(newSecretary);
            }
        }

        public void DeleteSecretary(string jmbg)
        {
            if (SecretaryRepository.FindOneByJmbg(jmbg) == null)
            {
                throw new Exception("Secretary with that jmbg doesn't exist!");
            }
            else
            {
                SecretaryRepository.RemoveSecretary(jmbg);
            }
        }
    }
}