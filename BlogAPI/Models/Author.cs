using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [MaxLength(50)]
        public required string Name { get; set; }

        [MaxLength(50)]
        public required string Username { get; set; }

        [MaxLength(255)]
        [EmailAddress]
        public required string EMail { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
    }
}
