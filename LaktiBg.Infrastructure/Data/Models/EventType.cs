﻿using System.ComponentModel.DataAnnotations;
using static LaktiBg.Infrastructure.Constants.DataConstants.EventTypeConstants;

namespace LaktiBg.Infrastructure.Data.Models
{
    public class EventType
    {
        public EventType()
        {
            Events = new List<EventTypeConnection>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLenght)]
        public string Name { get; set; } = string.Empty;

        ICollection<EventTypeConnection> Events { get; set; }
    }
}
