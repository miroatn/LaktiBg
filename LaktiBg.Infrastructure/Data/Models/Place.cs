using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static LaktiBg.Infrastructure.Constants.DataConstants.PlaceConstants;

namespace LaktiBg.Infrastructure.Data.Models
{
    public class Place
    {
        public Place()
        {
            Images = new List<Image>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLenght)]
        public string Name { get; set; } = string.Empty;

        public string? OwnerId { get; set; }

        [MaxLength(ContactMaxLenght)]
        public string? Contact { get; set; }

        [MaxLength(AddressMaxLenght)]
        public string? Address { get; set; }

        [Required]
        public bool IsPublic { get; set; } = false;

        public ICollection<Image> Images { get; set; }

        [Required]
        [Range(RatingMinValue, RatingMaxValue)]
        [Precision(18,2)]
        public decimal Rating { get; set; }
    }
}
