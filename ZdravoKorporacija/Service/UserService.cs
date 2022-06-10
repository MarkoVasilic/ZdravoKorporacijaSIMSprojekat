using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using ZdravoKorporacija.Interfaces;

namespace ZdravoKorporacija.Service
{
    public class UserService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IManagerRepository _managerRepository;
        private readonly ISecretaryRepository _secretaryRepository;

        public UserService(IDoctorRepository doctorRepository,
            IManagerRepository managerRepository, ISecretaryRepository secretaryRepository)
        {
            this._doctorRepository = doctorRepository;
            this._managerRepository = managerRepository;
            this._secretaryRepository = secretaryRepository;
        }

        public User? CheckUserJmbgExistence(string userJmbg)
        {
            User user = _doctorRepository.FindOneByJmbg(userJmbg);
            if (user == null)
                user = _managerRepository.FindOneByJmbg(userJmbg);
            if (user == null)
                user = _secretaryRepository.FindOneByJmbg(userJmbg);
            if (user == null)
                throw new Exception("Something went wrong!");
            return user;
        }

        public List<String> CreateFullNamesOfUser(List<String> userJmbgs)
        {
            List<String> userFullNames = new List<string>();
            foreach (var userJmbg in userJmbgs)
            {
                User user = _doctorRepository.FindOneByJmbg(userJmbg);
                if (user == null)
                    user = _managerRepository.FindOneByJmbg(userJmbg);
                if (user == null)
                    user = _secretaryRepository.FindOneByJmbg(userJmbg);
                userFullNames.Add(user.FirstName + " " + user.LastName);
            }

            return userFullNames;
        }
    }
}
