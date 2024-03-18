using LaktiBg.Core.Contracts.Place;
using LaktiBg.Core.Models.Image;
using LaktiBg.Core.Models.PlaceModels;
using LaktiBg.Infrastructure.Data.Common;
using LaktiBg.Infrastructure.Data.Models;

namespace LaktiBg.Core.Services.PlaceServices
{
    public class PlaceService : IPlaceService
    {
        private readonly IRepository repository;

        public PlaceService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<int> CreateAsync(PlaceFormModel model, string ownerId)
        {
            List<ImageViewModel> imageModels = new List<ImageViewModel>();

            if (model.Files != null)
            {
                foreach (var file in model.Files)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        string fileExtention = Path.GetExtension(file.FileName.ToLower());

                        if (memoryStream.Length < 4194304 && isPhoto(fileExtention))
                        {
                            var newphoto = new ImageViewModel()
                            {
                                Bytes = memoryStream.ToArray(),
                                Description = file.FileName,
                                FileExtension = Path.GetExtension(file.FileName),
                                Size = file.Length,
                            };

                            imageModels.Add(newphoto);
                        }
                        else
                        {
                            //ModelState.AddModelError("File", "The file is too large.");
                        }
                    }

                }

            }

            List<Image> images = new List<Image>();

            foreach (var item in imageModels)
            {
                var image = new Image()
                {
                    Bytes = item.Bytes,
                    Description = item.Description,
                    FileExtension = item.FileExtension,
                    Size = item.Size,
                    PlaceId = item.PlaceId,
                };

                images.Add(image);
            }

            Place place = new Place()
            {
                Name = model.Name,
                OwnerId = ownerId,
                Contact = model.Contact,
                Address = model.Address,
                IsPublic = model.IsPublic,
                Images = images,
                Rating = 5,
            };

            await repository.AddAsync(place);
            await repository.SaveChangesAsync();
           
            return place.Id;
        }

        private bool isPhoto(string fileExtention)
        {
            return fileExtention == ".jpg" 
                || fileExtention == ".jpeg" 
                || fileExtention == ".png" 
                || fileExtention == ".bmp";
        }

        public Task<int> CreateAsync(PlaceFormModel model)
        {
            throw new NotImplementedException();
        }
    }
}
