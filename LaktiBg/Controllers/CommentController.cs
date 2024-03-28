﻿using LaktiBg.Core.Contracts;
using LaktiBg.Core.Contracts.Event;
using LaktiBg.Core.Contracts.User;
using LaktiBg.Core.Models.CommentModels;
using LaktiBg.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LaktiBg.Controllers
{
    public class CommentController : BaseController
    {
        private readonly IEventService eventService;
        private readonly ICommentService commentService;
        private readonly IUserService userService;

        public CommentController(IEventService _eventService, ICommentService _commentservice, IUserService _userService)
        {
            eventService = _eventService;
            commentService = _commentservice;
            userService = _userService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            CommentFormModel model = new CommentFormModel();

            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Add(CommentFormModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if(await eventService.CheckEventById(id) == false)
            {
                return BadRequest();
            }

            if (await userService.ExistById(User.Id()) == false)
            {
                return Unauthorized();
            }

            model.AuthorId = User.Id();

            await commentService.AddCommentAsync(model, id);

            return RedirectToAction("All", new {id = id});

        }

        [HttpGet]

        public async Task<IActionResult> All(int id)
        {
            IEnumerable<CommentViewModel> models = await commentService.GetCommentsByEventIdAsync(id);
            ViewBag.EventId = id;
            ViewBag.EventName = await eventService.GetEventNameByIdAsync(id);

            return View(models);
        }

        [HttpGet]

        public async Task<IActionResult> Delete(int id, int eventId)
        {
            if (await commentService.CommentExistByIdAsync(id) == false)
            {
                return BadRequest();
            }

            if (await commentService.IsUserOwnerOfCommentAsync(id,eventId, User.Id()) == false)
            {
                return Unauthorized();
            }

            await commentService.DeleteAsync(id);

            ViewBag.EventId = id;
            ViewBag.EventName = await eventService.GetEventNameByIdAsync(id);

            return RedirectToAction("All", new {id = eventId});
        }
    }
}