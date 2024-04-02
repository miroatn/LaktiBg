using LaktiBg.Core.Contracts.ImageService;
using LaktiBg.Core.Contracts.User;
using LaktiBg.Core.Models.UserModels;
using LaktiBg.Core.Services.ImageServices;
using LaktiBg.Core.Services.UserServices;
using LaktiBg.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LaktiBg.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService userService;
        private readonly IImageService imageService;

        public UserController(IUserService _userService, IImageService _imageService)
        {
            userService = _userService;
            imageService = _imageService;

        }
        public async Task<IActionResult> ViewProfile(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (await userService.ExistById(id) == false)
            {
                return BadRequest();
            }

            UserViewModel model = await userService.GetUserViewModelByIdAsync(id);

            if (model.AvatarBytes != null)
            {
                model.AvatarToShow = await imageService.ConvertBytesToStringAsync(model.AvatarBytes);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddFriend(string userId, string friendId)
        {
            if(await userService.ExistById(userId) == false || userId == null)
            {
                return BadRequest();
            }

            if (await userService.ExistById(friendId) == false|| friendId == null)
            {
                return BadRequest();
            }

            await userService.AddFriendAsync(userId, friendId);


            return RedirectToAction("ViewProfile", new { id = friendId });
        }

        public async Task<IActionResult> ShowFriends(string userId)
        {
            if (await userService.ExistById(userId) == false)
            {
                return BadRequest();
            }

            IList<FriendViewModel> friends = await userService.GetUserFriendsAsync(userId);

            foreach (var user in friends)
            {
                if (user.Image != null)
                {
                    user.ImageToShow = await imageService.ConvertImageToStringAsync(user.Image);
                }
            }

            foreach (var user in friends)
            {
                if (user.Image != null)
                {
                    user.ImageToShow = await imageService.ConvertImageToStringAsync(user.Image);
                }
            }

            return View(friends);
        }

        public async Task<IActionResult> FriendRequests(string id)
        {
            if (await userService.ExistById(id) == false)
            {
                return BadRequest();
            }

            IList<UserFriendsViewModel> models = await userService.GetFriendRequestsAsync(id);

            if (models == null)
            {
                RedirectToAction("ViewProfile", new { id = id });
            }

            return View(models);
        }

        public async Task<IActionResult> AcceptRequest(string userId, string friendId)
        {
            if (await userService.ExistById(userId) == false)
            {
                return BadRequest();
            }

            if (await userService.ExistById(friendId) == false)
            {
                return BadRequest();
            }

            await userService.AcceptFriendRequestAsync(userId, friendId);

            return RedirectToAction("ShowFriends", new { userId = userId});

        }

        public async Task<IActionResult> RemoveFriend(string userId, string friendId)
        {
            if (await userService.ExistById(userId) == false)
            {
                return BadRequest();
            }

            if (await userService.ExistById(friendId) == false)
            {
                return BadRequest();
            }

            await userService.RemoveFriendAsync(userId, friendId);

            return RedirectToAction("ShowFriends", new { userId = userId });
        }

        public async Task<IActionResult> ChangeRating(string userId, string direction, string friendId)
        {
            if (await userService.ExistById(userId) == false && await userService.ExistById(friendId) == false)
            {
                return BadRequest();
            }

            if (await userService.CheckIfUserCanVoteAsync(userId, friendId))
            {
                await userService.UpdateUserRatingAsync(userId, direction);

            }


            return RedirectToAction("ViewProfile", new { id = userId });
        }

        public async Task<IActionResult> MyEvents(string userId, [FromQuery]AllUserEventsQueryModel model)
        {
            if (await userService.ExistById(userId) == false)
            {
                return BadRequest();
            }

            UserEventQueryServiceModel events = await userService.GetUserEventsAsync(
                userId,
                model.CurrentPage,
                model.EventsPerPage);

            model.TotalEventsCount = events.TotalEventsCount;
            model.Events = events.Events;

            return View(model);
        }

        public async Task<IActionResult> UserAllEvents(string userId, [FromQuery]AllUserEventsQueryModel model)
        {
            if (await userService.ExistById(userId) == false)
            {
                return BadRequest();
            }

            UserEventQueryServiceModel events = await userService.GetUsersAllEventsAsync(
                userId,
                model.CurrentPage,
                model.EventsPerPage);

            model.TotalEventsCount = events.TotalEventsCount;
            model.Events = events.Events;

            return View(model);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(string userId)
        {
            if (await userService.ExistById(userId) == false)
            {
                return BadRequest();
            }

            if (User.Id() != userId)
            {
                return Unauthorized();
            }

            UserEditModel model = await userService.GetUserEditModelAsync(userId);

            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(UserEditModel model)
        {
            if (await userService.ExistById(User.Id()) == false)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userId = User.Id();

            await userService.EditUserAsync(model, userId);

            return RedirectToAction("ViewProfile", new {id = userId});

        }


    }


}
