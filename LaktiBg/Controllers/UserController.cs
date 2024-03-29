using LaktiBg.Core.Contracts.ImageService;
using LaktiBg.Core.Contracts.User;
using LaktiBg.Core.Models.UserModels;
using LaktiBg.Core.Services.ImageServices;
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
    }
}
