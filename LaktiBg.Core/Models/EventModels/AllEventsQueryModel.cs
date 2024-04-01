using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaktiBg.Core.Enums;

namespace LaktiBg.Core.Models.EventModels
{
    public class AllEventsQueryModel
    {
        public int EventsPerPage { get; } = 3;

        public string Category { get; set; } = null!;

        public string SearchTerm { get; set; } = null!;

        public EventSorting Sorting { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalEventsCount { get; set; }

        public IEnumerable<string> Categories { get; set; } = null!;

        public IEnumerable<EventViewModel> Events { get; set; } = new List<EventViewModel>();
    }
}
