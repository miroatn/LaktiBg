using LaktiBg.Core.Models.Event;

namespace LaktiBg.Core.Contracts.Event
{
    public interface IEventService
    {
        Task<IEnumerable<EventViewModel>> AllAsync();

    }
}
