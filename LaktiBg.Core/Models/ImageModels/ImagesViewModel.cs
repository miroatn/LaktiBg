namespace LaktiBg.Core.Models.ImageModels
{
    public class ImagesViewModel
    {
        public int PlaceId { get; set; }

        public Dictionary<int, string> imagesToShow = new Dictionary<int, string>();
    }
}
