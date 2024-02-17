using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogAPI.Models
{
    public class AuthorArticle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [ForeignKey("AuthorId")]
        public int AuthorId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [ForeignKey("ArticleId")]
        public int ArticleId { get; set; }
    }
}
