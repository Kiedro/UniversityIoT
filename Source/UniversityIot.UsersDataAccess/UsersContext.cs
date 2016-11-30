using System.Data.Entity;
using UniversityIot.UsersDataAccess.Models;

namespace UniversityIot.UsersDataAccess
{
    public class UsersContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
