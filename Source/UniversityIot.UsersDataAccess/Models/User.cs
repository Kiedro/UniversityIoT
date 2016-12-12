using System.ComponentModel.DataAnnotations;

namespace UniversityIot.UsersDataAccess.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [MaxLength(10)]
        public string CustomerNumber { get; set; }

        public string Password { get; set; }
    }
}