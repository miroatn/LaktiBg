using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaktiBg.Infrastructure.Data.Models
{
    public class UsersEvents
    {
        [ForeignKey(nameof(User))]
        [Required]
        public string UserId { get; set; } = string.Empty;

        public ApplicationUser User { get; set; } = null!;

        [ForeignKey(nameof(Event))]
        [Required]
        public int EventId { get; set; }

        public Event Event { get; set; } = null!;
    }
}
