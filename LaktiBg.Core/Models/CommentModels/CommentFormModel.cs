using System.ComponentModel.DataAnnotations;
using static LaktiBg.Core.Constants.MessageConstants;
using static LaktiBg.Infrastructure.Constants.DataConstants.CommentConstants;

namespace LaktiBg.Core.Models.CommentModels
{
    public class CommentFormModel
    {
        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(TextMaxLenght, MinimumLength = TextMinLenght, ErrorMessage = LenghtMessage)]
        public string Text { get; set; } = string.Empty;

        public string AuthorId { get; set; } = string.Empty;

        public int EventId { get; set; }

    }
}
