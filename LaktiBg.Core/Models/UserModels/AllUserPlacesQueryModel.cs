using LaktiBg.Core.Models.PlaceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaktiBg.Core.Models.UserModels
{
    public class AllUserEventsQueryModel
    {
        public int EventsPerPage { get; } = 5;

        public int CurrentPage { get; set; } = 1;

        public int TotalEventsCount { get; set; }

        public IEnumerable<UsersEventsViewModel> Events { get; set; } = new List<UsersEventsViewModel>();
    }
}
