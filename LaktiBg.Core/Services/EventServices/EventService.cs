﻿using LaktiBg.Core.Contracts.Event;
using LaktiBg.Core.Models.Event;
using LaktiBg.Core.Models.PlaceModels;
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

        public Task<IEnumerable<EventViewModel>> AllAsync()
        {
            throw new NotImplementedException();
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

        //public async Task<IEnumerable<EventViewModel>> AllAsync()
        //{
        //    //return await repository.AllReadOnly<Event>()
        //    //    .Select(e => new EventViewModel()
        //    //    {
        //    //        Id = e.Id,
        //    //        Name = e.Name,
        //    //        Types = e.Types,
        //    //        StartDate = e.StartDate.ToString(DateTimeFormat),
        //    //        Place = e.Place,
        //    //        OrganizerId = e.OrganizerId,
        //    //        MinRatingRequired = e.MinRatingRequired,
        //    //        ParticipantsMaxCount = e.ParticipantsMaxCount,
        //    //        MinAgeRequired = e.MinAgeRequired,
        //    //        Description = e.Description,
        //    //        Participants = e.Participants,
        //    //        Comments = e.Comments,
        //    //    })
        //    //    .ToListAsync();
        //}
    }
}
