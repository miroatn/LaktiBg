using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaktiBg.Infrastructure.Data.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = string.Empty;

        public ApplicationUser User { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Place))]
        public int PlaceId { get; set; }

        public Place Place { get; set; } = null!;

        [Required]
        public string Url { get; set; } = string.Empty;
    }
}
