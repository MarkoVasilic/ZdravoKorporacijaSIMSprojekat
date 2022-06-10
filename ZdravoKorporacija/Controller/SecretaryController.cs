using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class SecretaryController
    {
        private readonly SecretaryService _secretaryService;

        public SecretaryController(SecretaryService secretaryService)
        {
            this._secretaryService = secretaryService;
        }

        public Secretary? getSecretaryByUsername(String username)
        {
            return _secretaryService.GetOneByUsername(username);
        }

        public List<Secretary> GetAllSecretary()
        {
            return _secretaryService.GetAllSecretaries();
        }

        public void CreateSecretary(string firstName, string lastName, string username, string password,
            string jmbg, DateTime? dateOfBirth, Gender gender, string? email, string? phoneNumber,
            string? address)
        {
            _secretaryService.CreateSecretary(firstName, lastName, username, password,
            jmbg, dateOfBirth, gender, email, phoneNumber, address);
        }

        public void DeleteSecretary(string jmbg)
        {
            _secretaryService.DeleteSecretary(jmbg);
        }

        public Secretary? GetOneSecretary(string jmbg)
        {
            return _secretaryService.GetOneByJmbg(jmbg);
        }

    }
}