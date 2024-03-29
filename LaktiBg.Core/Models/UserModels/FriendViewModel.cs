using LaktiBg.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaktiBg.Core.Models.UserModels
{
    public class FriendViewModel
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public Image? Image { get; set; } = null!;

        public string ImageToShow { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

    }
}
