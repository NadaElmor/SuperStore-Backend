using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SuperStore.Core.Entities.User;
using System.Security.Claims;

namespace SuperStore.Extentions
{
    public static class UserManagerExtentions
    {
        public static async Task<AppUser?> FindUserWithAddressAsync(this UserManager<AppUser> userManager, ClaimsPrincipal User)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var user =await userManager.Users.Include(U => U.Address).SingleOrDefaultAsync(U=>U.Email==Email);
            return user;
        }
    }
}
