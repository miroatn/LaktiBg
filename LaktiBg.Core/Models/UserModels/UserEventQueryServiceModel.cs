using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaktiBg.Core.Models.UserModels
{
    public class UserEventQueryServiceModel
    {
        public IEnumerable<UsersEventsViewModel> Events { get; set; } = new List<UsersEventsViewModel>();

        public int TotalEventsCount { get; set; }
    }
}
