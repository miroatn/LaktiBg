using LaktiBg.Core.Contracts.Event;
using LaktiBg.Core.Models.Event;
using LaktiBg.Core.Models.PlaceModels;
using LaktiBg.Extensions;
using LaktiBg.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System;
using static LaktiBg.Infrastructure.Constants.DataConstants;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
    }
}
