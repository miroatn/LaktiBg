using Microsoft.AspNetCore.Mvc;

namespace LaktiBg.Areas.Admin.Controllers
{
    public class ImageController : AdminBaseController
    {
        [HttpPost]
        public async Task<IActionResult> AllPlaceImages(List<string> imagesToShow)
        {

            return View(imagesToShow);
        }
    }
}
