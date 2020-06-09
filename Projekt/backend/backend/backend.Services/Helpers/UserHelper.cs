using backend.Data.DbModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace backend.Services.Helpers
{
    public static class UserHelper
    {
        public async static Task<string> GetId(this UserManager<User> userManager, ClaimsPrincipal user)
        {
            string userId;
            // sprawdzam czy uzytkownik istnieje
            if (user.Identity.Name != null)
            {
                var userIdentity = await userManager.FindByEmailAsync(user.Identity.Name);
                userId = userIdentity.Id;
            }
            else
            {
                userId = string.Empty;
            }

            return userId;
        }
    }
}
