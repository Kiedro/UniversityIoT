using System.Data.Entity;
using UniversityIot.UsersDataAccess.Models;

namespace UniversityIot.UsersDataAccess
{
    public class UsersContext : DbContext
    {
        public UsersContext() : base("UsersContext")
        {

        }

        public IDbSet<User> Users { get; set; }
    }
}
