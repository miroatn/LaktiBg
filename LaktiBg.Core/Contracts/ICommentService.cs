using LaktiBg.Core.Models.CommentModels;
using LaktiBg.Infrastructure.Data.Common;

namespace LaktiBg.Core.Contracts
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentViewModel>> GetCommentsByEventId(int eventId);

        Task AddComment(CommentFormModel commentViewModel, int id);
    }
}
