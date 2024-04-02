using LaktiBg.Core.Enums;
using LaktiBg.Core.Models.EventModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaktiBg.Core.Models.PlaceModels
{
    public class AllPlacesQueryModel
    {
        public int PlacesPerPage { get; } = 3;

        public string SearchTerm { get; set; } = null!;

        public int CurrentPage { get; set; } = 1;

        public int TotalPlacesCount { get; set; }

        public IEnumerable<PlaceViewModel> Places { get; set; } = new List<PlaceViewModel>();
    }
}
