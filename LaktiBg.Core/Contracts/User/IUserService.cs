using LaktiBg.Core.Models.UserModels;
using LaktiBg.Infrastructure.Data.Models;

namespace LaktiBg.Core.Contracts.User
{
    public interface IUserService 
    {
        Task<bool> ExistById(string userId);

        Task<string> GetUsersNameByIdAsync(string userId);

        Task<int> GetUsersAgeById(string userId);

        Task<decimal> GetUsersRatingById(string userId);

        Task<UserViewModel> GetUserViewModelByIdAsync(string userId);

        Task<ICollection<UsersEventsViewModel>> GetUsersFinishedEventsAsync(string userId);

        Task<ICollection<UsersEventsViewModel>> GetUsersOnGoingEventsAsync(string userId);

        Task AddFriendAsync(string userId, string friendId);

        Task<IList<FriendViewModel>> GetUserFriendsAsync(string userId);

        Task<bool> CheckIfUserIsFriend(string userId, string friendId);

    }
}
