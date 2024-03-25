namespace LaktiBg.Core.Models.CommentModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public string AuthorId { get; set; } = string.Empty;
        public int EventId { get; set; }
        public string AuthorName { get; set; } = string.Empty;

        public string EventTitle { get; set; } = string.Empty;
    }
}
