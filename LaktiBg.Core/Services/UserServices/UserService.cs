using LaktiBg.Core.Contracts.User;
using LaktiBg.Infrastructure.Data.Common;
using LaktiBg.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LaktiBg.Core.Services.UserServices
{

    public class UserService : IUserService
    {
        private readonly IRepository repository;

        public UserService(IRepository _repository)
        {
            repository = _repository;
        }
        public async Task<bool> ExistById(string userId)
        {
            return await repository.AllReadOnly<ApplicationUser>()
                .AnyAsync(u => u.Id == userId);
        }

        public async Task<string> GetUsersNameByIdAsync(string userId)
        {
           var user = await repository.AllReadOnly<ApplicationUser>()
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user?.FirstName + " " + user?.LastName;
        }
    }
}
