using LaktiBg.Core.Models.PlaceModels;

namespace LaktiBg.Core.Contracts.Place
{
    public interface IPlaceService
    {
        Task<int> CreateAsync(PlaceFormModel model, string ownerId);

        Task<IEnumerable<PlaceViewModel>> AllAsync();

        Task<PlaceViewModel> Details(int id);

        Task Edit(PlaceFormModel model);

        Task Delete(int id);

        Task<bool> PlaceExistById(int id);

        Task<PlaceFormModel> FindPlaceById(int id);

        Task<bool> IsUserOwner(string userId, int placeId);

        Task DeleteImage(int imageId);

        Task<Dictionary<int, string>> FindImagesByPlaceId(int placeId);

        Task<int> FindPlaceIdByImageId(int imageId);

        Task DeletePlace(int placeId);

    }
}
