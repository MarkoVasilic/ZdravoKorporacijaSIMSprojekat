using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class SecretaryController
    {
        private readonly SecretaryService SecretaryService;

        public SecretaryController(SecretaryService secretaryService)
        {
            this.SecretaryService = secretaryService;
        }

        public Secretary? getSecretaryByUsername(String username)
        {
            return SecretaryService.GetOneByUsername(username);
        }

        public List<Secretary> GetAllSecretary()
        {
            return SecretaryService.GetAllSecretaries();
        }

        public void CreateSecretary(string firstName, string lastName, string username, string password,
            string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? phoneNumber,
            string? address)
        {
            SecretaryService.CreateSecretary(firstName, lastName, username, password,
            jmbg, dateOfBirth, gender, email, phoneNumber, address);
        }

        public void DeleteSecretary(string jmbg)
        {
            SecretaryService.DeleteSecretary(jmbg);
        }

        public Secretary? GetOneSecretary(string jmbg)
        {
            return SecretaryService.GetOneByJmbg(jmbg);
        }

    }
}