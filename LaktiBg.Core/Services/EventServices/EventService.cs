﻿using LaktiBg.Core.Contracts.Event;
using LaktiBg.Core.Models.CommentModels;
using LaktiBg.Core.Models.EventModels;
using LaktiBg.Core.Models.ImageModels;
using LaktiBg.Core.Models.PlaceModels;
using LaktiBg.Core.Models.UserModels;
using LaktiBg.Infrastructure.Data.Common;
using LaktiBg.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using static LaktiBg.Infrastructure.Constants.DataConstants;
using static LaktiBg.Core.Constants.MessageConstants;
using LaktiBg.Core.Contracts.User;

namespace LaktiBg.Core.Services.EventServices
{
    public class EventService : IEventService
    {
        private readonly IRepository repository;

        private readonly IUserService userService;


        public EventService(IRepository _repository, IUserService _userService)
        {
            repository = _repository;
            userService = _userService;
        }

        public async Task AddAsync(EventFormModel model)
        {

            IList<EventTypeConnection> types = await GetEventTypeConnections(model.SelectedTypes);

            if (types == null)
            {
                //TODO
            }


            Place place = await GetPlaceByIdAsync(model.SelectedPlaceId);

            if (place == null)
            {
                //TODO
            }

            if (place != null && types != null) 
            {
                Event newEvent = new Event()
                {
                    Name = model.Name,
                    Types = types,
                    CreationDate = DateTime.Now,
                    StartDate = model.StartDate,
                    Place = place,
                    IsPublic = model.IsPublic,
                    IsVisible = model.IsVisible,
                    OrganizerId = model.OrganizerId,
                    MinRatingRequired = model.MinRatingRequired,
                    ParticipantsMaxCount = model.ParticipantsMaxCount,
                    MinAgeRequired = model.MinAgeRequired,
                    Description = model.Description,
                };

                await repository.AddAsync(newEvent);
                await repository.SaveChangesAsync();
            }

        }


        public async Task<IList<EventTypeConnection>> GetEventTypeConnections(IEnumerable<int> ids)
        {
            IList<EventTypeConnection> eventTypeConnections = new List<EventTypeConnection>();

            foreach (var id in ids)
            {
                EventTypeConnection? connection = await repository.AllReadOnly<EventType>()
                                                        .Where(x => x.Id == id)
                                                        .Select(x => new EventTypeConnection
                                                        {
                                                            EventTypeId = x.Id,
                                                        }).FirstOrDefaultAsync();

                if (connection != null)
                {
                    eventTypeConnections.Add(connection);
                }

            }

            return eventTypeConnections;
        }

        public async Task<IEnumerable<EventTypeViewModel>> GetEventTypeViewsAsync()
        {
            return await repository.All<EventType>()
                .Select(et => new EventTypeViewModel
                {
                    Id = et.Id,
                    Name = et.Name
                })
                .ToListAsync();
        }

        public async Task<Place> GetPlaceByIdAsync(int id)
        {
            return await repository.GetByIdAsync<Place>(id);

        }

        public async Task<IEnumerable<PlaceEventModel>> GetPlacesViewsAsync()
        {
            return await repository.All<Place>()
                .Select(p => new PlaceEventModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Address = p.Address,
                    Rating = p.Rating,
                }).ToListAsync();
        }



        public async Task<IEnumerable<EventViewModel>> AllAsync(string userId)
        {

            IEnumerable<EventViewModel> models =  await repository.AllReadOnly<Event>()
                .Where(e => e.IsDeleted == false)
                .Select(e => new EventViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Types = e.Types.Select(t => new EventTypeViewModel
                    {
                       Name = t.EventType.Name,
                    }).ToList(),
                    StartDate = e.StartDate.ToString(DateTimeFormat),
                    Place = e.Place,
                    IsVisible = e.IsVisible,
                    IsFinished = e.IsFinished,
                    IsPublic = e.IsPublic,
                    OrganizerId = e.OrganizerId,
                    MinRatingRequired = e.MinRatingRequired,
                    MinRatingToShow = e.MinRatingRequired != null ? e.MinRatingRequired.ToString() : NoRestrictionAdded,
                    ParticipantsMaxCount = e.ParticipantsMaxCount,
                    ParticipantsMaxCountToShow = e.ParticipantsMaxCount != null ? e.ParticipantsMaxCount.ToString() : NoRestrictionAdded,
                    MinAgeRequired = e.MinAgeRequired,
                    MinAgeRequiredToShow = e.MinAgeRequired != null ? e.MinAgeRequired.ToString() : NoRestrictionAdded,
                    Description = e.Description,
                    Participants = e.Participants.Select(p => new UserViewModel
                    {
                        Id = p.UserId,
                        Name = p.User.FirstName + " " + p.User.LastName,
                    }).ToList(),
                    Comments = e.Comments.Select(c => new CommentViewModel
                    {
                        Id= c.Id,
                        Text = c.Text,
                        AuthorId = c.AuthorId,
                        EventId = c.EventId,
                    }).ToList(),
                    Images = e.Images.Select(i => new ImageViewModel
                    {
                        Id = i.Id,
                        Bytes = i.Bytes,
                        Description = i.Description,
                        FileExtension = i.FileExtension,
                        Size = i.Size,
                        PlaceId = i.PlaceId,
                    }).ToList(),
                })
                .ToListAsync();

            foreach (var model in models)
            {
                model.Organizer = await GetUsersNameByIdAsync(model.OrganizerId);

                model.TypesToShow = string.Join(", ", model.Types.Select(t => t.Name));

                model.UserAge = await userService.GetUsersAgeById(userId);

                model.UserRating = await userService.GetUsersRatingById(userId);
            }


            List<byte[]> imageBytesList = new List<byte[]>();

            foreach (var model in models)
            {
                foreach (var image in model.Images)
                {
                    string base64String = Convert.ToBase64String(image.Bytes);
                    string imageDataURL = $"data:image/png;base64,{base64String}";
                    model.ImagesToShow.Add(imageDataURL);
                }
            }

            return models;
        }

        private async Task<string> GetUsersNameByIdAsync(string userId)
        {
            var user = await repository.AllReadOnly<ApplicationUser>()
                 .FirstOrDefaultAsync(u => u.Id == userId);

            return user?.FirstName + " " + user?.LastName;
        }

        public async Task<bool> CheckEventById(int id)
        {
            return await repository.AllReadOnly<Event>()
                .AnyAsync(u => u.Id == id);
        }

        public async Task ParticipateInEvent(int id, string userId)
        {
            Event? currentEvent = await repository.All<Event>()
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();

            UsersEvents userEvent = new UsersEvents()
            {
                EventId = id,
                UserId = userId
            };

            currentEvent?.Participants.Add(userEvent);

            await repository.SaveChangesAsync();

        }

        public async Task<bool> CheckIfUserIsAlreadyInEvent(int id, string userId)
        {
            return await repository.AllReadOnly<Event>()
                                    .AnyAsync(u => u.Id == id && u.Participants
                                                                    .Any(p => p.UserId == userId));

        }

        public async Task<EventViewModel> GetEventViewModelByIdAsync(int id)
        {
            EventViewModel? model = await repository.AllReadOnly<Event>()
            .Where(e => e.IsDeleted == false && e.Id == id)
            .Select(e => new EventViewModel()
            {
                Id = e.Id,
                Name = e.Name,
                Types = e.Types.Select(t => new EventTypeViewModel
                {
                    Name = t.EventType.Name,
                }).ToList(),
                StartDate = e.StartDate.ToString(DateTimeFormat),
                Place = e.Place,
                IsVisible = e.IsVisible,
                IsFinished = e.IsFinished,
                IsPublic = e.IsPublic,
                OrganizerId = e.OrganizerId,
                MinRatingRequired = e.MinRatingRequired,
                MinRatingToShow = e.MinRatingRequired != null ? e.MinRatingRequired.ToString() : NoRestrictionAdded,
                ParticipantsMaxCount = e.ParticipantsMaxCount,
                ParticipantsMaxCountToShow = e.ParticipantsMaxCount != null ? e.ParticipantsMaxCount.ToString() : NoRestrictionAdded,
                MinAgeRequired = e.MinAgeRequired,
                MinAgeRequiredToShow = e.MinAgeRequired != null ? e.MinAgeRequired.ToString() : NoRestrictionAdded,
                Description = e.Description,
                Participants = e.Participants.Select(p => new UserViewModel
                {
                    Id = p.UserId,
                    Name = p.User.FirstName + " " + p.User.LastName,
                }).ToList(),
                Comments = e.Comments.Select(c => new CommentViewModel
                {
                    Id = c.Id,
                    Text = c.Text,
                    AuthorId = c.AuthorId,
                    EventId = c.EventId,
                }).ToList(),
                Images = e.Images.Select(i => new ImageViewModel
                {
                    Id = i.Id,
                    Bytes = i.Bytes,
                    Description = i.Description,
                    FileExtension = i.FileExtension,
                    Size = i.Size,
                    PlaceId = i.PlaceId,
                }).ToList(),
            })
            .FirstOrDefaultAsync();

            if (model != null)
            {

                model.Organizer = await GetUsersNameByIdAsync(model.OrganizerId);

                model.TypesToShow = string.Join(", ", model.Types.Select(t => t.Name));


                List<byte[]> imageBytesList = new List<byte[]>();

                foreach (var image in model.Images)
                {
                    string base64String = Convert.ToBase64String(image.Bytes);
                    string imageDataURL = $"data:image/png;base64,{base64String}";
                    model.ImagesToShow.Add(imageDataURL);
                }

                return model;

            }

            return null;
        }

        public async Task DeleteAsync(int id)
        {
            Event? currentEvent = await repository.All<Event>()
                                    .Where(e => e.Id == id)
                                    .FirstOrDefaultAsync();

            List<Comment> comments = await repository.All<Comment>()
                                        .Where(c => c.EventId == id)
                                        .ToListAsync();

            List<Image> images = await repository.All<Image>()
                                        .Where(c => c.EventId == id)
                                        .ToListAsync();

            List<EventTypeConnection> etc = await repository.All<EventTypeConnection>()
                                            .Where(c => c.EventId == id)
                                            .ToListAsync();

            if (currentEvent != null)
            {
                if (comments != null)
                {
                    await repository.RemoveRange<Comment>(comments);
                }

                if (images != null)
                {
                    await repository.RemoveRange<Image>(images);
                }

                if (etc != null)
                {
                    await repository.RemoveRange<EventTypeConnection>(etc);
                }

                currentEvent.IsDeleted = true;
                await repository.SaveChangesAsync();
            
            }
        }

        public async Task<EventFormModel> GetEventFormModelByIdAsync(int id)
        {
            IEnumerable<EventTypeViewModel> types = await repository.All<EventType>()
                                                    .Select(et => new EventTypeViewModel
                                                    {
                                                        Id = et.Id,
                                                        Name= et.Name,
                                                    }).ToListAsync();

            IEnumerable<PlaceEventModel> places = await repository.All<Place>()
                                                    .Select(p => new PlaceEventModel
                                                    {
                                                        Id = p.Id,
                                                        Name = p.Name,
                                                        Address = p.Address,
                                                        Rating = p.Rating,
                                                    }).ToListAsync();

            EventFormModel? currentEvent = await repository.All<Event>()
                            .Where(e => e.Id == id)
                            .Select(e => new EventFormModel
                            {
                                Id = e.Id,
                                Name = e.Name,
                                StartDate = e.StartDate,
                                IsPublic = e.IsPublic,
                                IsVisible = e.IsVisible,
                                OrganizerId = e.OrganizerId,
                                MinRatingRequired = e.MinRatingRequired,
                                ParticipantsMaxCount = e.ParticipantsMaxCount,
                                MinAgeRequired = e.MinAgeRequired,
                                Description = e.Description,

                            }).FirstOrDefaultAsync();

            if (currentEvent != null)
            {
                currentEvent.Types = types;
                currentEvent.Places = places;
            }

            return currentEvent;
        }

        public async Task EditAsync(EventFormModel model)
        {
            Event? currentEvent = await repository.All<Event>()
                                            .Where(x => x.Id == model.Id)
                                            .FirstOrDefaultAsync();

            if (currentEvent != null)
            {
                Place place = await GetPlaceByIdAsync(model.SelectedPlaceId);

                IList<EventTypeConnection> eventTypesToDelete = await repository.All<EventTypeConnection>()
                                                                        .Where(etc => etc.EventId == currentEvent.Id)
                                                                        .ToListAsync();

                if (eventTypesToDelete != null)
                {
                    await repository.RemoveRange<EventTypeConnection>(eventTypesToDelete);
                    await repository.SaveChangesAsync();

                }


                IList<EventTypeConnection> types = await GetEventTypeConnections(model.SelectedTypes);

                   
                if (types == null)
                {
                    //TODO
                }
                else
                {
                    foreach (var type in types)
                    {
                        type.EventId = model.Id;
                        await repository.AddAsync(type);
                    }
                    
                }

                if (place == null)
                {
                    //TODO
                }
                else
                {
                    currentEvent.Place = place;
                }

                currentEvent.Name = model.Name;
                currentEvent.StartDate = model.StartDate;
                currentEvent.IsPublic = model.IsPublic;
                currentEvent.IsVisible = model.IsVisible;
                currentEvent.MinRatingRequired = model.MinRatingRequired;
                currentEvent.ParticipantsMaxCount = model.ParticipantsMaxCount;
                currentEvent.MinAgeRequired = model.MinRatingRequired;
                currentEvent.Description = model.Description;

                await repository.SaveChangesAsync();
            }
        }

        public async Task LeaveEvent(int id, string userId)
        {
            UsersEvents? usersEvent = await repository.All<UsersEvents>()
                                             .Where(ue => ue.UserId == userId && ue.EventId == id)
                                             .FirstOrDefaultAsync();

            if (usersEvent != null)
            {
                await repository.RemoveAsync<UsersEvents>(usersEvent);
                await repository.SaveChangesAsync();
            }

        }

        public async Task<string> GetEventNameByIdAsync(int id)
        {
            string? eventName = await repository.All<Event>()
                                    .Where(e => e.Id == id)
                                    .Select(e => e.Name)
                                    .FirstOrDefaultAsync();

            return eventName;
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await repository.All<Event>()
                                    .Where(e => e.Id == id)
                                    .FirstOrDefaultAsync();
        }
    }
}
