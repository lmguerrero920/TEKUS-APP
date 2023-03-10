using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tekus.Core.Entities;

namespace Tekus.WebAPI.Extensions
{
    public static class UserManagerExtensions
    {


        public static async Task<User> FindUserAsync(this UserManager<User> input, ClaimsPrincipal usr)
        {
            var email = usr?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var usuario = await input.Users.SingleOrDefaultAsync(x => x.Email == email);

            return usuario;
        }

    }
}
