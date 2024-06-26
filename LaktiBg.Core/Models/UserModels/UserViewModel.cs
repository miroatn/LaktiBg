﻿using LaktiBg.Core.Models.ImageModels;
using Microsoft.AspNetCore.Http;

namespace LaktiBg.Core.Models.UserModels
{
    public class UserViewModel
    {
        public string Id { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }

        public decimal Rating { get; set; }

        public ICollection<UserFriendsViewModel> Friends { get; set; } = new List<UserFriendsViewModel>();

        public DateTime RegistrationDate { get; set; }

        public ICollection<UsersEventsViewModel> FinishedEvents { get; set; } = new List<UsersEventsViewModel>();

        public ICollection<UsersEventsViewModel> OngoingEvents { get; set; } = new List<UsersEventsViewModel>();

        public string? Address { get; set; }

        public string? Description { get; set; }

        public byte[] AvatarBytes { get; set; } = null!;

        public string AvatarToShow { get; set; } = string.Empty;

        public IFormFile? File { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }
    }
}
