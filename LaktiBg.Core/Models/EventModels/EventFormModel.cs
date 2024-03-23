using LaktiBg.Core.Models.PlaceModels;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static LaktiBg.Core.Constants.MessageConstants;
using static LaktiBg.Infrastructure.Constants.DataConstants.EventConstants;

namespace LaktiBg.Core.Models.EventModels
{
    public class EventFormModel
    {
        [StringLength(NameMaxLenght,
            MinimumLength = NameMinLenght, 
            ErrorMessage = LenghtMessage)]
        [Required(ErrorMessage = RequiredMessage)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        public IEnumerable<EventTypeViewModel> Types { get; set; } = new List<EventTypeViewModel>();

        public IEnumerable<int> SelectedTypes { get; set; } = new List<int>();

        [Required(ErrorMessage = RequiredMessage)]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = RequiredMessage)]
        public IEnumerable<PlaceEventModel> Places { get; set; } = new List<PlaceEventModel>();

        public int SelectedPlaceId { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        public bool IsPublic { get; set; }
       
        [Required(ErrorMessage = RequiredMessage)]
        public bool IsVisible { get; set; }

        public string OrganizerId { get; set; } = string.Empty;

        public int? MinRatingRequired { get; set; }

        public int? ParticipantsMaxCount { get; set; }

        public int? MinAgeRequired { get; set; }

        [StringLength(DescriptionMaxLenght, 
            MinimumLength = DescriptionMinLenght, 
            ErrorMessage = LenghtMessage)]
        [Required(ErrorMessage = RequiredMessage)]

        public string Description { get; set; } = string.Empty;

    }
}
