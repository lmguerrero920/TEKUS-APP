using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tekus.BusinessLogic.Logic;
using Tekus.Core.Entities;
using Tekus.Core.Interfaces;
using Tekus.Core.Specifications;
using Tekus.WebAPI.DTOs;
using Tekus.WebAPI.Errors;
using Tekus.WebAPI.Extensions;

namespace Tekus.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager; 
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IGenericSecurityRepository<User> _securityRepository;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService,
           IMapper mapper, IPasswordHasher<User> passwordHasher, IGenericSecurityRepository<User> genericSecurityRepository, RoleManager<IdentityRole>  roleManager )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _securityRepository = genericSecurityRepository;
            _roleManager = roleManager;

        }

        [HttpPost("login")]
        //Validación de Usuari: Se geenera Token con fecha de expiración y data asociada al modelo user
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            User user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (User == null)
            {
                return Unauthorized(new CodeErrorResponse(401));
            }

            Microsoft.AspNetCore.Identity.SignInResult result = 
                await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized(new CodeErrorResponse(401));
            }

            IList<string> list = await _userManager.GetRolesAsync(user);
            IList<string> roles = list;

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user, roles),
                Name = user.Name,
                LastName = user.LastName, 
                Admin = roles.Contains("ADMIN") ? true : false
            };

        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            User user = new User
            {
                Email = registerDto.Email,
                UserName = registerDto.Username,
                Name = registerDto.Name,
                LastName = registerDto.LastName
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new CodeErrorResponse(400));
            }

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Token = _tokenService.CreateToken(user, null),
                Email = user.Email,
                UserName = user.UserName,
                Admin = false
            };

        }



        //[Authorize]
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<UserDto>> Update(string id, RegisterDto registerDto)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(new CodeErrorResponse(404, "El user no existe"));
            }

            user.Name = registerDto.Name;
            user.LastName = registerDto.LastName; 

            if (!string.IsNullOrEmpty(registerDto.Password))
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, registerDto.Password);
            }

            IdentityResult result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(new CodeErrorResponse(400, "No se pudo actualizar el user"));
            }

            IList<string> roles = await _userManager.GetRolesAsync(user);

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user, roles), 
                Admin = roles.Contains("ADMIN") ? true : false

            };
        }


       // [Authorize(Roles = "ADMIN")]
        [HttpGet("pagination")]
        public async Task<ActionResult<Pagination<UserDto>>> Getusers([FromQuery] UserSpecificationParams userParams)
        {
            UserSpecification spec = new UserSpecification(userParams);
            IReadOnlyList<User> users = await _securityRepository.GetAllWithSpec(spec);

            UserForCountingSpecification specCount = new UserForCountingSpecification(userParams);
            int totalusers = await _securityRepository.CountAsync(specCount);

            decimal rounded = Math.Ceiling(Convert.ToDecimal(totalusers) / Convert.ToDecimal(userParams.PageSize));
            int totalPages = Convert.ToInt32(rounded);

            IReadOnlyList<UserDto> data = _mapper.Map<IReadOnlyList<User>, IReadOnlyList<UserDto>>(users);

            return Ok(
                new Pagination<UserDto>
                {
                    Count = totalusers,
                    Data = data,
                    PageCount = totalPages,
                    PageIndex = userParams.PageIndex,
                    PageSize = userParams.PageSize
                }
             );
        }


       // [Authorize(Roles = "ADMIN")]
        [HttpPut("role/{id}")]
        public async Task<ActionResult<UserDto>> UpdateRole(string id, RoleDto roleParam)
        {
            IdentityRole role = await _roleManager.FindByNameAsync(roleParam.Name);
            if (role == null)
            {
                return NotFound(new CodeErrorResponse(404, "El role no existe"));
            }

            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(new CodeErrorResponse(404, "El user no existe"));
            }

            UserDto userDto = _mapper.Map<User, UserDto>(user);


            if (roleParam.Status)
            {

                IdentityResult result = await _userManager.AddToRoleAsync(user, roleParam.Name);
                if (result.Succeeded)
                {
                    userDto.Admin = true;
                }

                if (result.Errors.Any())
                {
                    if (result.Errors.Where(x => x.Code == "UserAlreadyInRole").Any())
                    {
                        userDto.Admin = true;
                    }
                }
            }
            else
            {

                IdentityResult result = await _userManager.RemoveFromRoleAsync(user, roleParam.Name);
                if (result.Succeeded)
                {
                    userDto.Admin = false;
                }
            }


            if (userDto.Admin)
            {
                List<string> roles = new List<string>();
                roles.Add("ADMIN");
                userDto.Token = _tokenService.CreateToken(user, roles);
            }
            else
            {
                userDto.Token = _tokenService.CreateToken(user, null);
            }

            return userDto;
        }


        //[Authorize(Roles = "ADMIN")]
        [HttpGet("account/{id}")]
        public async Task<ActionResult<UserDto>> GetuserBy(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound(new CodeErrorResponse(404, "el user no existe"));
            }

            IList<string> roles = await _userManager.GetRolesAsync(user);

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName, 
                Admin = roles.Contains("ADMIN") ? true : false
            };

        }


        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetUser()
        {
            User user = await _userManager.FindUserAsync(HttpContext.User);

            IList<string> roles = await _userManager.GetRolesAsync(user);

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName, 
                Token = _tokenService.CreateToken(user, roles),
                Admin = roles.Contains("ADMIN") ? true : false
            };
        }

        [HttpGet("validateemail")]
        public async Task<ActionResult<bool>> ValidateEmail([FromQuery] string email)
        {
            User user = await _userManager.FindByEmailAsync(email); 
            if (user == null) return false; 
            return true;
        }


        [HttpGet("QuantityUsers")]
        public async Task<ActionResult<Pagination<UserDto>>> QuantityUsers([FromQuery] UserSpecificationParams userParams)
        {
            UserForCountingSpecification specCount = new UserForCountingSpecification(userParams);
            int totalUsers = await _securityRepository.CountAsync(specCount); 
            return Ok(
                new ReportingDto
                {
                    Details = "Cantidad de users en el Sistema",
                    Count = totalUsers

                }
             );
        }




    }
}
