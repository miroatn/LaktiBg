using LaktiBg.Core.Contracts.Event;
using LaktiBg.Core.Contracts.ImageService;
using LaktiBg.Core.Contracts.User;
using LaktiBg.Core.Models.EventModels;
using LaktiBg.Core.Models.ImageModels;
using LaktiBg.Core.Models.UserModels;
using LaktiBg.Extensions;
using Microsoft.AspNetCore.Mvc;


namespace LaktiBg.Controllers
{
    public class ImageController : BaseController
    {
        private readonly IImageService imageService;

        private readonly IEventService eventService;

        private readonly IUserService userService;

        public ImageController(IImageService _imageService, IEventService _eventService, IUserService _userService)
        {
            imageService = _imageService;
            eventService = _eventService;
            userService = _userService;
        }

        [HttpGet]
        public async Task<IActionResult> DeleteImages(int id, string entityType)
        {
            ImagesViewModel model = new ImagesViewModel();

            model.EntityId = id;
            if (entityType == "Place")
            {
                model.imagesToShow = await imageService.ConvertImagesToStringAsync(await imageService.GetImagesByIdAsync(id, "Place"));
            }
            else if (entityType == "Event")
            {
                model.imagesToShow = await imageService.ConvertImagesToStringAsync(await imageService.GetImagesByIdAsync(id, "Event"));

            }

            model.EntityType = entityType;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteImage(int id, int entityId, string entityType)
        {
            string userId = User.Id();

            if (await imageService.CheckIfUserIsTheImageAuthor(userId, id) == false)
            {
                return Forbid();
            }

            await imageService.DeleteImage(id);

            return RedirectToAction("DeleteImages", new { id = entityId, entityType = entityType});
        }

        [HttpGet]

        public async Task<IActionResult> AllEventImages(int id)
        {
            if (await eventService.CheckEventById(id) == false)
            {
                return BadRequest();
            }

            EventViewModel currentEvent = await eventService.GetEventViewModelByIdAsync(id);

            return View(currentEvent);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> AddImagesToEvent(EventViewModel model, int id)
        {

            if (model == null)
            {
                return BadRequest();
            }

            if (await eventService.CheckEventById(id) == false)
            {
                return BadRequest();
            }

            string userId = User.Id();

            if (await userService.ExistById(userId) == false)
            {
                return Unauthorized();
            }

            if (model.Files != null)
            {
                await imageService.SaveImagesToEventAsync(model.Files, id, userId);
            }

            return RedirectToAction("AllEventImages", new { id });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> AddProfileAvatar(UserViewModel model, string userId)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (await userService.ExistById(userId) == false)
            {
                return Unauthorized();
            }

            if (userId != User.Id())
            {
                return Forbid();
            }

            if (model.File != null)
            {
                await imageService.AddNewUserAvatar(model, userId);
            }

            return RedirectToAction("ViewProfile", "User", new {id = userId});
        }
    }
}
