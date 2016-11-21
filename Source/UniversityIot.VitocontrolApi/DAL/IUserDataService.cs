using System.Collections.Generic;
using UniversityIot.VitocontrolApi.DAL;
using UniversityIot.VitocontrolApi.Models;

namespace UniversityIot.VitocontrolApi.DAL
{
    public interface IUserDataService
    {
        //IList<User> Users { get; set; }
        
        void AddUser(User user);
        void RemoveUser(string userName);
        User FindUser(string userName);
    }
}

class userDAtaService : IUserDataService
{
    public void AddUser(User user)
    {
        throw new System.NotImplementedException();
    }

    public void RemoveUser(string userName)
    {
        throw new System.NotImplementedException();
    }

    public User FindUser(string userName)
    {
        throw new System.NotImplementedException();
    }
}