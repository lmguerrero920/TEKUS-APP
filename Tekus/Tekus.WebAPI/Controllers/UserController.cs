using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tekus.Core.Entities;
using Tekus.WebAPI.DTOs;
using Tekus.WebAPI.Errors;

namespace Tekus.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user =  await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized(new CodeErrorResponse(401));
            }
         var  result = await _signInManager.
            CheckPasswordSignInAsync( user, loginDto.Password,false);

            if (!result.Succeeded)
            {
                return Unauthorized(new CodeErrorResponse(401));
            }

            return new UserDto
            {
                Email = user.Email,
                UserName = user.UserName,
                Token="este es el token del usuario",
                Name= user.Name,
                LastName= user.LastName

            };

        }

    }
}
