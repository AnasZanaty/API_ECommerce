using Core.IdentityEntities;
using Demo.HandleResponses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.TokenService;
using Services.UserServices;
using Services.UserServices.Dto;
using System.Security.Claims;

namespace Demo.Controllers
{
   
    public class AccountController : BaseController
    {
   
        private readonly IUserService userService;
        private readonly UserManager<AppUser> userManager;

        public AccountController(IUserService userService , UserManager<AppUser> userManager)
        {
          
            this.userService = userService;
            this.userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>>Register (RegisterDto registerDto)
        {
            var user = await userService.Register(registerDto);
            if (user == null)
            {
                return BadRequest(new ApiException(400 , "The Email is Registered before"));

            }
            return Ok(user);

        }
        [HttpPost("Login")]

        public async Task <ActionResult<UserDto>> Login (LoginDto loginDto)
        {
            var user = await userService.LogIn(loginDto);
            if (user == null)
            {
                return Unauthorized(new ApiResponse(401));
            }
            return Ok(user);
        }

        [HttpGet("GetCurrentUser")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {

            var email = User?.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(email);

            return new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email
            };

        }
    }
}
