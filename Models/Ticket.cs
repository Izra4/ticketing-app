using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticketing.Models
{
    public class Ticket
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required, Column(TypeName = "varchar(10)")]
        public string Unique_Number { get; set; }

        [Required]
        public DateTime Valid_Time { get; set; }

        [Required]
        public Guid User_Id { get; set; }
        public User User { get; set; }

        [Required]
        public Guid Event_Id { get; set; }
        public Event Event { get; set; }

        [Required]
        public TicketStatus Status { get; set; }
    }
}
