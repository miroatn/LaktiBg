using LaktiBg.Core.Contracts.ImageService;
using LaktiBg.Core.Contracts.User;
using LaktiBg.Core.Models.UserModels;
using LaktiBg.Core.Services.ImageServices;
using LaktiBg.Core.Services.UserServices;
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

        public async Task<IActionResult> ChangeRating(string userId, string direction)
        {
            if (await userService.ExistById(userId) == false)
            {
                return BadRequest();
            }

            await userService.UpdateUserRatingAsync(userId, direction);

            return RedirectToAction("ViewProfile", new { id = userId });
        }


    }


}
