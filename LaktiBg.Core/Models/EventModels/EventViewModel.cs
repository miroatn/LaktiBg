using LaktiBg.Core.Models.CommentModels;
using LaktiBg.Core.Models.ImageModels;
using LaktiBg.Core.Models.UserModels;
using LaktiBg.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace LaktiBg.Core.Models.EventModels
{
    public class EventViewModel
    {

        public EventViewModel()
        {
            Types = new List<EventTypeViewModel>();
            Participants = new List<UserEventViewModel>();
            Comments = new List<CommentViewModel>();
            Images = new List<ImageViewModel>();
            ImagesToShow = new List<string>();
        }
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<EventTypeViewModel> Types { get; set; }

        public string TypesToShow { get; set; } = string.Empty;

        public string StartDate { get; set; } = string.Empty;

        public Place Place { get; set; } = null!;

        public string OrganizerId { get; set; } = string.Empty;

        public string Organizer { get; set; } = string.Empty;

        public bool IsPublic { get; set; } = false;

        public bool IsDeleted { get; set; } = false;

        public bool IsVisible { get; set; } = false;

        public bool IsFinished { get; set; } = false;

        public int? MinRatingRequired { get; set; }

        public int UserAge { get; set; }

        public string? MinRatingToShow { get; set; }

        public decimal UserRating { get; set; }

        public int? ParticipantsMaxCount { get; set; }
        public string? ParticipantsMaxCountToShow { get; set; }

        public int? MinAgeRequired { get; set; }

        public string? MinAgeRequiredToShow { get; set; }

        public string Description { get; set; } = string.Empty;

        public ICollection<UserEventViewModel> Participants { get; set; }

        public ICollection<CommentViewModel> Comments { get; set; }

        public ICollection<ImageViewModel> Images { get; set; }

        public IList<string> ImagesToShow { get; set; }

        public IFormFileCollection? Files { get; set; }
    }
}
