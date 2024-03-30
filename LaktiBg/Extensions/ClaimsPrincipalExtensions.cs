﻿using LaktiBg.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace LaktiBg.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }

    }
}
