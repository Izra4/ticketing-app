using System.ComponentModel.DataAnnotations;

namespace Ticketing.Dtos.Event
{
    public class PostDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Time{ get; set; }

        [Required]
        public string Place { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int Max_Audience { get; set; }

        public string Note { get; set; }
    }
}
