using LaktiBg.Infrastructure.Data.Models;

namespace LaktiBg.Core.Models.Event
{
    public class EventViewModel
    {

        public EventViewModel()
        {
            Types = new List<EventType>();
            Participants = new List<UsersEvents>();
            Comments = new List<Comment>();
        }
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<EventType> Types { get; set; }

        public string StartDate { get; set; } = string.Empty;

        public Place Place { get; set; } = null!;

        public string OrganizerId { get; set; } = string.Empty;

        public int? MinRatingRequired { get; set; }

        public int? ParticipantsMaxCount { get; set; }

        public int? MinAgeRequired { get; set; }

        public string Description { get; set; } = string.Empty;

        public ICollection<UsersEvents> Participants { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
