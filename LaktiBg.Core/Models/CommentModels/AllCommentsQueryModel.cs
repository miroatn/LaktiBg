using LaktiBg.Core.Models.PlaceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaktiBg.Core.Models.CommentModels
{
    public class AllCommentsQueryModel
    {
        public int CommentsPerPage { get; } = 5;

        public string SearchTerm { get; set; } = null!;

        public int CurrentPage { get; set; } = 1;

        public int TotalCommentsCount { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();
    }
}
