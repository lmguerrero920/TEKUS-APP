using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Core.Entities;

namespace Tekus.BusinessLogic.Data
{
    public  class SecurityDbContextData
    {
        public static async  Task SeedUserAsync(UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                User user = new User
                {
                    Name = "Luis",
                    LastName = "Guerrero",
                    UserName = "luguerrero10",
                    Email ="luguerrero10@poligran.edu.co"

                };
              await  userManager.CreateAsync(user,"Xamarin10*++");
            }
        }

    }
}
