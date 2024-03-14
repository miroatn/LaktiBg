using System.ComponentModel.DataAnnotations.Schema;

namespace LaktiBg.Infrastructure.Data.Models
{
    public class EventTypeConnection
    {
        [ForeignKey(nameof(Event))]
        public int EventId { get; set; }

        public Event Event { get; set; } = null!;

        [ForeignKey(nameof(EventType))]
        public int EventTypeId { get; set; }

        public EventType EventType { get; set; } = null!;


    }
}
