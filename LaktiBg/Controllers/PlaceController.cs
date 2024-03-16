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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            List<ImageViewModel> images = new List<ImageViewModel>();

            if (model.Files != null)
            {
                foreach (var file in model.Files)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        // Upload the file if less than 4 MB  
                        if (memoryStream.Length < 4194304)
                        {
                            //based on the upload file to create Photo instance.  
                            //You can also check the database, whether the image exists in the database.  
                            var newphoto = new ImageViewModel()
                            {
                                Bytes = memoryStream.ToArray(),
                                Description = file.FileName,
                                FileExtension = Path.GetExtension(file.FileName),
                                Size = file.Length,
                            };
                            //add the photo instance to the list.  
                            images.Add(newphoto);
                        }
                        else
                        {
                            ModelState.AddModelError("File", "The file is too large.");
                        }
                    }

                }

            }


            model.Images = images;
            string userId = User.Id();

            int newPlaceId = await placeService.CreateAsync(model, userId);

            return RedirectToAction(nameof(Details), new {id = newPlaceId});
        }

        [HttpGet]

        public async Task<IActionResult> Details()
        {
            throw new NotImplementedException();
        }
    }
}
