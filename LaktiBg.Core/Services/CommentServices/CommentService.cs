using LaktiBg.Core.Contracts;
using LaktiBg.Infrastructure.Data.Common;

namespace LaktiBg.Core.Services.CommentServices
{
    public class CommentService : ICommentService
    {
        private readonly IRepository repository;

        public CommentService(IRepository _repository)
        {
            repository = _repository;
        }
    }
}
