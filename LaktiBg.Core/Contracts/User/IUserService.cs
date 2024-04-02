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

        Task<UserEventQueryServiceModel> GetUsersAllEventsAsync(string userId,
                                                                   int currentPage,
                                                                   int eventsPerPage);

        Task AddFriendAsync(string userId, string friendId);

        Task<IList<FriendViewModel>> GetUserFriendsAsync(string userId);

        Task<bool> CheckIfUserIsFriend(string userId, string friendId);

        Task<IList<UserFriendsViewModel>> GetFriendRequestsAsync(string userId);


        Task AcceptFriendRequestAsync(string userId, string friendId);

        Task<string> GetFriendRequestCountAsync(string userId);

        Task RemoveFriendAsync(string userId, string friendId);

        Task UpdateUserRatingAsync(string userId, string direction);

        Task<UserEventQueryServiceModel> GetUserEventsAsync(
                                        string userId,
                                        int currentPage,
                                        int eventsPerPage);

        Task<bool> CheckIfUserCanVoteAsync(string userId, string friendId);

        Task<int> GetFriendsSameEventCounterAsync(string userId, string friendId);

        Task EditUserAsync(UserEditModel model, string userId);

        Task<UserEditModel> GetUserEditModelAsync(string userId);

    }
}
