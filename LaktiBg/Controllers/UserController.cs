using LaktiBg.Core.Contracts.User;
using LaktiBg.Core.Models.UserModels;
using Microsoft.AspNetCore.Mvc;

namespace LaktiBg.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService userService;

        public UserController(IUserService _userService)
        {
            userService = _userService;
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
    }
}
