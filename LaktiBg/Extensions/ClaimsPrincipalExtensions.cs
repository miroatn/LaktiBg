using LaktiBg.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using static LaktiBg.Core.Constants.RoleConstants;
using System.Security.Claims;

namespace LaktiBg.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole(AdminRole);
        }

    }
}
