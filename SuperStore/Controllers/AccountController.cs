using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperStore.Core.Entities.User;
using SuperStore.Core.Services.Contracts;
using SuperStore.DTOs;
using SuperStore.Errors;
using SuperStore.Extentions;
using System.Security.Claims;

namespace SuperStore.Controllers
{
   
    public class AccountController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,IAuthService authService,IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
            _mapper = mapper;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var User = await _userManager.FindByEmailAsync(loginDto.Email);
            if (User is null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(User, loginDto.Password,false);
            if(result.Succeeded is false) return Unauthorized(new ApiResponse(401));

            return Ok(new UserDto()
            {
                DisplayName=User.DisplayName, Email=User.Email,
                Token=await _authService.CreateTokenAsync(User,_userManager)
            });

        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (CheckEmailExists(registerDto.Email).Result.Value) return BadRequest(new ApiResponse(400, "Email already exists"));
            var User = new AppUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Email.Split('@')[0],
            };
            var Result = await _userManager.CreateAsync(User, registerDto.Password);
            if (Result.Succeeded is false) return BadRequest(new ApiResponse(401));
            return Ok(new UserDto()
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await _authService.CreateTokenAsync(User, _userManager)
            });
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var Email=User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(Email);
            return Ok(new UserDto()
            {
                DisplayName=user.DisplayName,
                Email  = user.Email,
                Token= await _authService.CreateTokenAsync(user, _userManager)
            });
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await _userManager.FindUserWithAddressAsync(User);
            var MappedAddress=_mapper.Map<AddressDto>(user.Address);
            if(MappedAddress is null)return NotFound(new ApiResponse(404,"Address is not found"));
            return MappedAddress;
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto model)
        {
            var user = await _userManager.FindUserWithAddressAsync(User);
            model.Id = user.Address.Id;
            var mappedAddress = _mapper.Map<Address>(model);
           
            user.Address=mappedAddress;
            var Result = await _userManager.UpdateAsync(user);
            if (!Result.Succeeded) return BadRequest(new ApiResponse(400, "Failed to update the address"));
            
            return Ok(model);
        }
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            
            if (user == null) return false;
            return true;
        }

    }
}
