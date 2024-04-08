using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaktiBg.Core.Models.EventModels
{
    public class EventTypeAddModel
    {
        public IEnumerable<EventTypeViewModel> Models { get; set; } = new List<EventTypeViewModel>();

        public string EventName { get; set; } = string.Empty;
    }
}
