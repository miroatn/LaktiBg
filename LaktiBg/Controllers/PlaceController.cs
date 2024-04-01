using LaktiBg.Core.Contracts.PlaceServices;
using LaktiBg.Core.Models.ImageModels;
using LaktiBg.Core.Models.PlaceModels;
using LaktiBg.Extensions;
using LaktiBg.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

        public async Task<IActionResult> All([FromQuery]AllPlacesQueryModel model)
        {

            var places = await placeService.AllAsync(
                User.Id(),
                model.SearchTerm,
                model.CurrentPage,
                model.PlacesPerPage);

            model.TotalPlacesCount = places.TotalPlacesCount;
            model.Places = places.Places;

            return View(model);
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

            var formModel = await placeService.GetPlaceFormModelByPlaceId(id);

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

        public async Task<IActionResult> DeletePlace(int id)
        {
            if (await placeService.PlaceExistById(id) == false)
            {
                return BadRequest();
            }

            await placeService.DeletePlace(id);

            return RedirectToAction("All");
        }

        
    }
}
