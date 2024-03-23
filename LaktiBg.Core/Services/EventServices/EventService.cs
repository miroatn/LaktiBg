using LaktiBg.Core.Contracts.Event;
using LaktiBg.Core.Models.CommentModels;
using LaktiBg.Core.Models.EventModels;
using LaktiBg.Core.Models.ImageModels;
using LaktiBg.Core.Models.PlaceModels;
using LaktiBg.Core.Models.UserModels;
using LaktiBg.Infrastructure.Data.Common;
using LaktiBg.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using static LaktiBg.Infrastructure.Constants.DataConstants;

namespace LaktiBg.Core.Services.EventServices
{
    public class EventService : IEventService
    {
        private readonly IRepository repository;

        public EventService(IRepository _repository)
        {
            repository = _repository;
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
                EventTypeConnection? connection = await repository.All<EventType>()
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

        public async Task<IEnumerable<EventViewModel>> AllAsync()
        {

            IEnumerable<EventViewModel> models =  await repository.AllReadOnly<Event>()
                .Select(e => new EventViewModel()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Types = e.Types.Select(t => new EventTypeViewModel
                    {
                       Id = e.Id,
                       Name = e.Name
                    }).ToList(),
                    StartDate = e.StartDate.ToString(DateTimeFormat),
                    Place = e.Place,
                    OrganizerId = e.OrganizerId,
                    MinRatingRequired = e.MinRatingRequired,
                    ParticipantsMaxCount = e.ParticipantsMaxCount,
                    MinAgeRequired = e.MinAgeRequired,
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
    }
}
