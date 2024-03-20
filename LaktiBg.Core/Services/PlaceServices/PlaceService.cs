using LaktiBg.Core.Contracts.Place;
using LaktiBg.Core.Models.Image;
using LaktiBg.Core.Models.PlaceModels;
using LaktiBg.Core.Services.ImageServices;
using LaktiBg.Infrastructure.Data.Common;
using LaktiBg.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LaktiBg.Core.Services.PlaceServices
{
    public class PlaceService : IPlaceService
    {
        private readonly IRepository repository;

        public PlaceService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<IEnumerable<PlaceViewModel>> AllAsync()
        {
            List<PlaceViewModel> models = await repository.All<Place>().Select(p => new PlaceViewModel
            {
                Id = p.Id,
                IsPublic = p.IsPublic,
                Name = p.Name,
                OwnerId = p.OwnerId,
                Contact = p.Contact,
                Address = p.Address,
                Images = p.Images.Select(i => new ImageViewModel 
                {
                    Id = i.Id,
                    Bytes = i.Bytes,
                    Description = i.Description,
                    FileExtension = i.FileExtension,
                    Size = i.Size,
                    PlaceId = i.PlaceId,
                }).ToList(),
                Rating = p.Rating,
            }).ToListAsync();

            List<byte[]> imageBytesList = new List<byte[]>();

            foreach (PlaceViewModel model in models)
            {
                foreach (var image in model.Images)
                {
                    string base64String = Convert.ToBase64String(image.Bytes);
                    string imageDataURL = $"data:image/png;base64,{base64String}";
                    model.ImagesToShow.Add(imageDataURL);
                }
            }

            return models;
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

                        File.Delete(outputPath);

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

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteImage(int imageId)
        {

            Image? image = await repository.All<Image>()
                .Where(x => x.Id == imageId).FirstOrDefaultAsync();

            if (image != null)
            {
                await repository.RemoveAsync(image);
                await repository.SaveChangesAsync();
            }
        }

        public Task DeletePlace(int placeId)
        {
            throw new NotImplementedException();
        }

        public async Task<PlaceViewModel> Details(int id)
        {
            Place? place = await repository.GetByIdAsync<Place>(id);

            PlaceViewModel model = new();
            model.Images = await repository.All<Image>().Where(x => x.PlaceId == id).Select(i => new ImageViewModel
            {
                Id = i.Id,
                Bytes = i.Bytes,
                Description = i.Description,
                FileExtension = i.FileExtension,
                Size = i.Size,
                PlaceId = i.PlaceId,
            }).ToListAsync();

            if (place != null)
            {
                model.Id = place.Id;
                model.Name = place.Name;
                model.Contact = place.Contact;
                model.Address = place.Address;
                model.IsPublic = place.IsPublic;
                model.Rating = 5;
                model.OwnerId = place.OwnerId;

                foreach (var image in model.Images)
                {
                    string base64String = Convert.ToBase64String(image.Bytes);
                    string imageDataURL = $"data:image/png;base64,{base64String}";
                    model.ImagesToShow.Add(imageDataURL);
                }

            }

            return model;
        }

        public async Task Edit(PlaceFormModel model)
        {
            Place? place = await repository.GetByIdAsync<Place>(model.Id);

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

                        File.Delete(outputPath);
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

            if (place != null) 
            {
                place.Name = model.Name;
                place.Contact = model.Contact;
                place.Address = model.Address;
                place.IsPublic = model.IsPublic;
                place.Images = images;
            }

            await repository.SaveChangesAsync();

        }

        public async Task<Dictionary<int, string>> FindImagesByPlaceId(int placeId)
        {
            Dictionary<int, string> imagesToShow = new Dictionary<int, string>();
            List<Image> images = await repository.All<Image>()
                                                .Where(x => x.PlaceId == placeId)
                                                .ToListAsync();

            foreach (var image in images)
            {
                string base64String = Convert.ToBase64String(image.Bytes);
                string imageDataURL = $"data:image/png;base64,{base64String}";
                imagesToShow.Add(image.Id, imageDataURL);
            }

            return imagesToShow;
        }

        public async Task<PlaceFormModel> FindPlaceById(int id)
        {
            var model = await repository.AllReadOnly<Place>()
                  .Where(p => p.Id == id)
                  .Select(p => new PlaceFormModel
                  {
                      Id = p.Id,
                      Name = p.Name,
                      Contact = p.Contact,
                      Address = p.Address,
                      IsPublic = p.IsPublic,

                  }).FirstAsync();

            model.Images = await repository.All<Image>().Where(x => x.PlaceId == id).Select(i => new ImageViewModel
            {
                Id = i.Id,
                Bytes = i.Bytes,
                Description = i.Description,
                FileExtension = i.FileExtension,
                Size = i.Size,
                PlaceId = i.PlaceId,
            }).ToListAsync();

            return model;
        }

        public async Task<int> FindPlaceIdByImageId(int imageId)
        {
            Image? image = await repository.GetByIdAsync<Image>(imageId);

            if (image != null && image.PlaceId != null)
            {
                return (int)image.PlaceId;
            }

            return 0;
            
        }

        public async Task<bool> IsUserOwner(string userId, int placeId)
        {
            return await repository.AllReadOnly<Place>()
                .AnyAsync(x => x.OwnerId == userId && x.Id == placeId);
        }

        public async Task<bool> PlaceExistById(int id)
        {
            return await repository.AllReadOnly<Place>()
                .AnyAsync(p => p.Id == id);
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
