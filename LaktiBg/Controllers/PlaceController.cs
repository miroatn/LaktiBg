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

        public async Task<IActionResult> Details()
        {
            throw new NotImplementedException();
        }
    }
}
