namespace LaktiBg.Core.Models.ImageModels
{
    public class ImageViewModel
    {
        public int Id { get; set; }

        public byte[] Bytes { get; set; } = null!;

        public string Description { get; set; } = string.Empty;

        public string FileExtension { get; set; } = string.Empty;

        public decimal Size { get; set; }

        public string? UserId { get; set; }

        public int? PlaceId { get; set; }
    }
}
