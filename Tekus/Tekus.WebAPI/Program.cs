using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tekus.BusinessLogic.Data;
using Tekus.Core.Entities;

namespace Tekus.WebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IHost host =  CreateHostBuilder(args).Build();
          using(var scope = host.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;
                ILoggerFactory loggerFactory =
                services.GetRequiredService<ILoggerFactory>();
                
                try
                {
                    TekusDbContext context = services.GetRequiredService<TekusDbContext>();
                    await context.Database.MigrateAsync();
                    await TekusDbContextData.LoadDataAsync(context,loggerFactory);
                   
                    UserManager<User> userManager = services.GetRequiredService<UserManager<User>>();
                    SecurityDbContext identityContext = services.GetRequiredService<SecurityDbContext>();
                    await identityContext.Database.MigrateAsync();
                    await SecurityDbContextData.SeedUserAsync(userManager);


                }

                catch (Exception e)
                {
                     var logger= loggerFactory.CreateLogger<Program>();
                    logger.LogError(e, "Errores en el proceso de Migración");
                } 
            }
            host.Run();
        
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
