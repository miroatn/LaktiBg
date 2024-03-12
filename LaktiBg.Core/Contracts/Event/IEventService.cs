using LaktiBg.Core.Models.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaktiBg.Core.Contracts.Event
{
    public interface IEventService
    {
        Task<IEnumerable<EventViewModel>> AllAsync();
    }
}
