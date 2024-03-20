using LaktiBg.Core.Models.Image;
using System.ComponentModel.DataAnnotations;
using static LaktiBg.Infrastructure.Constants.DataConstants.PlaceConstants;
using static LaktiBg.Core.Constants.MessageConstants;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaktiBg.Core.Models.PlaceModels
{
    public class PlaceFormModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(NameMaxLenght,
            MinimumLength = NameMinlenght,
            ErrorMessage = LenghtMessage)]
        public string Name { get; set; } = string.Empty;

        [StringLength(ContactMaxLenght, 
            MinimumLength = ContactMinLenght)]
        public string? Contact { get; set; }

        [StringLength(AddressMaxLenght, MinimumLength = AddressMinLenght)]
        public string? Address { get; set; }

        public bool IsPublic { get; set; }

        public ICollection<ImageViewModel> Images { get; set; } = new List<ImageViewModel>();

        [FromForm]
        [NotMapped]
        public IFormFileCollection? Files { get; set; }

    }
}
