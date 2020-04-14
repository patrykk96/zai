using backend.Data.DbModels;
using backend.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace backend.Repository
{
    public static class DbInitializer
    {
        public static void Seed(RoleManager<IdentityRole> roleManager)
        {
            //TODO: Add seeding data and users
            SeedRoles(roleManager);
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                foreach (var roleName in Enum.GetValues(typeof(UserRole)))
                {
                    IdentityRole role = new IdentityRole();
                    role.Name = roleName.ToString();
                    roleManager.CreateAsync(role).Wait();
                }
            }

        }
    }
}
