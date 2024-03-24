using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaktiBg.Core.Contracts.User
{
    public interface IUserService 
    {
        public Task<bool> ExistById(string userId);

        public Task<string> GetUsersNameByIdAsync(string userId);

        public Task<int> GetUsersAgeById(string userId);

        public Task<decimal> GetUsersRatingById(string userId);
    }
}
