using LaktiBg.Core.Contracts.Event;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LaktiBg.Controllers
{
    public class EventController : BaseController
    {
        private readonly IEventService eventService;

        public EventController(IEventService _eventService)
        {
            eventService = _eventService;
        }

        public async Task<IActionResult> All()
        {
            var model = await eventService.AllAsync();

            return View(model);
        }
    }
}
