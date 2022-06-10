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
        private readonly IDoctorRepository doctorRepository;
        private readonly IManagerRepository managerRepository;
        private readonly ISecretaryRepository secretaryRepository;

        public UserService(IDoctorRepository doctorRepository,
            IManagerRepository managerRepository, ISecretaryRepository secretaryRepository)
        {
            this.doctorRepository = doctorRepository;
            this.managerRepository = managerRepository;
            this.secretaryRepository = secretaryRepository;
        }

        public User? CheckUserJmbgExistence(string userJmbg)
        {
            User user = doctorRepository.FindOneByJmbg(userJmbg);
            if (user == null)
                user = managerRepository.FindOneByJmbg(userJmbg);
            if (user == null)
                user = secretaryRepository.FindOneByJmbg(userJmbg);
            if (user == null)
                throw new Exception("Something went wrong!");
            return user;
        }

        public List<String> CreateFullNamesOfUser(List<String> userJmbgs)
        {
            List<String> userFullNames = new List<string>();
            foreach (var userJmbg in userJmbgs)
            {
                User user = doctorRepository.FindOneByJmbg(userJmbg);
                if (user == null)
                    user = managerRepository.FindOneByJmbg(userJmbg);
                if (user == null)
                    user = secretaryRepository.FindOneByJmbg(userJmbg);
                userFullNames.Add(user.FirstName + " " + user.LastName);
            }

            return userFullNames;
        }
    }
}
