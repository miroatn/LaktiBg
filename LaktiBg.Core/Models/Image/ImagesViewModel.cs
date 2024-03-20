namespace LaktiBg.Core.Models.Image
{
    public class ImagesViewModel
    {
        public int PlaceId { get; set; }

        public Dictionary<int, string> imagesToShow = new Dictionary<int, string>();
    }
}
