using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace assignment_4.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        
        public string UserId { get; set; } = string.Empty;

        public ApplicationUser? User { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(30)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Summary { get; set; } = string.Empty;

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Content { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime TimeStamp { get; set; } = DateTime.Now;
    }
}