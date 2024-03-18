using LaktiBg.Core.Contracts.Place;
using LaktiBg.Core.Models.Image;
using LaktiBg.Core.Models.PlaceModels;
using LaktiBg.Core.Services.ImageServices;
using LaktiBg.Infrastructure.Data.Common;
using LaktiBg.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Http;

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

                        var outputPath = Path.Combine("uploads", file.FileName);
                        var imageService = new ImageService();
                        await imageService.CompressAndSaveImageAsync(file, outputPath, 30);


                        string fileExtention = Path.GetExtension(file.FileName.ToLower());
                        byte[] reducedFile = File.ReadAllBytes(outputPath);

                        await file.CopyToAsync(memoryStream);
                        

                        if (reducedFile.Length < 4194304 && isPhoto(fileExtention))
                        {
                            var newphoto = new ImageViewModel()
                            {
                                Bytes = reducedFile,
                                Description = file.FileName,
                                FileExtension = Path.GetExtension(file.FileName),
                                Size = reducedFile.Length,
                            };

                            imageModels.Add(newphoto);
                        }
                        else
                        {
                            //ModelState.AddModelError("File", "The file is too large.");
                        }

                        //TODO: Deleting the file from folder after uploading
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


        public Task<int> CreateAsync(PlaceFormModel model)
        {
            throw new NotImplementedException();
        }

        private bool isPhoto(string fileExtention)
        {
            return fileExtention == ".jpg"
                || fileExtention == ".jpeg"
                || fileExtention == ".png"
                || fileExtention == ".bmp";
        }
    }
}
