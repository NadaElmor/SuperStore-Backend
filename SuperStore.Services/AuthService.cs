using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SuperStore.Core.Entities.User;
using SuperStore.Core.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SuperStore.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
        {
            //private claim ( userDefined )
            var AuthClaims = new List<Claim>()
            { 
                new Claim(ClaimTypes.GivenName,user.UserName),
                new Claim(ClaimTypes.Email,user.Email)
            };
            //check if there any role
            var UserRoles = await userManager.GetRolesAsync(user);
            foreach (var Role in UserRoles) AuthClaims.Add(new Claim(ClaimTypes.Role, Role));
            //Authkey
            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            //Generate Token
            var Token = new JwtSecurityToken(
                audience: _configuration["JWT:ValidAudience"],
                issuer:_configuration["JWT:ValidIssuer"],
                expires: DateTime.UtcNow.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
                claims:AuthClaims,
                signingCredentials:new SigningCredentials(AuthKey,SecurityAlgorithms.HmacSha384)
                );
            return new JwtSecurityTokenHandler().WriteToken(Token);

        }
    }
}
