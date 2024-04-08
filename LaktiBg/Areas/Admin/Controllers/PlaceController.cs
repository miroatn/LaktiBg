using LaktiBg.Core.Contracts.PlaceServices;
using LaktiBg.Core.Models.PlaceModels;
using Microsoft.AspNetCore.Mvc;

namespace LaktiBg.Areas.Admin.Controllers
{
    public class PlaceController : AdminBaseController
    {
        private readonly IPlaceService placeService;
        private readonly ILogger<PlaceController> logger;

        public PlaceController(IPlaceService _placeService, ILogger<PlaceController> _logger)
        {
            placeService = _placeService;
            logger = _logger;

        }
        public async Task<IActionResult> ApproveList()
        {
            IEnumerable<PlaceViewModel> models = await placeService.GetAllUnaprovedAsync();

            return View(models);
        }

        public async Task<IActionResult> Approve(int id)
        {
            try
            {
                await placeService.Approve(id);
            }
            catch (NullReferenceException ex)
            {
                logger.LogError(ex, "Admin/Place/Approve");
                return BadRequest();
            }

            return RedirectToAction(nameof(ApproveList));
        }
    }
}
