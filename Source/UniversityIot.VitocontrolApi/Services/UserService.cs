using System;
using UniversityIot.VitocontrolApi.DAL;
using UniversityIot.VitocontrolApi.Models;

namespace UniversityIot.VitocontrolApi.Services
{
    public class UserService
    {
        private readonly IUserDataService _userDataService;

        public UserService(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        public User GetUser(string userName)
        {
            User user =  _userDataService.FindUser(userName);
            if (user == null)
            {
                throw new InvalidOperationException();
            }
            return user;
        }

        public bool CreateUser(string userName)
        {
            if (_userDataService.FindUser(userName) == null)
            {
                _userDataService.AddUser(new User { UserName = userName });
                return true;
            }
            return false;
        }

        public void DeleteUser(string userName)
        {
            if (_userDataService.FindUser(userName) != null)
            {
                _userDataService.RemoveUser(userName);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}