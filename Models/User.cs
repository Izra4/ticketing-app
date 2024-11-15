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

        [Required, EmailAddress, Column(TypeName = "varchar(255)")]
        public string Email { get; set; }

        [Required, Phone, Column(TypeName = "varchar(15)")]
        public string Phone { get; set; }

        [Required]
        [Column(TypeName = "varchar(200)")]
        public string Password { get; set; }

        [Required]
        public UserRole Role { get; set; } = UserRole.User;

        [Required]
        public DateTime Created_At { get; set; } = DateTime.Now;

        public ICollection<Ticket> Tickets { get; set; }
    }
}
