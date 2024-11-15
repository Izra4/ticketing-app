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

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required, Column(TypeName = "varchar(100)")]
        public string Place { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int MaxAudience {  get; set; }

        public string Note { get; set; }

        [Required]
        public bool IsPublished { get; set; } = false;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
