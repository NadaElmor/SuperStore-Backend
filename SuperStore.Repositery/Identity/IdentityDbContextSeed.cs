using Microsoft.AspNetCore.Identity;
using SuperStore.Core.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Repositery.Identity
{
    public static class IdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var User = new AppUser()
                {
                    DisplayName="Nada",
                    Email="nada@gmail.com",
                    UserName="NadaElmor",
                    PhoneNumber="0109827816"
                };
                await userManager.CreateAsync(User,"Pa$$w0rd");
            }
        }
    }
}
