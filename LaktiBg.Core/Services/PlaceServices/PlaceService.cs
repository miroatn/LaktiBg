using LaktiBg.Core.Contracts.Place;
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
            List<Image> images = new List<Image>();

            foreach (var item in model.Images)
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
    }
}
