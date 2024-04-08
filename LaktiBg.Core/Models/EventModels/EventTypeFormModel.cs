using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaktiBg.Core.Models.EventModels
{
    public class EventTypeFormModel
    {
        [MaxLength(100)]
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
