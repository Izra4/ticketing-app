using System.ComponentModel.DataAnnotations;
using Ticketing.Models;

namespace Ticketing.Dtos.User
{
    public class RegisterDto
    {
        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone]
        public string Phone { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public UserRole Role { get; set; }
    }
}
