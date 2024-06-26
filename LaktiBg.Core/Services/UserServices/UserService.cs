﻿using LaktiBg.Core.Contracts.Event;
using LaktiBg.Core.Contracts.ImageService;
using LaktiBg.Core.Contracts.User;
using LaktiBg.Core.Models.UserModels;
using LaktiBg.Infrastructure.Data.Common;
using LaktiBg.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp.Formats;
using static LaktiBg.Core.Constants.ErrorMessageConstants;

namespace LaktiBg.Core.Services.UserServices
{

    public class UserService : IUserService
    {
        private readonly IRepository repository;


        public UserService(IRepository _repository)
        {
            repository = _repository;
        }
        public async Task<bool> ExistById(string userId)
        {
            return await repository.AllReadOnly<ApplicationUser>()
                .AnyAsync(u => u.Id == userId);
        }

        public async Task<string> GetUsersNameByIdAsync(string userId)
        {
           var user = await repository.AllReadOnly<ApplicationUser>()
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user?.FirstName + " " + user?.LastName;
        }

        public async Task<int> GetUsersAgeById(string userId)
        {
            ApplicationUser? user = await repository.AllReadOnly<ApplicationUser>()
                        .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new UnauthorizedAccessException(UnauthorizedAccesError);
            }

            int Years = new DateTime(DateTime.Now.Subtract(user.BirthDate).Ticks).Year - 1;

            return Years;
        }

        public async Task<decimal> GetUsersRatingById(string userId)
        {
            return await repository.AllReadOnly<ApplicationUser>()
                .Where(u => u.Id == userId)
                .Select(u => u.Rating)
                .FirstOrDefaultAsync();

        }

        public async Task<UserViewModel> GetUserViewModelByIdAsync(string userId)
        {
            UserViewModel? model = await repository.AllReadOnly<ApplicationUser>()
                                                    .Where(au => au.Id == userId)
                                                    .Select(au => new UserViewModel
                                                    {
                                                        Id = au.Id,
                                                        FirstName = au.FirstName,
                                                        LastName = au.LastName,
                                                        BirthDate = au.BirthDate,
                                                        Rating = au.Rating,
                                                        Friends = au.Friends.Select(f => new UserFriendsViewModel
                                                        {
                                                            UserId = f.UserId,
                                                            UserFriendId = f.UserFriendId
                                                        }).ToList(),
                                                        RegistrationDate = au.RegistrationDate,
                                                        Address = au.Address == null ? "No information" : au.Address,
                                                        Description = au.Description == null ? "No information" : au.Description,
                                                        UserName = au.UserName,
                                                        PhoneNumber = au.PhoneNumber == null ? "No information" : au.PhoneNumber,
                                                        AvatarBytes = au.Avatar.Bytes
                                                    })
                                                    .FirstOrDefaultAsync();

            if (model != null)
            {
                model.FinishedEvents = await GetUsersFinishedEventsAsync(userId);

                model.OngoingEvents = await GetUsersOnGoingEventsAsync(userId);
            }




            return model;
        }

        public async Task<ICollection<UsersEventsViewModel>> GetUsersFinishedEventsAsync(string userId)
        {
            return await repository.AllReadOnly<UsersEvents>()
                                                    .Where(ue => ue.UserId == userId &&
                                                    ue.Event.IsFinished == true
                                                    && ue.Event.IsDeleted == false)
                                                    .Select(ue => new UsersEventsViewModel
                                                    {
                                                        UserId = ue.UserId,
                                                        EventId = ue.Event.Id,
                                                        EventName = ue.Event.Name,
                                                        EventDate = ue.Event.StartDate
                                                    }).ToListAsync();
        }

        public async Task<UserEventQueryServiceModel> GetUsersAllEventsAsync(string userId,
                                                                                int currentPage = 1,
                                                                                int eventsPerPage = 1)
        {
            var eventsToShow = repository.AllReadOnly<UsersEvents>()
                                                    .Where(ue => ue.UserId == userId
                                                    && ue.Event.IsDeleted == false)
                                                    .OrderByDescending(ue => ue.Event.StartDate);


            IEnumerable<UsersEventsViewModel> events = await eventsToShow
                                                            .Skip((currentPage - 1) * eventsPerPage)
                                                            .Take(eventsPerPage)
                                                            .Select(ue => new UsersEventsViewModel
                                                            {
                                                            UserId = ue.UserId,
                                                            EventId = ue.Event.Id,
                                                            EventName = ue.Event.Name,
                                                            EventDate = ue.Event.StartDate
                                                            })
                                                            .ToListAsync();

            int totalEventsToShow = await eventsToShow.CountAsync();

            return new UserEventQueryServiceModel
            {
                Events = events,
                TotalEventsCount = totalEventsToShow
            };
        }


        public async Task<ICollection<UsersEventsViewModel>> GetUsersOnGoingEventsAsync(string userId)
        {
            return await repository.AllReadOnly<UsersEvents>()
                                                    .Where(ue => ue.UserId == userId &&
                                                    ue.Event.IsFinished == false
                                                    && ue.Event.IsDeleted == false)
                                                    .Select(ue => new UsersEventsViewModel
                                                    {
                                                        UserId = ue.UserId,
                                                        EventId = ue.Event.Id,
                                                        EventName = ue.Event.Name,
                                                        EventDate = ue.Event.StartDate
                                                    }).ToListAsync();
        }

        public async Task<UserEventQueryServiceModel> GetUserEventsAsync(
                                                        string userId,
                                                        int currentPage = 1,
                                                        int eventsPerPage = 1)
        {

            var eventsToShow = repository.AllReadOnly<Event>()
                                                    .Where(ue => ue.OrganizerId == userId
                                                         && ue.IsDeleted == false)
                                                    .OrderByDescending(ue => ue.StartDate);

             IEnumerable<UsersEventsViewModel> events = await eventsToShow
                                                    .Skip((currentPage - 1) * eventsPerPage)
                                                    .Take(eventsPerPage)
                                                    .Select(ue => new UsersEventsViewModel
                                                    {
                                                        UserId = ue.OrganizerId,
                                                        EventId = ue.Id,
                                                        EventName = ue.Name,
                                                        EventDate = ue.StartDate,
                                                        Status = ue.IsFinished == true ? "Приключил" : "Настоящ"
                                                       
                                                    })
                                                    .ToListAsync();

            int totalEventsToShow = await eventsToShow.CountAsync();

            return new UserEventQueryServiceModel 
            {
                Events = events,
                TotalEventsCount = totalEventsToShow
            };
        }

        public async Task AddFriendAsync(string userId, string friendId)
        {

            UserFriends userFriends = new UserFriends() 
            { 
                UserId = userId,
                UserFriendId = friendId
            };


            ApplicationUser? currentUser = await repository.All<ApplicationUser>()
                                                .Where(au => au.Id == userId)
                                                .FirstOrDefaultAsync();

            if (currentUser != null)
            {
                currentUser.Friends.Add(userFriends);
            }

            await repository.SaveChangesAsync();
        }

        public async Task<IList<FriendViewModel>> GetUserFriendsAsync(string userId)
        {
            List<FriendViewModel> friends = await repository.AllReadOnly<UserFriends>()
                                    .Where(uf => uf.UserId == userId && uf.IsAccepted == true)
                                    .Select(uf => new FriendViewModel
                                    {
                                        Id = uf.UserFriendId,
                                        Name = uf.UserFriend.FirstName + " " + uf.UserFriend.LastName,
                                        Image = uf.UserFriend.Avatar,
                                        Email = uf.UserFriend.Email,
                                    })
                                    .ToListAsync();



            return friends;
        }

        public async Task<bool> CheckIfUserIsFriend(string userId, string friendId)
        {
            return await repository.AllReadOnly<UserFriends>()
                                   .AnyAsync(uf => (uf.UserId == userId && uf.UserFriendId == friendId) ||
                                   (uf.UserId == friendId && uf.UserFriendId == userId));

        }

        public async Task<IList<UserFriendsViewModel>> GetFriendRequestsAsync(string userId)
        {
            return await repository.AllReadOnly<UserFriends>()
                                    .Where(uf => uf.UserFriendId == userId && uf.IsAccepted == false)
                                    .Select(uf => new UserFriendsViewModel
                                    {
                                        Id = uf.Id,
                                        UserId = uf.UserId,
                                        UserName = uf.User.FirstName + " " + uf.User.LastName,
                                        UserFriendId = uf.UserFriendId,
                                        IsAccepted = uf.IsAccepted,
                                    }).ToListAsync();
        }



        public async Task AcceptFriendRequestAsync(string userId, string friendId)
        {
            UserFriends? friend = await repository.All<UserFriends>()
                                  .Where(uf => uf.UserFriendId == userId 
                                  && uf.UserId == friendId
                                  && uf.IsAccepted == false)
                                  .FirstOrDefaultAsync();

            if (friend != null)
            {
                friend.IsAccepted = true;
                await repository.SaveChangesAsync();
            }


            UserFriends userFriends = new UserFriends()
            {
                UserId = userId,
                UserFriendId = friendId,
                IsAccepted = true
            };

            ApplicationUser? currentUser = await repository.All<ApplicationUser>()
                                                .Where(au => au.Id == userId)
                                                .FirstOrDefaultAsync();

            if (currentUser != null)
            {
                currentUser.Friends.Add(userFriends);
            }

            await repository.SaveChangesAsync();
        }

        public async Task<string> GetFriendRequestCountAsync(string userId)
        {
            int result = await repository.AllReadOnly<UserFriends>()
                                        .Where(uf => uf.UserFriendId == userId 
                                            && uf.IsAccepted == false)
                                        .CountAsync();

            if (result == 0)
            {
                return "0";
            }

            return result.ToString();
        }

        public async Task RemoveFriendAsync(string userId, string friendId)
        {
            UserFriends? user = await repository.All<UserFriends>()
                                        .Where(uf => uf.UserId == userId && uf.UserFriendId == friendId)
                                        .FirstOrDefaultAsync();

            if (user != null)
            {
                await repository.RemoveAsync(user);
            }

            UserFriends? friend = await repository.All<UserFriends>()
                                        .Where(uf => uf.UserId == friendId
                                                &&  uf.UserFriendId == userId)
                                        .FirstOrDefaultAsync();

            if (friend != null)
            {
                await repository.RemoveAsync(friend);
            }

            await repository.SaveChangesAsync();
        }

        public async Task UpdateUserRatingAsync(string userId, string direction)
        {
            ApplicationUser? user = await repository.All<ApplicationUser>()
                                        .Where(au => au.Id == userId)
                                        .FirstOrDefaultAsync();

            if (user != null)
            {
                if (direction == "up" && user.Rating < 7)
                {
                    user.Rating += 0.05M;
                }
                else if (direction == "down" && user.Rating > 1)
                {
                    user.Rating -= 0.05M;
                }
            }

            await repository.SaveChangesAsync();
        }

        public async Task<bool> CheckIfUserCanVoteAsync(string userId, string friendId)
        {
            UserFriends? currentRelation = await repository.All<UserFriends>()
                                                .Where(uf => uf.UserId == friendId &&  
                                                uf.UserFriendId == userId)
                                                .FirstOrDefaultAsync();

            int sameEventCount = await GetFriendsSameEventCounterAsync(userId, friendId);

            if (currentRelation != null)
            {
                if (currentRelation.VisitedEventCounter < sameEventCount)
                {
                    currentRelation.VisitedEventCounter = sameEventCount;
                    await repository.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<int> GetFriendsSameEventCounterAsync(string userId, string friendId)
        {
            return await repository.All<Event>()
                           .CountAsync(e => e.Participants.Any(p => p.UserId == userId 
                                         && e.IsFinished == true)
                                         && e.Participants.Any(p => p.UserId == friendId
                                         && e.IsFinished == true));

        }

        public async Task EditUserAsync(UserEditModel model, string userId)
        {
            ApplicationUser? user = await repository.All<ApplicationUser>()
                                                .Where(au => au.Id == userId)
                                                .FirstOrDefaultAsync();

            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.BirthDate = model.BirthDate;
                user.Address = model.Address;
                user.Description = model.Description;
                user.PhoneNumber = model.PhoneNumber;
                await repository.SaveChangesAsync();
            }
        }


        public async Task<UserEditModel> GetUserEditModelAsync(string userId)
        {
            UserEditModel? model = await repository.All<ApplicationUser>()
                                    .Where(au => au.Id == userId)
                                    .Select(au => new UserEditModel
                                    {
                                        FirstName = au.FirstName,
                                        LastName = au.LastName,
                                        BirthDate = au.BirthDate,
                                        Address = au.Address,
                                        Description = au.Description,
                                        PhoneNumber = au.PhoneNumber
                                    })
                                    .FirstOrDefaultAsync();

            return model;
        }

        public async Task<bool> CheckIfUsersAreAlreadyFriendsAsync(string userId, string friendId)
        {
            return await repository.AllReadOnly<UserFriends>()
                                    .Where(uf => uf.UserId == userId
                                                && uf.UserFriendId == friendId)
                                    .AnyAsync();
        }
    }
}
