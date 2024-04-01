using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaktiBg.Core.Models.EventModels
{
    public class EventQueryServiceModel
    {
        public int TotalEventsCount { get; set; }

        public IEnumerable<EventViewModel> Events { get; set; } = new List<EventViewModel>();
    }
}
