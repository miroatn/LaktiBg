using LaktiBg.Core.Models.CommentModels;
using LaktiBg.Infrastructure.Data.Common;

namespace LaktiBg.Core.Contracts
{
    public interface ICommentService
    {
        Task<CommentQueryServiceModel> GetCommentsByEventIdAsync(
            int eventId,
            string? searchTerm,
            int currentPage,
            int commentsPerPage);

        Task AddCommentAsync(CommentFormModel commentViewModel, int id);

        Task DeleteAsync(int id);

        Task<bool> CommentExistByIdAsync(int id);

        Task<bool> IsUserOwnerOfCommentAsync(int id,int eventId, string userId);
    }
}
