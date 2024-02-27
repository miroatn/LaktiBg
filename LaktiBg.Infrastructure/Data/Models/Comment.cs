using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static LaktiBg.Infrastructure.Constants.DataConstants.CommentConstants;

namespace LaktiBg.Infrastructure.Data.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TextMaxLenght)]
        public string Text { get; set; } = string.Empty;

        [Required]
        [ForeignKey(nameof(Author))]
        public string AuthorId { get; set; } = string.Empty;

        public IdentityUser Author { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Event))]
        public int EventId { get; set; }

        public Event Event { get; set; } = null!;
    }
}
