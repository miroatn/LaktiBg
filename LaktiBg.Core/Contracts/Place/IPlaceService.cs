using LaktiBg.Core.Models.PlaceModels;

namespace LaktiBg.Core.Contracts.Place
{
    public interface IPlaceService
    {
        Task<int> CreateAsync(PlaceFormModel model, string ownerId);
    }
}
