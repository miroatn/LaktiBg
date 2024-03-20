using LaktiBg.Core.Contracts.Place;
using LaktiBg.Core.Models.Image;
using LaktiBg.Core.Models.PlaceModels;
using LaktiBg.Extensions;
using LaktiBg.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LaktiBg.Controllers
{
    public class PlaceController : BaseController
    {
        private readonly IPlaceService placeService;

        public PlaceController(IPlaceService _placeService)
        {
            placeService = _placeService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            PlaceFormModel model = new PlaceFormModel();

            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Add(PlaceFormModel model)
        {

            string userId = User.Id();

            int newPlaceId = await placeService.CreateAsync(model, userId);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction(nameof(Details), new {id = newPlaceId});
        }


        [HttpGet]

        public async Task<IActionResult> Details(int id)
        {
            var model = await placeService.Details(id);

            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> All()
        {
            var models = await placeService.AllAsync();

            return View(models);
        }

        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {
            if (await placeService.PlaceExistById(id) == false)
            {
                return BadRequest();
            }

            string userId = User.Id();

            if (await placeService.IsUserOwner(userId, id) == false)
            {
                return BadRequest();
            }

            var formModel = await placeService.FindPlaceById(id);

            return View(formModel);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(PlaceFormModel model)
        {
            if (await placeService.PlaceExistById(model.Id) == false)
            {
                return BadRequest();
            }

            string userId = User.Id();

            if (await placeService.IsUserOwner(userId, model.Id) == false)
            {
                return BadRequest();
            }

            await placeService.Edit(model);

            return RedirectToAction("All", "Place");
        }

        [HttpGet]

        public async Task<IActionResult> DeleteImages(int id)
        {
            ImagesViewModel model = new ImagesViewModel();

            model.PlaceId = id;
            model.imagesToShow = await placeService.FindImagesByPlaceId(id);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteImage(int id)
        {
            int placeId = await placeService.FindPlaceIdByImageId(id);

            await placeService.DeleteImage(id);

            return RedirectToAction("DeleteImages", new {id = placeId});
        }

        [HttpGet]

        public async Task<IActionResult> DeletePlace(int id)
        {

        }

        
    }
}
