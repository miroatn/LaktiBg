using LaktiBg.Core.Contracts.ImageService;
using LaktiBg.Core.Models.ImageModels;
using LaktiBg.Core.Services.PlaceServices;
using Microsoft.AspNetCore.Mvc;

namespace LaktiBg.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageService imageService;

        public ImageController(IImageService _imageService)
        {
            imageService = _imageService;
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
    }
}
