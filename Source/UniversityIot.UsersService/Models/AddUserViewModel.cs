namespace UniversityIot.UsersService.Models
{
    using System.ComponentModel.DataAnnotations;

    public class AddUserViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(10)]
        [MinLength(10)]
        public string CustomerNumber { get; set; }

        [Required]
        public string Password { get; set; }
    }
}