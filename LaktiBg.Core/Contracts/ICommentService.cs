using LaktiBg.Core.Models.CommentModels;
using LaktiBg.Infrastructure.Data.Common;

namespace LaktiBg.Core.Contracts
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentViewModel>> GetCommentsByEventIdAsync(int eventId);

        Task AddCommentAsync(CommentFormModel commentViewModel, int id);

        Task DeleteAsync(int id);

        Task<bool> CommentExistByIdAsync(int id);

        Task<bool> IsUserOwnerOfCommentAsync(int id,int eventId, string userId);
    }
}
