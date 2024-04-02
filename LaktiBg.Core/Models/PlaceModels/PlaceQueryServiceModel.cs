using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaktiBg.Core.Models.PlaceModels
{
    public class PlaceQueryServiceModel
    {
        public int TotalPlacesCount { get; set; }

        public IEnumerable<PlaceViewModel> Places { get; set; } = new List<PlaceViewModel>();
    }
}
