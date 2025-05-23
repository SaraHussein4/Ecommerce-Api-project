using AutoMapper;
using ECommerce.Core.Identity;
using ECommerce.Core.Service;
using ECommerceApi.DTOs;
using ECommerceApi.Errors;
using ECommerceApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ECommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService ,IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            this.mapper = mapper;
        }
        [HttpPost("Register")]

        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if (CheckEmailExists(model.Email).Result.Value)
            {
                return BadRequest("This Email Is Already Exist");
            }

            var user = new AppUser()
            {
                DispalyName = model.DispalyName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber,
             

            };
            var Result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, "User");
            if (!Result.Succeeded)
            {
                var errors = Result.Errors.Select(e => e.Description).ToArray();
                return BadRequest(new
                {
                    statusCode = 400,
                    errors = errors
                });
            }
            await _userManager.AddToRoleAsync(user, "User");

            var returnerUser = new UserDto()
            {
                DispalyName = model.DispalyName,
                Email = model.Email,
                Token = await _tokenService.CreateTokenAsync(user , _userManager),
                  Role = "User"

            };
            return Ok(returnerUser);

        }


        [HttpPost("Login")]

        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "User";
            if (user is null) return Unauthorized();
            var Result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!Result.Succeeded) return Unauthorized();
            return new UserDto
            {
                DispalyName = user.DispalyName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager),
           Role = role
            };
        }

        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null)
            {
                return BadRequest(new ApiResponse(400, "Email claim not found in token."));
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound(new ApiResponse(404, "User not found."));
            }

            return Ok(new UserDto
            {
                DispalyName = user.DispalyName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });
        }
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> CurrentUserWithAddress()
        {
            var user =await _userManager.FindUserWithAddressAsync(User);
            var mappedAddress = mapper.Map<Address, AddressDto>(user.Address);
            return (mappedAddress);
        }

        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto UpdatedAddress)
        {
            var user = await _userManager.FindUserWithAddressAsync(User);
            if (user == null) return Unauthorized();
            var address = mapper.Map<AddressDto, Address>(UpdatedAddress);
            address.id = user.Address.id;
            user.Address = address;
            var Result = await _userManager.UpdateAsync(user);
            if (!Result.Succeeded) return BadRequest();
            return (UpdatedAddress);
        }
        [HttpGet("EmailExists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
           
            return await _userManager.FindByEmailAsync(email) is not null;

        }

        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "User logged out successfully." });
        }

    }
}
