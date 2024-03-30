using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaktiBg.Infrastructure.Data.Models
{
    public class UserFriends
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(UserFriend))]
        public string UserFriendId { get; set; } = string.Empty;
        public ApplicationUser UserFriend { get; set; } = null!;

        public bool IsAccepted { get; set; } = false;
    }
}
