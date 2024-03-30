using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaktiBg.Core.Models.UserModels
{
    public class UserFriendsViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;

        public string? UserName { get; set; }

        public string UserFriendId { get; set; } = string.Empty;

        public bool IsAccepted { get; set; }
    }
}
