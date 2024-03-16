using LaktiBg.Core.Contracts.Event;
using LaktiBg.Core.Models.Event;
using LaktiBg.Infrastructure.Data.Common;
using LaktiBg.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Task<IEnumerable<EventViewModel>> AllAsync()
        {
            throw new NotImplementedException();
        }

        //public async Task<IEnumerable<EventViewModel>> AllAsync()
        //{
        //    return await repository.AllReadOnly<Event>()
        //        .Select(e => new EventViewModel()
        //        {
        //            Id = e.Id,
        //            Name = e.Name,
        //            Types = e.Types,
        //            StartDate = e.StartDate.ToString(DateTimeFormat),
        //            Place = e.Place,
        //            OrganizerId = e.OrganizerId,
        //            MinRatingRequired = e.MinRatingRequired,
        //            ParticipantsMaxCount = e.ParticipantsMaxCount,
        //            MinAgeRequired = e.MinAgeRequired,
        //            Description = e.Description,
        //            Participants = e.Participants,
        //            Comments = e.Comments,
        //        })
        //        .ToListAsync();
        //}
    }
}
