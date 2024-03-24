using LaktiBg.Core.Contracts.Event;
using LaktiBg.Core.Models.EventModels;
using LaktiBg.Core.Models.PlaceModels;
using LaktiBg.Extensions;
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
            string userId = User.Id();
            var model = await eventService.AllAsync(userId);

            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> Add()
        {
            EventFormModel model = new EventFormModel();

            IEnumerable<EventTypeViewModel> eventTypes = await eventService.GetEventTypeViewsAsync();

            if (eventTypes != null)
            {
                model.Types = eventTypes;
            }

            IEnumerable<PlaceEventModel> places = await eventService.GetPlacesViewsAsync();

            if (places != null)
            {
                model.Places = places;
            }

            string organizerid = User.Id();


            if (organizerid == null)
            {
                return BadRequest();
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EventFormModel model)
        {
            string organizerid = User.Id();

            if (organizerid == null)
            {
                return BadRequest();
            }

            model.OrganizerId = organizerid;

            if (!ModelState.IsValid)
            {
                IEnumerable<EventTypeViewModel> eventTypes = await eventService.GetEventTypeViewsAsync();

                if (eventTypes != null)
                {
                    model.Types = eventTypes;
                }

                IEnumerable<PlaceEventModel> places = await eventService.GetPlacesViewsAsync();

                if (places != null)
                {
                    model.Places = places;
                }

                return View(model);
            }

            await eventService.AddAsync(model);

            return RedirectToAction("All", "Event");
        }

        [HttpGet]

        public async Task<IActionResult> Participate(int id, string userId)
        {
            if (User.Id() != userId)
            {
                return BadRequest();
            }

            if (await eventService.CheckEventById(id) == false)
            {
                return BadRequest();
            }

            if (await eventService.CheckIfUserIsAlreadyInEvent(id, userId) == true)
            {
                return RedirectToAction("All");
            }

            

            await eventService.ParticipateInEvent(id, userId);

            return RedirectToAction("All");
        }

        [HttpGet]
        
        public async Task<IActionResult> Details(int id)
        {
            if(await eventService.CheckEventById(id) == false)
            {
                return BadRequest();
            }

            string userId = User.Id();
            var model = await eventService.GetEventByIdAsync(id, userId);

            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> Participants(int id)
        {
            if (await eventService.CheckEventById(id) == false)
            {
                return BadRequest();
            }

            string userId = User.Id();
            var model = await eventService.GetEventByIdAsync(id, userId);

            return View(model);
        }
    }
          

}



