using LaktiBg.Core.Contracts.Event;
using LaktiBg.Core.Contracts.ImageService;
using LaktiBg.Core.Models.EventModels;
using LaktiBg.Core.Models.ImageModels;
using Microsoft.AspNetCore.Mvc;

namespace LaktiBg.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageService imageService;

        private readonly IEventService eventService;

        public ImageController(IImageService _imageService, IEventService _eventService)
        {
            imageService = _imageService;
            eventService = _eventService;

        }

        [HttpGet]
        public async Task<IActionResult> DeleteImages(int id)
        {
            ImagesViewModel model = new ImagesViewModel();

            model.EntityId = id;
            model.imagesToShow = await imageService.ConvertImagesToStringAsync(await imageService.GetImagesByIdAsync(id,"Place"));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteImage(int id, int entityId)
        {

            await imageService.DeleteImage(id);

            return RedirectToAction("DeleteImages", new { id = entityId });
        }

        [HttpGet]

        public async Task<IActionResult> AllEventImages(int id)
        {
            EventViewModel currentEvent = await eventService.GetEventViewModelByIdAsync(id);

            return View(currentEvent);
        }

        [HttpPost]

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

            if (model.Files != null)
            {
                await imageService.SaveImagesToEventAsync(model.Files, id);
            }

            return RedirectToAction("AllEventImages", new { id });
        }
    }
}
