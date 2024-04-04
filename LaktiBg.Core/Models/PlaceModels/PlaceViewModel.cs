using LaktiBg.Core.Models.ImageModels;
using Microsoft.AspNetCore.Http;

namespace LaktiBg.Core.Models.PlaceModels
{
    public class PlaceViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? OwnerId { get; set; }

        public string? Contact { get; set; }

        public string? Address { get; set; }

        public bool IsPublic { get; set; } = false;

        public ICollection<ImageViewModel> Images { get; set; } = new List<ImageViewModel>();

        public List<string> ImagesToShow { get; set; } = new List<string>();

        public decimal Rating { get; set; }

        public IFormFileCollection? Files { get; set; }

        public bool IsApproved { get; set; }
    }
}
