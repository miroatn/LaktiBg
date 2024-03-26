using LaktiBg.Core.Contracts;
using LaktiBg.Core.Contracts.Event;
using LaktiBg.Core.Contracts.User;
using LaktiBg.Core.Models.CommentModels;
using LaktiBg.Infrastructure.Data.Common;
using LaktiBg.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LaktiBg.Core.Services.CommentServices
{
    public class CommentService : ICommentService
    {
        private readonly IRepository repository;
        private readonly IUserService userService;
        private readonly IEventService eventService;

        public CommentService(IRepository _repository, IUserService _userService, IEventService _eventService)
        {
            repository = _repository;
            userService = _userService;
            eventService = _eventService;
        }

        public async Task AddCommentAsync(CommentFormModel model, int id)
        {
            Comment comment = new Comment()
            {
                Text = model.Text,
                AuthorId = model.AuthorId,
                EventId = id,
                DateTime = DateTime.Now,
            };

            await repository.AddAsync(comment);
            await repository.SaveChangesAsync();
        }

        public async Task<bool> CommentExistByIdAsync(int id)
        {
            return await repository.All<Comment>()
                                .AnyAsync(comment => comment.Id == id);

        }

        public async Task DeleteAsync(int id)
        {
            Comment? commentToDelete = await repository.All<Comment>()
                                            .Where(c => c.Id == id)
                                            .FirstOrDefaultAsync();

            if (commentToDelete != null)
            {
                await repository.RemoveAsync(commentToDelete);
                await repository.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CommentViewModel>> GetCommentsByEventIdAsync(int eventId)
        {

            IEnumerable<CommentViewModel> comments = await repository.AllReadOnly<Comment>()
                                    .Where(c => c.EventId == eventId)
                                    .Select(c => new CommentViewModel
                                    { 
                                        Id = c.Id,
                                        Text = c.Text,
                                        AuthorId = c.AuthorId,
                                        EventId = c.EventId,
                                        DateTime = c.DateTime,
                                    })
                                    .ToListAsync();

            foreach (var comment in comments)
            {
                comment.AuthorName = await userService.GetUsersNameByIdAsync(comment.AuthorId);
                comment.EventTitle = await eventService.GetEventNameByIdAsync(comment.EventId);
            }

            return comments;
        }

        public async Task<bool> IsUserOwnerOfCommentAsync(int id,int eventId, string userId)
        {
            return await repository.All<Comment>()
                            .AnyAsync(c => c.Id == id && c.AuthorId == userId && c.EventId == eventId);
        
        }
    }
}
