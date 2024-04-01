using LaktiBg.Core.Contracts.Event;
using LaktiBg.Core.Contracts.User;
using LaktiBg.Core.Models.EventModels;
using LaktiBg.Core.Models.PlaceModels;
using LaktiBg.Core.Services.UserServices;
using LaktiBg.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LaktiBg.Controllers
{
    public class EventController : BaseController
    {
        private readonly IEventService eventService;

        private readonly IUserService userService;

        public EventController(IEventService _eventService, IUserService _userService)
        {
            eventService = _eventService;
            userService = _userService;
        }

        public async Task<IActionResult> All([FromQuery]AllEventsQueryModel model)
        {
            string userId = User.Id();

            var events = await eventService.AllAsync(userId,
                model.SearchTerm,
                model.Sorting,
                model.CurrentPage,
                model.EventsPerPage);

            model.TotalEventsCount = events.TotalEventsCount;
            model.Events = events.Events;
           
            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> Add()
        {
            if (await userService.ExistById(User.Id()) == false)
            {
                return BadRequest();
            }

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
            if (await userService.ExistById(User.Id()) == false)
            {
                return BadRequest();
            }

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

        public async Task<IActionResult> Edit(int id)
        {
            if (await userService.ExistById(User.Id()) == false)
            {
                return BadRequest();
            }

            if (await eventService.CheckEventById(id) == false)
            {
                return BadRequest();
            }

            var model = await eventService.GetEventFormModelByIdAsync(id);



            if (model == null)
            {
                return BadRequest();
            }

            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(EventFormModel model)
        {
            if (await userService.ExistById(User.Id()) == false)
            {
                return BadRequest();
            }

            if (await eventService.CheckEventById(model.Id) == false)
            {
                return BadRequest();
            }

            await eventService.EditAsync(model);

            return RedirectToAction("Details", "Event", new {id = model.Id});
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

            await eventService.ParticipateInEvent(id, userId);

            return RedirectToAction("Details", new {id = id});
        }

        [HttpGet]
        
        public async Task<IActionResult> Details(int id)
        {
            if(await eventService.CheckEventById(id) == false)
            {
                return BadRequest();
            }

            string userId = User.Id();
            var model = await eventService.GetEventViewModelByIdAsync(id);
            model.UserAge = await userService.GetUsersAgeById(userId);
            model.UserRating = await userService.GetUsersRatingById(userId);

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
            var model = await eventService.GetEventViewModelByIdAsync(id);
            model.UserAge = await userService.GetUsersAgeById(userId);
            model.UserRating = await userService.GetUsersRatingById(userId);

            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> Delete(int id)
        {
           
            await eventService.DeleteAsync(id);

            return RedirectToAction("All", "Event");
        }

        [HttpGet]

        public async Task<IActionResult> Leave(int id, string userId)
        {
            if (await eventService.CheckIfUserIsAlreadyInEvent(id, userId) == false)
            {
                return BadRequest();
            }

            if (await eventService.CheckEventById(id) == false)
            {
                return BadRequest();
            }

            if (await userService.ExistById(userId) == false)
            {
                return Unauthorized();
            }

            await eventService.LeaveEvent(id, userId);

            return RedirectToAction("All");
        }

    }
          

}



