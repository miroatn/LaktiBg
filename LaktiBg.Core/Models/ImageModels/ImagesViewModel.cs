namespace LaktiBg.Core.Models.ImageModels
{
    public class ImagesViewModel
    {
        public int EntityId { get; set; }

        public string EntityType { get; set; } = string.Empty;

        public Dictionary<int, string> imagesToShow = new Dictionary<int, string>();
    }
}
