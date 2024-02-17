using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogAPI.Models
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }


        [MaxLength(100)]
        public required string Title { get; set; }

        [MaxLength(2500)]
        public required string Context { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [ForeignKey("AuthorId")]
        public int AuthorId { get; set; }
    }
}
