using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static LaktiBg.Infrastructure.Constants.DataConstants.UserConstants;

namespace LaktiBg.Infrastructure.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Friends = new List<UserFriends>();
            FinishedEvents = new List<UsersEvents>();
        }

        [Required]
        [MaxLength(FirstNameMaxLenght)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(LastNameMaxLenght)]
        public string LastName { get; set; } = string.Empty;

        public DateTime? BirthDate { get; set; }

        [Range(RatingMinValue, RatingMaxValue)]
        [Precision(18, 2)]
        [Required]
        public decimal Rating { get; set; }

        public ICollection<UserFriends> Friends { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }

        public ICollection<UsersEvents> FinishedEvents { get; set; }

        public string? Address { get; set; }

        [MaxLength(DescriptionMaxLenght)]
        public string? Description { get; set; }

        public Image? Avatar { get; set; }
    }
}
