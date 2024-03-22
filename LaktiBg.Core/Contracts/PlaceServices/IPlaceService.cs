using LaktiBg.Core.Models.PlaceModels;
using LaktiBg.Infrastructure.Data.Models;

namespace LaktiBg.Core.Contracts.PlaceServices
{
    public interface IPlaceService
    {
        Task<int> CreateAsync(PlaceFormModel model, string ownerId);

        Task<IEnumerable<PlaceViewModel>> AllAsync();

        Task<PlaceViewModel> Details(int id);

        Task Edit(PlaceFormModel model);

        Task DeletePlace(int placeId);

        Task<bool> PlaceExistById(int id);

        Task<PlaceFormModel> GetPlaceFormModelByPlaceId(int id);

        Task<bool> IsUserOwner(string userId, int placeId);

        Task DeleteImage(int imageId);

        Task<Dictionary<int, string>> FindImagesByPlaceId(int placeId);

        Task<int> FindPlaceIdByImageId(int imageId);


    }
}
