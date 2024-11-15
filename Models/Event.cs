using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticketing.Models
{
    public class Event
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required, Column(TypeName = "varchar(200)")]
        public string Name { get; set; }

        [Required, Column(TypeName = "text")]
        public string Description { get; set; }

        [Required]
        public DateTime time { get; set; }

        [Required, Column(TypeName = "varchar(100)")]
        public string Place { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int Max_Audience {  get; set; }

        [Column(TypeName = "text")]
        public string Note { get; set; }

        [Required]
        public bool Is_Published { get; set; } = false;

        [Required]
        public DateTime Created_At { get; set; } = DateTime.Now;

        public ICollection<Ticket> Tickets { get; set; }
    }
}
