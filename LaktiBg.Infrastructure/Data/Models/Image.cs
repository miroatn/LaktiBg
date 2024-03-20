using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaktiBg.Infrastructure.Data.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        public byte[] Bytes { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public string FileExtension { get; set; } = string.Empty;
        
        public decimal Size { get; set; }

        [ForeignKey(nameof(User))]
        public string? UserId { get; set; } 

        public ApplicationUser User { get; set; } = null!;

        [ForeignKey(nameof(Place))]
        public int? PlaceId { get; set; }

        public Place Place { get; set; } = null!;

        [ForeignKey(nameof(Event))]
        public int? EventId { get; set; }

        public Event Event { get; set; } = null!;

    }
}
