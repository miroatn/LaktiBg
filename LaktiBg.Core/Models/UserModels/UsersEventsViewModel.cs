using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaktiBg.Core.Models.UserModels
{
    public class UsersEventsViewModel
    {
        public string UserId { get; set; } = string.Empty;

        public int EventId { get; set; }

        public string EventName { get; set; } = string.Empty;

        public DateTime EventDate { get; set; }

    }
}
