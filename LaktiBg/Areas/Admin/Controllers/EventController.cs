using LaktiBg.Core.Contracts.Event;
using LaktiBg.Core.Models.EventModels;
using Microsoft.AspNetCore.Mvc;

namespace LaktiBg.Areas.Admin.Controllers
{
    public class EventController : AdminBaseController
    {
        private readonly IEventService eventService;

        public EventController(IEventService _eventService)
        {
            eventService = _eventService;
        }

        public async Task<IActionResult> AllEventTypes()
        {
            EventTypeAddModel model = new EventTypeAddModel();

            model.Models = await eventService.GetEventTypeViewsAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEventType(string EventName)
        {
            if (EventName != null)
            {
                await eventService.AddNewEventType(EventName);
            }
            else
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(AllEventTypes));
        }
    }
}
