using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticketing.Models
{
    public class User
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required, Column(TypeName = "varchar(200)")]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone, Column(TypeName = "varchar(15)")]
        public string Phone { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Password { get; set; }

        [Required]
        public UserRole Role { get; set; } = UserRole.User;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
