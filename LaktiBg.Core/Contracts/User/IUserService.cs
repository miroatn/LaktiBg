﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaktiBg.Core.Contracts.User
{
    public interface IUserService 
    {
        public Task<bool> ExistById(string userId);
    }
}
