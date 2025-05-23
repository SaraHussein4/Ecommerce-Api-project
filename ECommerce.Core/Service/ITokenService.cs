﻿using ECommerce.Core.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Service
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(AppUser user , UserManager<AppUser> userManager);
    }
}
