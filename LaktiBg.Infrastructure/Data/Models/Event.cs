using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static LaktiBg.Infrastructure.Constants.DataConstants.EventConstants;

namespace LaktiBg.Infrastructure.Data.Models
{
    public class Event
    {
        public Event()
        {
            Participants = new List<UsersEvents>();
            Comments = new List<Comment>();
            Types = new List<EventTypeConnection>();
            Images = new List<Image>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLenght)]
        public string Name { get; set; } = string.Empty;


        [Required]
        public IList<EventTypeConnection> Types { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        [ForeignKey(nameof(Place))] 
        public int PlaceId { get; set; }

        [Required]
        public Place Place { get; set; } = null!;

        [Required]
        public bool IsPublic { get; set; } = false;

        [Required]
        public bool IsDeleted { get; set; } = false;

        [Required]
        public bool IsVisible { get; set; } = false;

        [Required]
        public bool IsFinished { get; set; } = false;

        [Required]
        public string OrganizerId { get; set; } = string.Empty;

        public int? MinRatingRequired { get; set; }

        public int? ParticipantsMaxCount { get; set; }

        public int? MinAgeRequired { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLenght)]
        public string Description { get; set; } = string.Empty;

        public IList<UsersEvents> Participants { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public IEnumerable<Image> Images { get; set; }


    }
}
