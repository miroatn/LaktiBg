using LaktiBg.Core.Contracts.ImageService;
using LaktiBg.Core.Contracts.PlaceServices;
using LaktiBg.Core.Models.ImageModels;
using LaktiBg.Core.Models.PlaceModels;
using LaktiBg.Infrastructure.Data.Common;
using LaktiBg.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using static LaktiBg.Core.Constants.ErrorMessageConstants;


namespace LaktiBg.Core.Services.PlaceServices
{
    public class PlaceService : IPlaceService
    {
        private readonly IRepository repository;
        private readonly IImageService imageService;


        public PlaceService(IRepository _repository, IImageService _imageService)
        {
            repository = _repository;
            imageService = _imageService;

        }

        public async Task<PlaceQueryServiceModel> AllAsync(
            string userId,
            string? searchTerm = null,
            int currentPage = 1,
            int placesPerPage = 1)
        {
            var placesToShow = repository.AllReadOnly<Place>()
                                    .Where(p => (p.IsPublic == true || p.OwnerId == userId) && p.IsApproved == true);

            if (searchTerm != null)
            {
                string normalizedSearchTerm = searchTerm.ToLower();
                placesToShow = placesToShow
                                    .Where(p => p.Name.ToLower().Contains(normalizedSearchTerm) ||
                                                p.Address.ToLower().Contains(normalizedSearchTerm) ||
                                                p.Contact.ToLower().Contains(normalizedSearchTerm));
            }


            IEnumerable<PlaceViewModel> places = await placesToShow
                .Skip((currentPage - 1) * placesPerPage)
                .Take(placesPerPage)
                .Where(p => p.IsPublic == true || p.OwnerId == userId)
                .Select(p => new PlaceViewModel
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

            foreach (PlaceViewModel model in places)
            {
                foreach (var image in model.Images)
                {
                    string base64String = Convert.ToBase64String(image.Bytes);
                    string imageDataURL = $"data:image/png;base64,{base64String}";
                    model.ImagesToShow.Add(imageDataURL);
                }
            }

            int totalPlaces = await placesToShow.CountAsync();

            return new PlaceQueryServiceModel()
            {
                TotalPlacesCount = totalPlaces,
                Places = places
            };
        }

        public async Task Approve(int id)
        {
            Place? place = await repository.All<Place>()
                                 .Where(p => p.Id == id)
                                 .FirstOrDefaultAsync(); 

            if (place == null)
            {
                throw new NullReferenceException(PlaceNotFoundError);
            }

            place.IsApproved = true;
            await repository.SaveChangesAsync();
        }

        public async Task<int> CreateAsync(PlaceFormModel model, string ownerId)
        {
            IList<Image> images = new List<Image>();


            if (model.Files != null)
            {
                images = await imageService.GetImagesFromViewModelAsync(model.Files, ownerId);

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

        public async Task DeletePlace(int id)
        {
            Place? place = await repository.GetByIdAsync<Place>(id);

            if (place != null)
            {
                List<Image> placeImages = await repository.All<Image>()
                                                          .Where(i => i.PlaceId == id)
                                                          .ToListAsync();

                if (placeImages.Count != 0)
                {
                    await repository.RemoveRange(placeImages);
                }

                await repository.RemoveAsync(place);
            }

            await repository.SaveChangesAsync();
        }

        public async Task<PlaceViewModel> Details(int id)
        {
            Place? place = await repository.GetByIdAsync<Place>(id);

            if (place == null)
            {
                throw new NullReferenceException(PlaceNotFoundError);
            }

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

        public async Task Edit(PlaceFormModel model, string userId)
        {
            Place? place = await repository.GetByIdAsync<Place>(model.Id);

            IList<Image> images = new List<Image>();

            if (model.Files != null)
            {
                images = await imageService.GetImagesFromViewModelAsync(model.Files, userId);

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

        public async Task<IEnumerable<PlaceViewModel>> GetAllUnaprovedAsync()
        {
            IEnumerable<PlaceViewModel> places = await repository.All<Place>()
               .Where(p => p.IsApproved == false)
               .Select(p => new PlaceViewModel
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

            foreach (PlaceViewModel model in places)
            {
                foreach (var image in model.Images)
                {
                    string base64String = Convert.ToBase64String(image.Bytes);
                    string imageDataURL = $"data:image/png;base64,{base64String}";
                    model.ImagesToShow.Add(imageDataURL);
                }
            }

            return places;
        }

        public async Task<PlaceFormModel> GetPlaceFormModelByPlaceId(int id)
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

    }
}
