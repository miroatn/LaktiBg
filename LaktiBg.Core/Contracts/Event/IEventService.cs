using LaktiBg.Core.Models.EventModels;
using LaktiBg.Core.Models.PlaceModels;
using LaktiBg.Infrastructure.Data.Models;

namespace LaktiBg.Core.Contracts.Event
{
    public interface IEventService
    {
        Task<IEnumerable<EventViewModel>> AllAsync(string userId);

        Task<IEnumerable<EventTypeViewModel>> GetEventTypeViewsAsync();

        Task<IEnumerable<PlaceEventModel>> GetPlacesViewsAsync();

        Task<IList<EventTypeConnection>> GetEventTypeConnections(IEnumerable<int> ids);

        Task<Place> GetPlaceByIdAsync(int id);

        Task AddAsync(EventFormModel viewModel);

        Task<bool> CheckEventById(int id);

        Task ParticipateInEvent(int id, string userId);

        Task<bool> CheckIfUserIsAlreadyInEvent(int id, string userId);

        Task<EventViewModel> GetEventViewModelByIdAsync(int id, string userId);

        Task DeleteAsync(int id);

        Task<EventFormModel> GetEventFormModelByIdAsync(int id);

        Task EditAsync(EventFormModel model);

    }
}
