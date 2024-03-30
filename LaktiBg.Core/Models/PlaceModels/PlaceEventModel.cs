using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaktiBg.Core.Models.PlaceModels
{
    public class PlaceEventModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Address { get; set; } = string.Empty;

        public decimal Rating { get; set; }
    }
}
